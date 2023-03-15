using Moq;
using Repositories.Repository.Entities;
using Services;
using AutoMapper;
using WebAPI.Models;
using WebAPI.Repositories.Interfaces;
using FluentAssertions;

namespace WebApiTest.Moq
{
    [TestClass]
    public class UsersServiceTests
    {
        //the ! operator after the null keyword is used to tell the compiler that these fields will never be null after they are initialized
        private Mock<IUsersRepository> _userRepositoryMock = null!;

        private Mock<IMapper> _mapperMock = null!;
        private UsersService _userService = null!;

        [TestInitialize]
        public void Initialize()
        {
            _userRepositoryMock = new Mock<IUsersRepository>();
            _mapperMock = new Mock<IMapper>();
            _userService = new UsersService(_userRepositoryMock.Object, _mapperMock.Object);
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
            _userRepositoryMock.Setup(x => x.GetAllUsersAsync(null)).ReturnsAsync(users);
            _mapperMock.Setup(x => x.Map<List<UserDto>>(users)).Returns(userDtos);

            // Act
            var result = await _userService.GetAllUsersDtoAsync();

            // Assert
            result.Should().NotBeNull();
            result.Count.Should().Be(users.Count);
            for (int i = 0; i < userDtos.Count; i++)
            {
                result[i].Id.Should().Be(userDtos[i].Id);
                result[i].Name.Should().Be(userDtos[i].Name);
                result[i].Surname.Should().Be(userDtos[i].Surname);
                result[i].EmailAddress.Should().Be(userDtos[i].EmailAddress);
                result[i].UserTypeId.Should().Be(userDtos[i].UserTypeId);
                result[i].UserTitleId.Should().Be(userDtos[i].UserTitleId);
            }
        }

        [TestMethod]
        public async Task GetAllUsersDtoAsync_UsingFilter_ShouldReturnOneUser()
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
            _userRepositoryMock.Setup(x => x.GetAllUsersAsync("Thanasis")).ReturnsAsync(users);
            _mapperMock.Setup(x => x.Map<List<UserDto>>(users)).Returns(userDtos);

            // Act
            var result = await _userService.GetAllUsersDtoAsync("Thanasis");

            // Assert
            result.Should().NotBeNull();
            result[0].Id.Should().Be(userDtos[0].Id);
            result[0].Name.Should().Be(userDtos[0].Name);
            result[0].Surname.Should().Be(userDtos[0].Surname);
            result[0].EmailAddress.Should().Be(userDtos[0].EmailAddress);
            result[0].UserTypeId.Should().Be(userDtos[0].UserTypeId);
            result[0].UserTitleId.Should().Be(userDtos[0].UserTitleId);
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

            result.Should().NotBeNull();
            result.Id.Should().Be(expectedUser.Id);
            result.Name.Should().Be(expectedUser.Name);
            result.Surname.Should().Be(expectedUser.Surname);
            _userRepositoryMock.Verify(x => x.GetUserByIdAsync(It.IsAny<int>()), Times.Once);
            _userRepositoryMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task GetUserDtoByIdAsync_WithInvalidId_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var Id = -1;

            // Act
            Func<Task> act = () => _userService.GetUserDtoByIdAsync(Id);

            // Assert
            await act.Should().ThrowAsync<ArgumentOutOfRangeException>();
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
            _userRepositoryMock.VerifyNoOtherCalls();
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
            _userRepositoryMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task AddNewUserDtoAsync_WhenUserDtoIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            UserDto userDto = null!;
            // Act
            Func<Task> act = () => _userService.AddNewUserDtoAsync(userDto);
            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
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
            // Act
            Func<Task> act = () => _userService.AddNewUserDtoAsync(userDto);
            // Assert
            await act.Should().ThrowAsync<ArgumentOutOfRangeException>();
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

            // Act
            Func<Task> act = () => _userService.AddNewUserDtoAsync(userDto);

            // Assert
            await act.Should().ThrowAsync<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public async Task AddNewUserDtoAsync_WhenNameLengthIsGreaterThan20_ShouldThrowArgumentOutOfRangeException()
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

            // Act
            Func<Task> act = () => _userService.AddNewUserDtoAsync(userDto);

            // Assert
            await act.Should().ThrowAsync<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public async Task AddNewUserDtoAsync_WhenSurnameLengthIsGreaterThan20_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var userDto = new UserDto
            {
                UserTypeId = 1,
                UserTitleId = 1,
                Name = "John",
                Surname = "Doeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee",
                EmailAddress = "johndoe@example.com"
            };

            // Act
            Func<Task> act = () => _userService.AddNewUserDtoAsync(userDto);

            // Assert
            await act.Should().ThrowAsync<ArgumentOutOfRangeException>();
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

            // Act
            Func<Task> act = () => _userService.AddNewUserDtoAsync(userDto);

            // Assert
            await act.Should().ThrowAsync<ArgumentOutOfRangeException>();
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
            _userRepositoryMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task DeleteUserAsync_InvalidId_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            int invalidUserId = 0;

            // Act
            Func<Task> act = () => _userService.DeleteUserAsync(invalidUserId);

            // Assert
            await act.Should().ThrowAsync<ArgumentOutOfRangeException>();
        }
    }
}