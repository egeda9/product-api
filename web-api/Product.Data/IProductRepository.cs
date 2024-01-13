namespace Product.Repository
{
    public interface IProductRepository
    {
        Task<IList<Model.Product>> GetAsync();

        Task<Model.Product?> GetAsync(int id);

        Task<int> CreateAsync(Model.Product product);

        Task<Model.Product?> UpdateAsync(int id, Model.Product product);

        Task DeleteAsync(int id);
    }
}