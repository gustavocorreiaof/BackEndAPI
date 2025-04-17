namespace BackEndChellenge.API.IntegrationTests;

[TestFixture]
public class APIControllerIntegrationTests
{
    /*private WebApplicationFactory<Program> _factory;
    private HttpClient _client;
    private string InsertUserURL = "/User/InsertUser";

    [SetUp]
    public void Setup()
    {
        _factory = new WebApplicationFactory<Program>();
        _client = _factory.CreateClient();
    }

    [TearDown]
    public void TearDown()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    #region InsertUser Tests

    #region Validate Request Tests
    [Test]
    public async Task InsertUser_InvalidTaxNumber_ReturnsBadRequest()
    {
        CreateUserRequest request = new CreateUserRequest
        {
            Name = "Maria",
            Password = "Password123!",
            TaxNumber = "12345678900",
            Email = "maria@email.com",
            UserType = UserType.CPF
        };

        var response = await _client.PostAsJsonAsync(InsertUserURL, request);

        var body = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

        Assert.NotNull(body);
        Assert.IsTrue(body.Errors.ContainsKey("TaxNumber"));    
        
        var actualMessage = body.Errors["TaxNumber"].First();
        Assert.AreEqual(RequestMsg.ERR006, actualMessage);
    }

    [Test]
    public async Task InsertUser_WhenTaxNumberIsNull_ReturnsBadRequest()
    {
        CreateUserRequest request = new CreateUserRequest()
        {
            Name = "Maryn",
            Email = "exemple1@gmail.com",
            Password = "Password123!",
            TaxNumber = null
        };

        var response = await _client.PostAsJsonAsync(InsertUserURL, request);

        var body = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

        Assert.NotNull(body);
        Assert.IsTrue(body.Errors.ContainsKey("TaxNumber"));
        Assert.That(body.Errors["TaxNumber"], Does.Contain("The TaxNumber field is required."));
    }

    [Test]
    public async Task InsertUser_WhenNameIsNull_ReturnsBadRequest()
    {
        CreateUserRequest request = new CreateUserRequest()
        {
            Name = null,
            Email = "exemple1@gmail.com",
            Password = "Password123!",
            TaxNumber = "78476815000139"
        };

        var response = await _client.PostAsJsonAsync(InsertUserURL, request);

        var body = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

        Assert.NotNull(body);
        Assert.IsTrue(body.Errors.ContainsKey("Name"));
        Assert.That(body.Errors["Name"], Does.Contain("The Name is required."));
    }

    [Test]
    public async Task InsertUser_WhenEmailIsNull_ReturnsBadRequest()
    {
        CreateUserRequest request = new CreateUserRequest()
        {
            Name = "Test",
            Email = null,
            Password = "Password123!",
            TaxNumber = "78476815000139"
        };

        var response = await _client.PostAsJsonAsync(InsertUserURL, request);

        var body = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

        Assert.NotNull(body);
        Assert.IsTrue(body.Errors.ContainsKey("Email"));
        Assert.That(body.Errors["Email"].Contains("The Email is required."));
    }

    [Test]
    public async Task InsertUser_WhenPasswordIsInvalid_ReturnsBadRequest()
    {
        CreateUserRequest request = new CreateUserRequest()
        {
            Name = "Test",
            Email = "exemple1@gmail.com",
            Password = "weakpassword",
            TaxNumber = "78476815000139"
        };

        var response = await _client.PostAsJsonAsync(InsertUserURL, request);

        var body = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

        Assert.NotNull(body);
        Assert.IsTrue(body.Errors.ContainsKey("Password"));
        Assert.That(body.Errors["Password"].Contains("The password must contain at least 6 characters, including uppercase and lowercase letters, numbers, and a special character."));
    }
    #endregion
    
    [Test]
    public async Task InsertUser_WhenTryInserUserWithUsedTaxNumber_ReturnsBadRequest()
    {
        CreateUserRequest request = new CreateUserRequest()
        {
            Name = "Test",
            Email = "exemple1@gmail.com",
            Password = "Password123!",
            TaxNumber = "10041424000158"
        };

        var response = await _client.PostAsJsonAsync(InsertUserURL, request);

        var body = await response.Content.ReadFromJsonAsync<ErrorResponse>();

        Assert.NotNull(body);
        Assert.That(body.Message.Equals(ApiMsg.EX002));
    }

    [Test]
    public async Task InsertUser_WhenTryInserUserWithUsedEmail_ReturnsBadRequest()
    {
        CreateUserRequest request = new CreateUserRequest()
        {
            Name = "Test",
            Email = "miras@example.com",
            Password = "Password123!",
            TaxNumber = "84542889000129"
        };

        var response = await _client.PostAsJsonAsync(InsertUserURL, request);

        var body = await response.Content.ReadFromJsonAsync<ErrorResponse>();

        Assert.NotNull(body);
        Assert.That(body.Message.Equals(ApiMsg.EX003));
    }
    #endregion*/
}