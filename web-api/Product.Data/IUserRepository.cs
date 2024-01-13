using Product.Model;

namespace Product.Repository
{
    public interface IUserRepository
    {
        Task<IList<User>> GetAsync();

        Task<User?> GetAsync(int id);

        Task<User?> GetByUserNameAsync(string userName);

        Task<int> CreateAsync(User user);

        Task<User?> UpdateAsync(int id, Model.User user);

        Task DeleteAsync(int id);
    }
}
