using FluentAssertions;
using Moq;
using Product.Repository;
using Product.Service.Implementations;

namespace Product.Test
{
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;

        private IList<Model.User> _users;
        private Model.User _user;

        public UserServiceTest()
        {
            this._userRepositoryMock = new Mock<IUserRepository>();
            this.Initialize();
        }

        [Fact]
        public async Task Create_User_OK_Test()
        {
            // Given
            this._userRepositoryMock
                .Setup(x => x.CreateAsync(It.IsAny<Model.User>()))
                .ReturnsAsync(1);

            // When
            var userService = new UserService(this._userRepositoryMock.Object);
            var result = await userService.CreateAsync(this._user);

            // Then
            result.Should().Be(1);
            this._userRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<Model.User>()), Times.Once);
        }

        [Fact]
        public async Task Get_Users_OK_Test()
        {
            // Given
            this._userRepositoryMock
                .Setup(x => x.GetAsync())
                .ReturnsAsync(this._users);

            // When
            var userService = new UserService(this._userRepositoryMock.Object);
            var result = await userService.GetAsync();

            // Then
            result.Count.Should().Be(1);
            this._userRepositoryMock.Verify(x => x.GetAsync(), Times.Once);
        }

        [Fact]
        public async Task Get_User_OK_Test()
        {
            // Given
            this._userRepositoryMock
                .Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(this._user);

            // When
            var userService = new UserService(this._userRepositoryMock.Object);
            var result = await userService.GetAsync(1);

            // Then
            result?.Id.Should().Be(1);
            this._userRepositoryMock.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Get_User_Null_Test()
        {
            Model.User? myUser = null;

            // Given
            this._userRepositoryMock
                .Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(myUser);

            // When
            var userService = new UserService(this._userRepositoryMock.Object);
            var result = await userService.GetAsync(6);

            // Then
            result.Should().BeNull();
            this._userRepositoryMock.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Get_User_By_Name_OK_Test()
        {
            // Given
            this._userRepositoryMock
                .Setup(x => x.GetByUserNameAsync(It.IsAny<string>()))
                .ReturnsAsync(this._user);

            // When
            var userService = new UserService(this._userRepositoryMock.Object);
            var result = await userService.GetByUserNameAsync("test");

            // Then
            result?.Id.Should().Be(1);
            this._userRepositoryMock.Verify(x => x.GetByUserNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Update_User_OK_Test()
        {
            // Given
            this._userRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<Model.User>()))
                .ReturnsAsync(this._user);

            // When
            var userService = new UserService(this._userRepositoryMock.Object);
            var result = await userService.UpdateAsync(1, this._user);

            // Then
            result?.Id.Should().Be(1);
            this._userRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<Model.User>()), Times.Once);
        }

        [Fact]
        public async Task Delete_User_OK_Test()
        {
            // Given
            this._userRepositoryMock
                .Setup(x => x.DeleteAsync(It.IsAny<int>()));

            // When
            var userService = new UserService(this._userRepositoryMock.Object);
            await userService.DeleteAsync(1);

            // Then
            this._userRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<int>()), Times.Once);
        }

        private void Initialize()
        {
            this._user = new Model.User
            {
                Id = 1,
                CreatedAt = DateTime.Now,
                IsActive = true,
                LastName = "test",
                FirstName = "test",
                Email = "test@test.com",
                PasswordHash = "12345",
                PasswordSalt = "98765",
                Username = "test"
            };

            this._users = new[] { this._user };
        }
    }
}