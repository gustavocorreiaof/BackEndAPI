using BackEndChellengeAPI.Controllers;
using BackEndChellengeAPI.Requests;
using BackEndChellengeAPI.Responses;
using Core.Domain.Entities;
using Core.Domain.Exceptions;
using Core.Domain.Msgs;
using Core.Infrastructure.Repository.Interfaces;
using Core.Services.BusinesseRules;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace BackEndChellenge.API.IntegrationTests
{
    [TestFixture]
    public class UserControllerIntegrationTests
    {
        private Mock<UserBR> _mockUserBR;
        private Mock<IUserRepository> _mockUserRepository;
        private UserController _controller;
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;
        private string token;
        private const string inserUserUrl = "/User/";

        [SetUp]
        public async Task SetUp()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockUserBR = new Mock<UserBR>(_mockUserRepository.Object);
            _controller = new UserController(_mockUserBR.Object);
            
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
            token = await GetJWTToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }

        [Test]
        public async Task GetAllUsers_ReturnsOkResult_WithListOfUsers()
        {
            //Arrange and Act
            var response = await _client.GetAsync(inserUserUrl);
            string responseContent = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<User>>>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(apiResponse?.Data, Is.Not.Empty);
        }

        [Test]
        public async Task InsertUser_InvalidTaxNumber_ReturnsBadRequest()
        {
            //Arrange
            CreateUserRequest request = new()
            {
                Name = "Exemple",
                Email = "exemple@mail.com",
                Password = "password",
                TaxNumber = "123"
            };

            //Act
            var response = await _client.PostAsJsonAsync(inserUserUrl, request);
            var errosInResponse = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.IsTrue(errosInResponse.Contains(RequestMsg.ERR006), "Its expected errons about Taxnumber field but there is no have.");
        }

        [Test]
        public async Task InsertUser_WhenTaxNumberIsNull_ReturnsBadRequest()
        {
            //Arrange
            CreateUserRequest request = new CreateUserRequest()
            {
                Name = "Gustavo",
                Email = "gocorreia@email.com",
                TaxNumber = null!,
                Password = "Picole@123"
            };

            //Act
            var response = await _client.PostAsJsonAsync(inserUserUrl, request);
            var error = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.IsTrue(error.Contains("TaxNumber"), "Erros were expected for the TaxNumber field.");
        }

        [Test]
        public async Task InsertUser_WhenNameIsNull_ReturnsBadRequest()
        {
            //Arrange
            CreateUserRequest request = new CreateUserRequest()
            {
                Name = null!,
                Email = "exemplo@exmail.com",
                Password = "dfjbnjknKOLKN@123",
                TaxNumber = "49.633.621/0001-00",
            };

            //Act
            var response = await _client.PostAsJsonAsync(inserUserUrl, request);
            var errosInResponse = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.IsTrue(errosInResponse.Contains(RequestMsg.ERR001), "Erros were expected to Name field.");
        }

        [Test]
        public async Task InsertUser_WhenEmailIsNull_ReturnsBadRequest()
        {
            //Arrange
            CreateUserRequest request = new CreateUserRequest()
            {
                Name = "Miras",
                Email = null!,
                Password = "sdsdfbKJAS9q89(*#(",
                TaxNumber = "05.815.126/0001-38"
            };

            //Act
            var response = await _client.PostAsJsonAsync(inserUserUrl, request);
            var errosInResponse = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.IsTrue(errosInResponse.Contains(RequestMsg.ERR002), "Its expected erros about Email field.");
        }

        [Test]
        [TestCase("123")]
        public async Task InsertUser_WhenPasswordIsInvalid_ReturnsBadRequest(string invalidPassword)
        {
            //Arrange
            CreateUserRequest request = new()
            {
                Name = "Exemple",
                Email = "email@gmail.com",
                Password = invalidPassword,
                TaxNumber = "05.815.126/0001-38"
            };

            //Act
            var response = await _client.PostAsJsonAsync(inserUserUrl, request);
            var responseErros = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.IsTrue(responseErros.Contains(RequestMsg.ERR009), "Its expected erros about Password field.");
        }

        [Test]
        [TestCase("05815126000138")]
        public void InsertUser_WhenTryInserUserWithUsedTaxNumber_ThrowException(string usedTaxnumber)
        {
            // Arrange
            var mockUser = new User { Id = 1, TaxNumber = usedTaxnumber };
            _mockUserRepository
                .Setup(repo => repo.GetByTaxNumber(usedTaxnumber))
                .Returns(mockUser);

            var request = new CreateUserRequest
            {
                Name = "Gustavo",
                Email = "gocorreia@email.com",
                TaxNumber = usedTaxnumber,
                Password = "Picole@123"
            };

            // Act & Assert
            var ex = Assert.Throws<ApiException>(() => _controller.InsertUser(request));
            Assert.That(ex.Message, Is.EqualTo(ApiMsg.EX002));
        }

        [Test]
        [TestCase("miras@gmail.com")]
        public void InsertUser_WhenTryInserUserWithUsedEmail_ThrowException(string usedEmail)
        {
            //Arrange
            User user = new User()
            {
                Id = 1, Email = usedEmail
            };

            _mockUserRepository
                .Setup(service => service
                .GetByEmail(usedEmail)).Returns(user);

            CreateUserRequest request = new ()
            {
                Name = "Exemple",
                Email = usedEmail,
                Password = "lsdkfLKMJ213@#@#",
                TaxNumber = "05815126000138"
            };

            // Act & Assert
            var ex = Assert.Throws<ApiException>(() => _controller.InsertUser(request));
            Assert.That(ex.Message, Is.EqualTo(ApiMsg.EX003));
        }

        private async Task<string> GetJWTToken()
        {
            var response = await _client.PostAsync("/api/Auth", null);

            var rawJson = await response.Content.ReadAsStringAsync();
            var json = JsonDocument.Parse(rawJson);
            return json.RootElement.GetProperty("token").GetString()!;
        }
    }
}
