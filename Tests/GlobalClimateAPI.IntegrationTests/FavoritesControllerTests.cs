using Core.Domain.Msgs;
using GlobalClimateAPI.IntegrationTests.Models;
using GlobalClimateAPI.Requests;
using GlobalClimateAPI.Responses;
using GlobalClimateAPI.Responses.Base;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using System.Diagnostics.Metrics;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace GlobalClimateAPI.IntegrationTests;

[TestFixture]
public class FavoritesControllerTests
{
    private WebApplicationFactory<Program> _factory;
    private HttpClient _client;
    private string token;
    private readonly string userName = "GustavoTest";
    private readonly string password = "StrongPassoword123//";
    private readonly string favoritesUrl = "http://localhost:5233/api/Favorites/";
    private static readonly HttpClient _externalClient = new();

    [SetUp]
    public async Task SetUp()
    {
        _factory = new WebApplicationFactory<Program>();
        _client = _factory.CreateClient();
        token = await GetJWTToken();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    [Test]
    public async Task GetFavoriteCitiesByUserId_ReturnsOkResult_WithListOfCities()
    {
        //Arrange and Act
        var response = await _client.GetAsync(favoritesUrl + "GetFavoriteCitiesByUserId?userId=1");
        string responseContent = await response.Content.ReadAsStringAsync();

        var citiesResponse = JsonSerializer.Deserialize<GetFavoriteCitiesResponse>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(citiesResponse?.Cities, Is.Not.Empty);
    }

    [Test]
    public async Task GetFavoriteCountriesByUserId_ReturnsOkResult_WithListOfCountries()
    {
        //Arrange and Act
        var response = await _client.GetAsync(favoritesUrl + "GetFavoriteCountriesByUserId?userId=1");
        string responseContent = await response.Content.ReadAsStringAsync();

        var citiesResponse = JsonSerializer.Deserialize<GetFavoriteCountriesResponse>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(citiesResponse?.Countries, Is.Not.Empty);
    }

    [Test]
    public async Task AddFavoriteCityByUserId_ReturnsOkResults()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Files", "city_names.json");
        string citiesJson = File.ReadAllText(path);
        List<string> cities = JsonSerializer.Deserialize<List<string>>(citiesJson);
        Random random = new Random();
        int index = random.Next(cities.Count);
        string randomCity = cities[index];

        //arrange
        CityFavoriteRequest request = new CityFavoriteRequest() { CityName = randomCity, UserId = "1" };

        //act
        var respose = await _client.PostAsJsonAsync(favoritesUrl + "AddFavoriteCityByUserId", request);
        var content = await respose.Content.ReadAsStringAsync();

        var baseResponse = JsonSerializer.Deserialize<BaseResponse>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        //assert
        Assert.That(respose.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(baseResponse.Message, Is.EqualTo(ApiMsgs.INF005));

    }

    [Test]
    public async Task AddFavoriteCountryByUserId_ReturnsOkResults()
    {
        var response = await _externalClient.GetFromJsonAsync<List<Country>>("https://restcountries.com/v3.1/all");

        var random = new Random();
        var randomCountry = response[random.Next(response.Count)];

        //arrange
        CountryFavoriteRequest request = new CountryFavoriteRequest() { CountryName = randomCountry.Name.Common, UserId = "1" };

        //act
        var respose = await _client.PostAsJsonAsync(favoritesUrl + "AddFavoriteCountryByUserId", request);
        var content = await respose.Content.ReadAsStringAsync();

        var baseResponse = JsonSerializer.Deserialize<BaseResponse>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        //assert
        Assert.That(respose.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(baseResponse.Message, Is.EqualTo(ApiMsgs.INF007));
    }

    [Test]


    [TearDown]
    public void TearDown()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    private async Task<string> GetJWTToken()
    {
        LoginRequest request = new LoginRequest() { UserName = userName, Password = password };

        var response = await _client.PostAsJsonAsync("/api/Auth", request);

        var rawJson = await response.Content.ReadAsStringAsync();
        var json = JsonDocument.Parse(rawJson);
        return json.RootElement.GetProperty("token").GetString()!;
    }
}