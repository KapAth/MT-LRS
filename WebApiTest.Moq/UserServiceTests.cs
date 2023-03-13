using Moq;
using Repositories.Repository.Entities;
using Services;
using AutoMapper;
using WebAPI.Models;
using WebAPI.Repositories.Interfaces;

namespace WebApiTest.Moq
{
    [TestClass]
    public class UserServiceTests
    {
        //the ! operator after the null keyword is used to tell the compiler that these fields will never be null after they are initialized
        private Mock<IUsersRepository> _userRepositoryMock = null!;

        private Mock<IMapper> _mapperMock = null!;
        private UserService _userService = null!;

        [TestInitialize]
        public void Initialize()
        {
            _userRepositoryMock = new Mock<IUsersRepository>();
            _mapperMock = new Mock<IMapper>();
            _userService = new UserService(_userRepositoryMock.Object, _mapperMock.Object);
        }

        [TestMethod]
        public async Task GetAllUsersDtoAsync_ShouldReturnAllUsers()
        {
            // Arrange
            var users = new List<User>
        {
            new User { Id = 1, Name = "George", Surname = "Georgiou", EmailAddress = "georgiou@example.com", UserTypeId = 1, UserTitleId = 1 },
            new User { Id = 2, Name = "Thanasis", Surname = "Athanasiou", EmailAddress = "athanasiou@example.com", UserTypeId = 2, UserTitleId = 2 }
        };
            var userDtos = new List<UserDto>
        {
            new UserDto { Id = 1, Name = "George", Surname = "Georgiou", EmailAddress = "georgiou@example.com", UserTypeId = 1, UserTitleId = 1 },
            new UserDto { Id = 2, Name = "Thanasis", Surname = "Athanasiou", EmailAddress = "athanasiou@example.com", UserTypeId = 2, UserTitleId = 2 }
        };
            _userRepositoryMock.Setup(x => x.GetAllUsersAsync()).ReturnsAsync(users);
            _mapperMock.Setup(x => x.Map<List<UserDto>>(users)).Returns(userDtos);

            // Act
            var result = await _userService.GetAllUsersDtoAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userDtos.Count, result.Count);
            for (int i = 0; i < userDtos.Count; i++)
            {
                Assert.AreEqual(userDtos[i].Id, result[i].Id);
                Assert.AreEqual(userDtos[i].Name, result[i].Name);
                Assert.AreEqual(userDtos[i].Surname, result[i].Surname);
                Assert.AreEqual(userDtos[i].EmailAddress, result[i].EmailAddress);
                Assert.AreEqual(userDtos[i].UserTypeId, result[i].UserTypeId);
                Assert.AreEqual(userDtos[i].UserTitleId, result[i].UserTitleId);
            }
        }

