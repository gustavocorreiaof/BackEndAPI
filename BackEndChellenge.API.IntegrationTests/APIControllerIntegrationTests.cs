using Core.Entities;
using Core.Requests;
using Core.Util.Msgs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;


namespace BackEndChellenge.API.IntegrationTests;

[TestFixture]
public class APIControllerIntegrationTests
{
    private WebApplicationFactory<Program> _factory;
    private HttpClient _client;

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

    [Test]
    public async Task InsertUser_InvalidCpf_ReturnsBadRequest()
    {
        var request = new CreateUserRequest
        {
            Name = "Maria",
            Password = "Password123!",
            TaxNumber = "12345678900",
            Email = "maria@email.com",
            UserType = UserType.CPF
        };

        var response = await _client.PostAsJsonAsync("/API/InsertUser", request);

        var body = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

        Assert.NotNull(body);
        Assert.IsTrue(body.Errors.ContainsKey("TaxNumber"));    
        
        var actualMessage = body.Errors["TaxNumber"].First();
        Assert.AreEqual(RequestMsg.ERR006, actualMessage);
    }
}