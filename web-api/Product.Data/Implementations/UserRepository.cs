using Product.Model;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Options;

namespace Product.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly Settings _settings;

        public UserRepository(IOptions<Settings> options)
        {
            this._settings = options.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IList<User>> GetAsync()
        {
            var result = new List<User>();

            using (SqlConnection connection = new(this._settings.ConnectionString))
            {
                using (SqlCommand command = new("GetUsers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var user = new User
                                {
                                    Id = (int)reader["Id"],
                                    CreatedAt = (DateTime)reader["CreatedAt"],
                                    UpdatedAt = reader["UpdatedAt"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["UpdatedAt"],
                                    IsActive = (bool)reader["IsActive"],
                                    FirstName = (string)reader["FirstName"],
                                    LastName = (string)reader["LastName"],
                                    Username = (string)reader["Username"],
                                    Email = (string)reader["Email"]
                                };
                                result.Add(user);
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
        public async Task<User?> GetAsync(int id)
        {
            User? user = null;

            using (SqlConnection connection = new(this._settings.ConnectionString))
            {
                using (SqlCommand command = new("GetUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = id });
                    connection.Open();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                user = new User
                                {
                                    Id = (int)reader["Id"],
                                    CreatedAt = (DateTime)reader["CreatedAt"],
                                    UpdatedAt = reader["UpdatedAt"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["UpdatedAt"],
                                    IsActive = (bool)reader["IsActive"],
                                    FirstName = (string)reader["FirstName"],
                                    LastName = (string)reader["LastName"],
                                    Username = (string)reader["Username"],
                                    Email = (string)reader["Email"]
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

            return user;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<User?> GetByUserNameAsync(string userName)
        {
            User? user = null;

            using (SqlConnection connection = new(this._settings.ConnectionString))
            {
                using (SqlCommand command = new("GetUserByUserName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar) { Value = userName });
                    connection.Open();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                user = new User
                                {
                                    Id = (int)reader["Id"],
                                    CreatedAt = (DateTime)reader["CreatedAt"],
                                    UpdatedAt = reader["UpdatedAt"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["UpdatedAt"],
                                    IsActive = (bool)reader["IsActive"],
                                    FirstName = (string)reader["FirstName"],
                                    LastName = (string)reader["LastName"],
                                    Username = (string)reader["Username"],
                                    Email = (string)reader["Email"],
                                    PasswordHash = (string)reader["PasswordHash"]
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

            return user;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<int> CreateAsync(User user)
        {
            int newUserId;

            using (SqlConnection connection = new(this._settings.ConnectionString))
            {
                using (SqlCommand command = new("InsertUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar) { Value = user.Username });
                    command.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar) { Value = user.Email });
                    command.Parameters.Add(new SqlParameter("@PasswordHash", SqlDbType.NVarChar) { Value = user.PasswordHash });
                    command.Parameters.Add(new SqlParameter("@PasswordSalt", SqlDbType.NVarChar) { Value = user.PasswordSalt });
                    command.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar) { Value = user.FirstName });
                    command.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar) { Value = user.LastName });

                    var newUserIdParameter = new SqlParameter("@NewUserId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(newUserIdParameter);

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                    newUserId = (int)newUserIdParameter.Value;
                    connection.Close();
                }
            }


            return newUserId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User?> UpdateAsync(int id, User user)
        {
            var existingUser = await this.GetAsync(id);

            if (existingUser == null)
                return null;

            existingUser.Email = user.Email;
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;

            using (SqlConnection connection = new(this._settings.ConnectionString))
            {
                using (SqlCommand command = new("UpdateUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = id });
                    command.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar) { Value = existingUser.Email });
                    command.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar) { Value = existingUser.FirstName });
                    command.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar) { Value = existingUser.LastName });

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
                using (SqlCommand command = new("DeleteUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = id });

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                    connection.Close();
                }
            }
        }
    }
}
