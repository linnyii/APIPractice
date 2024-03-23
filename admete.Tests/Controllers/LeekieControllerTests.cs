using admete.Controllers;
using admete.Enums;
using admete.MockedDatabase;
using admete.Models;
using admete.Repos;
using admete.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace admete.Tests.Controllers;

[TestFixture]
public class LeekieControllerTests
{
    private ILeekieService _leekieService;
    private LeekieController _leekieController;

    [SetUp]
    public void SetUp()
    {
        _leekieService = Substitute.For<ILeekieService>();
        _leekieController = new LeekieController(_leekieService);
    }

    [Test]
    public void Get_SicBo_Result()
    {
        var expected = new List<GameResultDto>
        {
            new()
            {
                ProductType = 5, GameId = 103, StationName = "Sic Bo B", StartTime = DateTime.Now.AddMinutes(10),
                GameDetail = "333"
            }
        };
        _leekieService.GameResults(Arg.Any<Period>(), Arg.Any<int>()).Returns(expected);

        var gameResultRequest = new GameResultRequest
        {
            StartTime = new DateTime(2023, 1, 1),
            EndTime = new DateTime(2024, 1, 2),
            ProductType = 5
        };
        var actual = _leekieController.GameResults(gameResultRequest);

        actual.Value.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void StartDate_Must_Be_Later_Than_20230101()
    {
        var gameResultRequest = new GameResultRequest
        {
            StartTime = new DateTime(2022, 12, 31),
            EndTime = new DateTime(2024, 1, 2),
            ProductType = 5
        };
        var actual = _leekieController.GameResults(gameResultRequest);

        actual.Result.Should().BeOfType<BadRequestObjectResult>()
            .Which.Value.Should().BeEquivalentTo(new ErrorResponse(EnumErrorCode.InvalidStartDate));
    }

    [Test]
    public void EndDate_Must_Be_Greater_Than_StartDate()
    {
        var gameResultRequest = new GameResultRequest
        {
            StartTime = new DateTime(2024, 1, 3),
            EndTime = new DateTime(2024, 1, 2),
            ProductType = 5
        };
        var actual = _leekieController.GameResults(gameResultRequest);

        actual.Result.Should().BeOfType<BadRequestObjectResult>()
            .Which.Value.Should().BeEquivalentTo(new ErrorResponse(EnumErrorCode.InvalidDate));
    }
}