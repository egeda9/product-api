using Product.Model;
using Product.Repository;

namespace Product.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IList<User>> GetAsync()
        {
            return await this._userRepository.GetAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User?> GetAsync(int id)
        {
            return await this._userRepository.GetAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<User?> GetByUserNameAsync(string userName)
        {
            return await this._userRepository.GetByUserNameAsync(userName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<int> CreateAsync(User user)
        {
            return await this._userRepository.CreateAsync(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User?> UpdateAsync(int id, User user)
        {
            return await this._userRepository.UpdateAsync(id, user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            await this._userRepository.DeleteAsync(id);
        }
    }
}
