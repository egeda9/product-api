using Product.Model;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Product.Repository.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly Settings _settings;

        public ProductRepository(IOptions<Settings> options)
        {
            this._settings = options.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Model.Product>> GetAsync()
        {
            var result = new List<Model.Product>();

            using (SqlConnection connection = new(this._settings.ConnectionString))
            {
                using (SqlCommand command = new ("GetProducts", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var product = new Model.Product
                                {
                                    Id = (int) reader["Id"],
                                    CreatedAt = (DateTime) reader["CreatedAt"],
                                    UpdatedAt = reader["UpdatedAt"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["UpdatedAt"],
                                    IsAvailable = (bool) reader["IsAvailable"],
                                    IsActive = (bool)reader["IsActive"],
                                    Category = (string)reader["Category"],
                                    Description = (string)reader["Description"],
                                    Manufacturer = (string)reader["Manufacturer"],
                                    StockQuantity = (int)reader["StockQuantity"],
                                    Price = (decimal) reader["Price"],
                                    Name = (string)reader["Name"],
                                    ReleaseDate = (DateTime)reader["ReleaseDate"]
                                };
                                result.Add(product);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No results found.");
                        }
                    }

                    connection.Close();
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Model.Product?> GetAsync(int id)
        {
            Model.Product? product = null;

            using (SqlConnection connection = new(this._settings.ConnectionString))
            {
                using (SqlCommand command = new("GetProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@ProductId", SqlDbType.Int) { Value = id });
                    connection.Open();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                product = new Model.Product
                                {
                                    Id = (int)reader["Id"],
                                    CreatedAt = (DateTime)reader["CreatedAt"],
                                    UpdatedAt = reader["UpdatedAt"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["UpdatedAt"],
                                    IsAvailable = (bool)reader["IsAvailable"],
                                    IsActive = (bool)reader["IsActive"],
                                    Category = (string)reader["Category"],
                                    Description = (string)reader["Description"],
                                    Manufacturer = (string)reader["Manufacturer"],
                                    StockQuantity = (int)reader["StockQuantity"],
                                    Price = (decimal)reader["Price"],
                                    Name = (string)reader["Name"],
                                    ReleaseDate = (DateTime)reader["ReleaseDate"]
                                };
                            }
                        }
                        else
                        {
                            Console.WriteLine("No results found.");
                        }
                    }

                    connection.Close();
                }
            }

            return product;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<int> CreateAsync(Model.Product product)
        {
            int newProductId;

            using (SqlConnection connection = new(this._settings.ConnectionString))
            {
                using (SqlCommand command = new("InsertProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar) {Value = product.Name});
                    command.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar) {Value = product.Description});
                    command.Parameters.Add(new SqlParameter("@Price", SqlDbType.Decimal) {Value = product.Price});
                    command.Parameters.Add(new SqlParameter("@StockQuantity", SqlDbType.Int) {Value = product.StockQuantity});
                    command.Parameters.Add(new SqlParameter("@Manufacturer", SqlDbType.NVarChar) {Value = product.Manufacturer});
                    command.Parameters.Add(new SqlParameter("@Category", SqlDbType.NVarChar) {Value = product.Category});
                    command.Parameters.Add(new SqlParameter("@ReleaseDate", SqlDbType.DateTime2) {Value = product.ReleaseDate});
                    command.Parameters.Add(new SqlParameter("@IsAvailable", SqlDbType.Bit) {Value = product.IsAvailable});

                    var newProductIdParameter = new SqlParameter("@NewProductId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(newProductIdParameter);

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                    newProductId = (int) newProductIdParameter.Value;
                    connection.Close();
                }
            }


            return newProductId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<Model.Product?> UpdateAsync(int id, Model.Product product)
        {
            var existingProduct = await this.GetAsync(id);

            if (existingProduct == null)
                return null;

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price != 0 ? product.Price : existingProduct.Price;
            existingProduct.StockQuantity = product.StockQuantity != 0 ? product.StockQuantity : existingProduct.StockQuantity;
            existingProduct.Manufacturer = product.Manufacturer;
            existingProduct.Category = product.Category;
            existingProduct.ReleaseDate = product.ReleaseDate != DateTime.MinValue ? product.ReleaseDate : existingProduct.ReleaseDate;
            existingProduct.IsAvailable = product.IsAvailable;

            using (SqlConnection connection = new(this._settings.ConnectionString))
            {
                using (SqlCommand command = new("UpdateProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@ProductId", SqlDbType.Int) { Value = id });
                    command.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar) { Value = existingProduct.Name });
                    command.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar) { Value = existingProduct.Description });
                    command.Parameters.Add(new SqlParameter("@Price", SqlDbType.Decimal) { Value = existingProduct.Price });
                    command.Parameters.Add(new SqlParameter("@StockQuantity", SqlDbType.Int) { Value = existingProduct.StockQuantity });
                    command.Parameters.Add(new SqlParameter("@Manufacturer", SqlDbType.NVarChar) { Value = existingProduct.Manufacturer });
                    command.Parameters.Add(new SqlParameter("@Category", SqlDbType.NVarChar) { Value = existingProduct.Category });
                    command.Parameters.Add(new SqlParameter("@ReleaseDate", SqlDbType.DateTime2) { Value = existingProduct.ReleaseDate });
                    command.Parameters.Add(new SqlParameter("@IsAvailable", SqlDbType.Bit) { Value = existingProduct.IsAvailable });

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                    connection.Close();
                }
            }


            return await this.GetAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            using (SqlConnection connection = new(this._settings.ConnectionString))
            {
                using (SqlCommand command = new("DeleteProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@ProductId", SqlDbType.Int) { Value = id });

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                    connection.Close();
                }
            }
        }
    }
}