        [TestMethod]
        public async Task GetUserById_ReturnsUserDto()
        {
            // Arrange
            var expectedUser = new User { Id = 1, Name = "Thanasis", Surname = "Athanasiou" };
            var userDto = new UserDto { Id = 1, Name = "Thanasis", Surname = "Athanasiou" };

            _userRepositoryMock.Setup(x => x.GetUserByIdAsync(1)).ReturnsAsync(expectedUser);
            _mapperMock.Setup(x => x.Map<UserDto>(expectedUser)).Returns(userDto);

            // Act
            var result = await _userService.GetUserDtoByIdAsync(1);

            // Assert TODO FluentAssertions
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedUser.Id, result.Id);
            Assert.AreEqual(expectedUser.Name, result.Name);
            Assert.AreEqual(expectedUser.Surname, result.Surname);
            _userRepositoryMock.Verify(x => x.GetUserByIdAsync(It.IsAny<int>()), Times.Once);
            _userRepositoryMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task GetUserDtoByIdAsync_WithInvalidId_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var userId = -1;

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => _userService.GetUserDtoByIdAsync(userId));
        }

        [TestMethod]
        public async Task PutUserDtoAsync_UpdatesUser()
        {
            // Arrange
            var userDto = new UserDto { Id = 1, Name = "NewName", Surname = "NewSurname" };
            var expectedUser = new User { Id = 1, Name = "NewName", Surname = "NewSurname" };

            _mapperMock.Setup(m => m.Map<User>(userDto)).Returns(expectedUser);
            _userRepositoryMock.Setup(r => r.PutUserAsync(1, expectedUser));

            // Act
            await _userService.PutUserDtoAsync(userDto.Id, userDto);

            // Assert
            _userRepositoryMock.Verify(r => r.PutUserAsync(1, expectedUser), Times.Once);
        }

        [TestMethod]
        public async Task AddNewUserDtoAsync_WhenValidUserDtoProvided_ShouldAddUserToDatabase()
        {
            // Arrange

            var userDto = new UserDto
            {
                UserTypeId = 1,
                UserTitleId = 2,
                Name = "OneName",
                Surname = "OneSurname",
                EmailAddress = "example@example.com"
            };

            var user = new User
            {
                Id = 1,
                UserTypeId = 1,
                UserTitleId = 2,
                Name = "OneName",
                Surname = "OneSurname",
                EmailAddress = "example@example.com"
            };

            _mapperMock.Setup(x => x.Map<User>(userDto)).Returns(user);

            // Act
            await _userService.AddNewUserDtoAsync(userDto);

            // Assert
            _userRepositoryMock.Verify(x => x.AddNewUserAsync(user), Times.Once);
        }

        [TestMethod]
        public async Task AddNewUserDtoAsync_WhenUserDtoIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            UserDto userDto = null!;

            // Act + Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _userService.AddNewUserDtoAsync(userDto));
        }

        [TestMethod]
        public async Task AddNewUserDtoAsync_WhenUserTypeIdIsInvalid_ShouldThrowArgumentNullException()
        {
            // Arrange
            var userDto = new UserDto
            {
                UserTypeId = 0,
                UserTitleId = 1,
                Name = "OneName",
                Surname = "OneSurname",
                EmailAddress = "example@example.com"
            };

            // Act + Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _userService.AddNewUserDtoAsync(userDto));
        }

        [TestMethod]
        public async Task AddNewUserDtoAsync_WhenUserTitleIdIsInvalid_ShouldThrowArgumentNullException()
        {
            // Arrange
            var userDto = new UserDto
            {
                UserTypeId = 1,
                UserTitleId = 0,
                Name = "OneName",
                Surname = "OneSurname",
                EmailAddress = "example@example.com"
            };

            // Act + Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _userService.AddNewUserDtoAsync(userDto));
        }

        [TestMethod]
        public async Task AddNewUserDtoAsync_WhenNameOrSurnameLengthIsGreaterThan20_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var userDto = new UserDto
            {
                UserTypeId = 1,
                UserTitleId = 1,
                Name = "Johnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnn",
                Surname = "Doe",
                EmailAddress = "johndoe@example.com"
            };

            // Act + Assert
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => _userService.AddNewUserDtoAsync(userDto));
        }

        [TestMethod]
        public async Task AddNewUserDtoAsync_WhenEmailAddressLengthIsGreaterThan50_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var userDto = new UserDto
            {
                UserTypeId = 1,
                UserTitleId = 1,
                Name = "Joh",
                Surname = "Doe",
                EmailAddress = "johndoeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee" +
                "eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee" +
                "eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee@example.com"
            };

            // Act + Assert
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => _userService.AddNewUserDtoAsync(userDto));
        }

        [TestMethod]
        public async Task DeleteUserAsync_ValidId_DeletesUser()
        {
            // Arrange
            int userId = 1;
            _userRepositoryMock.Setup(x => x.DeleteUserAsync(userId));

            // Act
            await _userService.DeleteUserAsync(userId);

            // Assert
            _userRepositoryMock.Verify(x => x.DeleteUserAsync(userId), Times.Once);
        }

        [TestMethod]
        public async Task DeleteUserAsync_InvalidId_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            int invalidUserId = 0;

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => _userService.DeleteUserAsync(invalidUserId));
        }
    }
}