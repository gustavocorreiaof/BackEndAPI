using BackEndChellengeAPI.Controllers;
using BackEndChellengeAPI.Requests;
using Core.Domain.Entities;
using Core.Domain.Exceptions;
using Core.Domain.Msgs;
using Core.Infrastructure.Repository.Interfaces;
using Core.Services.BusinesseRules;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BackEndChellenge.API.IntegrationTests
{
    [TestFixture]
    public class UserControllerIntegrationTests
    {
        private Mock<UserBR> _mockUserBR;
        private Mock<IUserRepository> _mockUserRepository;
        private UserController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockUserBR = new Mock<UserBR>(_mockUserRepository.Object);
            _controller = new UserController(_mockUserBR.Object);
        }

        [Test]
        public void GetAllUsers_ReturnsOkResult_WithListOfUsers()
        {
            // Arrange
            List<User> mockUsers = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com" }
            }; 
            _mockUserRepository.Setup(repo => repo.GetAllUsers()).Returns(mockUsers);

            // Act
            var result = _controller.GetAllUsers();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult, "Expected OkObjectResult but got null.");

            dynamic value = okResult.Value!;
            List<User> users = value.Data;

            Assert.IsNotNull(users, "Expected users list but got null.");
        }

        [Test]
        public void InsertUser_InvalidTaxNumber_ThrowsApiException()
        {
            // Arrange
            var mockUser = new User { Id = 1, TaxNumber = "59099742000169" };
            _mockUserRepository
                .Setup(repo => repo.GetByTaxNumber("59099742000169"))
                .Returns(mockUser);

            var request = new CreateUserRequest
            {
                Name = "Gustavo",
                Email = "gocorreia@email.com",
                TaxNumber = "59099742000169",
                Password = "Picole@123"
            };

            // Act & Assert
            var ex = Assert.Throws<ApiException>(() => _controller.InsertUser(request));
            Assert.AreEqual(ApiMsg.EX002, ex.Message);
        }

        [Test]
        public void InsertUser_WhenTaxNumberIsNull_ReturnsBadRequest()
        {

        }
    }
}
