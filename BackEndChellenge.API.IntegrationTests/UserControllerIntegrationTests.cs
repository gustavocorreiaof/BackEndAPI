using BackEndChellengeAPI.Controllers;
using BackEndChellengeAPI.Requests;
using BackEndChellengeAPI.Responses;
using Core.Domain.Entities;
using Core.Domain.Enums;
using Core.Domain.Exceptions;
using Core.Domain.Msgs;
using Core.Infrastructure.Repository.Base;
using Core.Infrastructure.Repository.Interfaces;
using Core.Infrastructure.Util;
using Core.Services.BusinesseRules;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
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

            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetService<AppDbContext>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            db.User.AddRange(ReturnListOfUsers());
            db.SaveChanges();
        }

        [Test]
        public async Task GetAllUsers_ReturnsOkResult_WithListOfUsers()
        {
            //Arrange and Act
            var response = await _client.GetAsync(inserUserUrl + "GetAllUsers");
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
        public async Task GetById_ReturnsUser()
        {
            //Arrange & Act
            var response = await _client.GetAsync("/User/GetById?id=" + 5);
            var content = await response.Content.ReadAsStringAsync();

            ApiResponse<User> apiResponse = JsonSerializer.Deserialize<ApiResponse<User>>(content, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.True(apiResponse?.Data != null);
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
                Id = 1,
                Email = usedEmail
            };

            _mockUserRepository
                .Setup(service => service
                .GetByEmail(usedEmail)).Returns(user);

            CreateUserRequest request = new()
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

        [Test]
        [TestCase("NewName", "NewEmail@gmail.com", "NewPassword123@@", "70.961.879/0001-13", 1)]
        public async Task UpdateUser_WhenSuccessfully(string newName, string newEmail, string newPassword, string newTaxNumber, long userId)
        {
            UpdateUserRequest updateUserRequest = new UpdateUserRequest()
            { 
                NewEmail = newEmail, 
                NewPassword = newPassword, 
                NewTaxNumber = newTaxNumber, 
                UserId = userId, 
                NewName = newName 
            };

            await _client.PutAsJsonAsync("/User/", updateUserRequest);

            var response = await _client.GetAsync("/User/GetById?id=" + userId);
            var responseContent = await response.Content.ReadAsStringAsync();
            
            ApiResponse<User> apiResponse = JsonSerializer.Deserialize<ApiResponse<User>>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;

            Assert.That(apiResponse.Data.Name, Is.EqualTo(newName));
            Assert.That(apiResponse.Data.Email, Is.EqualTo(newEmail)); 
            Assert.That(BCrypt.Net.BCrypt.Verify(newPassword, apiResponse.Data.Password), Is.True);
            Assert.That(apiResponse.Data.TaxNumber, Is.EqualTo(Util.RemoveSpecialCharacters(newTaxNumber)));
        }

        [Test]
        public async Task UpdateUserName_WhenUpdateUserNameSuccessfully()
        {
            long userId = 5;

            var initialResponse = await _client.GetAsync("/User/GetById?id=" + userId);
            var initalContent = await initialResponse.Content.ReadAsStringAsync();
            ApiResponse<User> initialState = JsonSerializer.Deserialize<ApiResponse<User>>(initalContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

            string newName = "NewName";

            PatchUpdateRequest patchUpdateRequest = new() { Value = newName, UserId = userId};

            var updateResponse = await _client.PatchAsJsonAsync("/User/UpdateName", patchUpdateRequest);
            var updateRequestContent = await updateResponse.Content.ReadAsStringAsync();

            var finalResponse = await _client.GetAsync("/User/GetById?id=" + userId);
            var finalContent = await finalResponse.Content.ReadAsStringAsync();
            ApiResponse<User> finalState = JsonSerializer.Deserialize<ApiResponse<User>>(finalContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;


            Assert.That(finalState.Data.Name, Is.EqualTo(newName));
            Assert.That(initialState.Data.UpdateDate, !Is.EqualTo(finalState.Data.UpdateDate));
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }

        private async Task<string> GetJWTToken()
        {
            var response = await _client.PostAsync("/api/Auth", null);

            var rawJson = await response.Content.ReadAsStringAsync();
            var json = JsonDocument.Parse(rawJson);
            return json.RootElement.GetProperty("token").GetString()!;
        }

        private List<User> ReturnListOfUsers()
        {
            return new List<User>
            {
                new User { Id = 1, Name = "Gustavo DEV", Password = "Password123!", TaxNumber = "10041424000158", Email = "gustavocorreiadias.dev2@gmail.com", Type = UserType.CNPJ, CreationDate = new DateTime(2025, 4, 5, 0, 58, 52, 877) },
                new User { Id = 2, Name = "Gustavo Email Oficial", Password = "Password123!", TaxNumber = "07779288099", Email = "zerokller45@gmail.com", Type = UserType.CPF, CreationDate = new DateTime(2025, 4, 5, 0, 59, 5, 197) },
                new User { Id = 3, Name = "Maria", Password = "senha123", TaxNumber = "11122233344", Email = "maria@email.com", Type = UserType.CPF, CreationDate = new DateTime(2025, 4, 6, 19, 12, 41, 573) },
                new User { Id = 4, Name = "Test", Password = "Password123!", TaxNumber = "62822624000141", Email = "miras@example.com", Type = UserType.CNPJ, CreationDate = new DateTime(2025, 4, 17, 12, 42, 6, 570) },
                new User { Id = 5, Name = "Alessandro Ranio", Password = "HashiramaYMadara123@", TaxNumber = "95310729000170", Email = "gustavocorreiadias.dev@gmail.com", Type = UserType.CNPJ, CreationDate = new DateTime(2025, 4, 17, 19, 19, 50, 547) },
                new User { Id = 6, Name = "Ronaldinho", Password = "Biscoitinho@chocolate2025", TaxNumber = "94766715055", Email = "alekseixavier9@gmail.com", Type = UserType.CPF, CreationDate = new DateTime(2025, 4, 17, 19, 23, 23, 573) },
                new User { Id = 7, Name = "Lais", Password = "$2a$11$BDquXP/v7c0wCnT21TIiJuud/BcghrCgd90wsJOroHDsSL7MPsNx6", TaxNumber = "67816443000126", Email = "laiscavalcantedeoliveira@gmail.com", Type = UserType.CNPJ, CreationDate = new DateTime(2025, 4, 30, 23, 21, 52, 490) },
                new User { Id = 9, Name = "Andrezao", Password = "$2a$11$XzZiPYAIUmf/bh5JieYdf.mPFGcVUO6rMf1NzmfrcIjDxhpnzEAlq", TaxNumber = "05698249000136", Email = "andrefarias389@gmail.com", Type = UserType.CNPJ, CreationDate = new DateTime(2025, 5, 2, 16, 44, 54, 273) },
                new User { Id = 18, Name = "Gustavo", Password = "$2a$11$UZ0LgyvHhus5aO4ePhTy7.kgKKoV2XfKEoEoKBB8JtrloJo.OdoyC", TaxNumber = "59099742000169", Email = "gocorreia@email.com", Type = UserType.CNPJ, CreationDate = new DateTime(2025, 5, 3, 14, 41, 48, 40) }
            };
        }
    }
}
