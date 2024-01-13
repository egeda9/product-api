using Product.Repository;

namespace Product.Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Model.Product>> GetAsync()
        {
            return await this._productRepository.GetAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Model.Product?> GetAsync(int id)
        {
            return await this._productRepository.GetAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<int> CreateAsync(Model.Product product)
        {
            return await this._productRepository.CreateAsync(product);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<Model.Product?> UpdateAsync(int id, Model.Product product)
        {
            return await this._productRepository.UpdateAsync(id, product);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            await this._productRepository.DeleteAsync(id);
        }
    }
}
