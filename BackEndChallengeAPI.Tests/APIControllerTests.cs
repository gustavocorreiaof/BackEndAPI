using BackEndChellengeAPI.Controllers;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BackEndChallengeAPI.Tests;

[TestFixture]
public class APIControllerTests
{
    private APIController _controller;

    [SetUp]
    public void Setup()
    {
        _controller = new APIController();
    }

    [Test]
    public void GetAllUsers_ReturnsOkWithUserList()
    {
        // Act
        IActionResult result = _controller.GetAllUsers();

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okResult = result as OkObjectResult;

        Assert.IsInstanceOf<List<User>>(okResult.Value);
        var users = okResult.Value as List<User>;

        Assert.NotNull(users);
    }
}