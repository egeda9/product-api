using Product.Model;

namespace Product.Service
{
    public interface IUserService
    {
        Task<IList<Model.User>> GetAsync();

        Task<Model.User?> GetAsync(int id);

        Task<User?> GetByUserNameAsync(string userName);

        Task<int> CreateAsync(User user);

        Task<User?> UpdateAsync(int id, Model.User user);

        Task DeleteAsync(int id);
    }
}
