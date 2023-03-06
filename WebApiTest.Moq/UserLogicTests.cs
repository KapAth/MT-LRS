using Moq;
using Repositories.Interfaces;
using Repositories.Repository.Entities;
using Services;

namespace WebApiTest.Moq
{
    [TestClass]
    public class UserLogicTests
    {
        // TODO there is a TestInitialize to avoid duplicate initialization
        [TestMethod]
        public async Task GetUserById_ReturnsUser()
        {
            // Arrange
            var expectedUser = new User { Id = 1, Name = "Thanasis", Surname = "Kapsimalis"};

            var mockDAL = new Mock<IUser>();
            mockDAL.Setup(x => x.GetUserById(1)).ReturnsAsync(expectedUser);

            var userLogic = new UserLogic { _DAL = mockDAL.Object };

            // Act
            var result = await userLogic.GetUserById(1);

            // Assert TODO FluentAssertions
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedUser.Id, result.Id);
            Assert.AreEqual(expectedUser.Name, result.Name);
            Assert.AreEqual(expectedUser.Surname, result.Surname);
            mockDAL.Verify(x => x.GetUserById(It.IsAny<int>()), Times.Once);
            mockDAL.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task GetUserById_ReturnsNull()
        {
            // Arrange
            var expectedUser = new User { Id = 1, Name = "Thanasis", Surname = "Kapsimalis" };

            var mockDAL = new Mock<IUser>();
            mockDAL.Setup(x => x.GetUserById(1)).ReturnsAsync(expectedUser);

            var userLogic = new UserLogic { _DAL = mockDAL.Object };

            // Act
            var invalidResult = await userLogic.GetUserById(0); ;

            // Assert
            Assert.IsNull(invalidResult);
        }

        [TestMethod]
        public async Task PutUser_ReturnsUpdatedUser()
        {
            // Arrange
            var userToUpdate = new User { Id = 1, Name = "Thanasis", Surname = "Kapsimalis" };
            var updatedUser = new User { Id = 1, Name = "Kwstas", Surname = "Karakwstas" };


            var mockDAL = new Mock<IUser>();
            mockDAL.Setup(x => x.PutUser(1, userToUpdate)).ReturnsAsync(updatedUser);

            var userLogic = new UserLogic { _DAL = mockDAL.Object };

            // Act
            var result = await userLogic.PutUser(1, userToUpdate);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updatedUser.Id, result.Id);
            Assert.AreEqual(updatedUser.Name, result.Name);
            Assert.AreEqual(updatedUser.Surname, result.Surname);
        }


        [TestMethod]
        public async Task AddNewUser_ReturnsTrueWhenUserAdded()
        {
            // Arrange
            var newUser = new User { Name = "George", Surname = "Karageorge" };
            var addedUser = new User { Id = 1, Name = "John", Surname = "Karajohn" };

            var mockDAL = new Mock<IUser>();
            mockDAL.Setup(x => x.AddNewUser(newUser)).ReturnsAsync(addedUser);

            var userLogic = new UserLogic { _DAL = mockDAL.Object };

            // Act
            var result = await userLogic.AddNewUser(newUser);

            // Assert
            Assert.IsTrue(result);
        }


        [TestMethod]
        public async Task AddNewUser_ReturnsFalseWhenUserNotAdded()
        {
            // Arrange
            var newUser = new User { Name = "John", Surname = "Karajohn" };

            var mockDAL = new Mock<IUser>();
            mockDAL.Setup(x => x.AddNewUser(newUser)).ReturnsAsync((User)null);

            var userLogic = new UserLogic { _DAL = mockDAL.Object };

            // Act
            var result = await userLogic.AddNewUser(newUser);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task DeleteUser_ReturnsDeletedUser()
        {
            // Arrange
            var expectedUser = new User { Id = 1, Name = "John", Surname = "Karajohn" };

            var mockDAL = new Mock<IUser>();
            mockDAL.Setup(x => x.DeleteUser(1)).ReturnsAsync(expectedUser);

            var userLogic = new UserLogic { _DAL = mockDAL.Object };

            // Act
            var result = await userLogic.DeleteUser(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedUser.Id, result.Id);
            Assert.AreEqual(expectedUser.Name, result.Name);
            Assert.AreEqual(expectedUser.Surname, result.Surname);
        }
    }
}