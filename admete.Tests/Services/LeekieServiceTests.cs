using admete.MockedDatabase;
using admete.Models;
using admete.Repos;
using FluentAssertions;
using Microsoft.Extensions.Time.Testing;
using NSubstitute;

namespace admete.Tests.Services;

[TestFixture]
public class LeekieServiceTests
{
    private IDatabaseService _databaseService = Substitute.For<IDatabaseService>();
    private FakeTimeProvider _fakeTimeProvider;
    private LeekieService _leekieService;

    [SetUp]
    public void SetUp()
    {
        _databaseService = Substitute.For<IDatabaseService>();
        _fakeTimeProvider = new FakeTimeProvider();
        _leekieService = new LeekieService(_databaseService, _fakeTimeProvider);

        GivenMainGameResults();
        Given2023GameResults();
        Given2024GameResults();
        _fakeTimeProvider.SetUtcNow(DateTime.Parse("2024-03-08"));
    }

    [Test]
    public void Given_2023_GameResults_Only_Get_2023_GameResults()
    {
        var expected = new List<GameResultDto>()
        {
            new()
            {
                ProductType = 5, GameId = 1, StationName = "Sic Bo A",
                StartTime = new DateTime(2023, 3, 2),
                GameDetail = "111"
            }
        };
        Given2023GameResults(expected);

        var actual = _leekieService.GameResults(new Period()
        {
            Start = new DateTime(2023, 3, 1),
            End = new DateTime(2023, 3, 3)
        }, 5);

        actual.Should().BeEquivalentTo(expected);
        _databaseService.GetClient("SBO2023").Received(1).GetGameResults();
        _databaseService.GetClient("SBO2024").DidNotReceive().GetGameResults();
        _databaseService.GetClient("LiveCasinoMain").DidNotReceive().GetGameResults();
    }

    [Test]
    public void Given_2024_GameResults_Only_Get_2024_GameResults()
    {
        _fakeTimeProvider.SetUtcNow(DateTime.Parse("2024-06-01"));

        var expected = new List<GameResultDto>()
        {
            new()
            {
                ProductType = 5, GameId = 1, StationName = "Sic Bo A",
                StartTime = new DateTime(2024, 3, 2),
                GameDetail = "111"
            }
        };
        Given2024GameResults(expected);

        var actual = _leekieService.GameResults(new Period()
        {
            Start = new DateTime(2024, 3, 1),
            End = new DateTime(2024, 3, 3)
        }, 5);

        actual.Should().BeEquivalentTo(expected);
        _databaseService.GetClient("SBO2024").Received(1).GetGameResults();
        _databaseService.GetClient("SBO2023").DidNotReceive().GetGameResults();
        _databaseService.GetClient("LiveCasinoMain").DidNotReceive().GetGameResults();
    }

    [Test]
    public void Given_7Days_Before_GameResults_Only_Get_Main_GameResults()
    {
        var expected = new List<GameResultDto>()
        {
            new()
            {
                ProductType = 5, GameId = 1, StationName = "Sic Bo A",
                StartTime = new DateTime(2024, 3, 1),
                GameDetail = "111"
            }
        };
        GivenMainGameResults(expected);

        var actual = _leekieService.GameResults(new Period()
        {
            Start = new DateTime(2024, 3, 1),
            End = new DateTime(2024, 3, 3)
        }, 5);

        actual.Should().BeEquivalentTo(expected);
        _databaseService.GetClient("LiveCasinoMain").Received(1).GetGameResults();
        _databaseService.GetClient("SBO2023").DidNotReceive().GetGameResults();
        _databaseService.GetClient("SBO2024").DidNotReceive().GetGameResults();
    }

    [Test]
    public void Get_GameResults_By_Period()
    {
        GivenMainGameResults(
            new GameResultDto
            {
                ProductType = 5, GameId = 1, StationName = "Sic Bo A", GameDetail = "111",
                StartTime = new DateTime(2024, 3, 1)
            },
            new GameResultDto
            {
                ProductType = 5, GameId = 1, StationName = "Sic Bo A", GameDetail = "111",
                StartTime = new DateTime(2024, 3, 5)
            }
        );
        Given2024GameResults(
            new GameResultDto
            {
                ProductType = 5, GameId = 1, StationName = "Sic Bo A", GameDetail = "111",
                StartTime = new DateTime(2024, 2, 5)
            }
        );
        Given2023GameResults(
            new GameResultDto
            {
                ProductType = 5, GameId = 1, StationName = "Sic Bo A", GameDetail = "111",
                StartTime = new DateTime(2023, 11, 5)
            },
            new GameResultDto
            {
                ProductType = 5, GameId = 1, StationName = "Sic Bo A", GameDetail = "111",
                StartTime = new DateTime(2023, 10, 5)
            }
        );

        var actual = _leekieService.GameResults(new Period()
        {
            Start = new DateTime(2023, 10, 6),
            End = new DateTime(2024, 3, 3)
        }, 5);

        actual.Should().BeEquivalentTo(new List<GameResultDto>()
        {
            new()
            {
                ProductType = 5, GameId = 1, StationName = "Sic Bo A", GameDetail = "111",
                StartTime = new DateTime(2024, 3, 1)
            },
            new()
            {
                ProductType = 5, GameId = 1, StationName = "Sic Bo A", GameDetail = "111",
                StartTime = new DateTime(2024, 2, 5)
            },
            new()
            {
                ProductType = 5, GameId = 1, StationName = "Sic Bo A", GameDetail = "111",
                StartTime = new DateTime(2023, 11, 5)
            },
        });
        _databaseService.GetClient("LiveCasinoMain").Received(1).GetGameResults();
        _databaseService.GetClient("SBO2024").Received(1).GetGameResults();
        _databaseService.GetClient("SBO2023").Received(1).GetGameResults();
    }

    [Test]
    public void Get_GameResults_By_ProductType()
    {
        GivenMainGameResults(
            new GameResultDto
            {
                ProductType = 5, GameId = 1, StationName = "Sic Bo A", GameDetail = "111",
                StartTime = new DateTime(2024, 3, 1)
            },
            new GameResultDto
            {
                ProductType = 5, GameId = 102, StationName = "Sic Bo A", GameDetail = "444",
                StartTime = new DateTime(2024, 3, 1)
            },
            new GameResultDto
            {
                ProductType = 1, GameId = 105, StationName = "Baccarat A", GameDetail = "D13;H6;;H1;C13;H10;",
                StartTime = new DateTime(2024, 3, 1)
            }
        );

        var actual = _leekieService.GameResults(new Period()
        {
            Start = new DateTime(2024, 2, 6),
            End = new DateTime(2024, 3, 3)
        }, 5);

        actual.Should().BeEquivalentTo(new List<GameResultDto>()
        {
            new GameResultDto
            {
                ProductType = 5, GameId = 1, StationName = "Sic Bo A", GameDetail = "111",
                StartTime = new DateTime(2024, 3, 1)
            },
            new GameResultDto
            {
                ProductType = 5, GameId = 102, StationName = "Sic Bo A", GameDetail = "444",
                StartTime = new DateTime(2024, 3, 1)
            },
        });
    }

    [Test]
    public void Get_GameResults_Sort_By_StartTime()
    {
        GivenMainGameResults(
            new GameResultDto
            {
                ProductType = 5, GameId = 1, StationName = "Sic Bo A", GameDetail = "111",
                StartTime = new DateTime(2024, 3, 3)
            },
            new GameResultDto
            {
                ProductType = 5, GameId = 1, StationName = "Sic Bo A", GameDetail = "111",
                StartTime = new DateTime(2024, 3, 2)
            },
            new GameResultDto
            {
                ProductType = 5, GameId = 1, StationName = "Sic Bo A", GameDetail = "111",
                StartTime = new DateTime(2024, 3, 1)
            }
        );

        var actual = _leekieService.GameResults(new Period()
        {
            Start = new DateTime(2024, 2, 6),
            End = new DateTime(2024, 3, 4)
        }, 5);

        actual.Should().BeEquivalentTo(new List<GameResultDto>()
        {
            new GameResultDto
            {
                ProductType = 5, GameId = 1, StationName = "Sic Bo A", GameDetail = "111",
                StartTime = new DateTime(2024, 3, 1)
            },
            new GameResultDto
            {
                ProductType = 5, GameId = 1, StationName = "Sic Bo A", GameDetail = "111",
                StartTime = new DateTime(2024, 3, 2)
            },
            new GameResultDto
            {
                ProductType = 5, GameId = 1, StationName = "Sic Bo A", GameDetail = "111",
                StartTime = new DateTime(2024, 3, 3)
            }
        }, c => c.WithStrictOrdering());
    }

    [Test]
    public void Get_GameResults_Sort_By_StartTime_And_GameId()
    {
        GivenMainGameResults(
            new GameResultDto
            {
                ProductType = 5, GameId = 2, StationName = "Sic Bo A", GameDetail = "111",
                StartTime = new DateTime(2024, 3, 1)
            },
            new GameResultDto
            {
                ProductType = 5, GameId = 1, StationName = "Sic Bo A", GameDetail = "111",
                StartTime = new DateTime(2024, 3, 1)
            }
        );

        var actual = _leekieService.GameResults(new Period()
        {
            Start = new DateTime(2024, 2, 6),
            End = new DateTime(2024, 3, 4)
        }, 5);

        actual.Should().BeEquivalentTo(new List<GameResultDto>()
        {
            new GameResultDto
            {
                ProductType = 5, GameId = 1, StationName = "Sic Bo A", GameDetail = "111",
                StartTime = new DateTime(2024, 3, 1)
            },
            new GameResultDto
            {
                ProductType = 5, GameId = 2, StationName = "Sic Bo A", GameDetail = "111",
                StartTime = new DateTime(2024, 3, 1)
            }
        }, c => c.WithStrictOrdering());
    }

    [Test]
    public void Given_GameResults_But_Get_Empty()
    {
        var actual = _leekieService.GameResults(new Period()
        {
            Start = new DateTime(2025, 2, 6),
            End = new DateTime(2025, 3, 4)
        }, 5);

        actual.Should().BeEmpty();
    }

    private void Given2024GameResults()
    {
        Given2024GameResults(new GameResultDto
        {
            ProductType = 5, GameId = 1, StationName = "Sic Bo A",
            StartTime = new DateTime(2024, 1, 2),
            GameDetail = "111"
        });
    }

    private void Given2023GameResults()
    {
        Given2024GameResults(new GameResultDto
        {
            ProductType = 5, GameId = 1, StationName = "Sic Bo A",
            StartTime = new DateTime(2023, 3, 2),
            GameDetail = "111"
        });
    }

    private void GivenMainGameResults()
    {
        GivenMainGameResults(new GameResultDto
        {
            ProductType = 5, GameId = 1, StationName = "Sic Bo A",
            StartTime = DateTime.Now,
            GameDetail = "111"
        });
    }

    private void Given2024GameResults(List<GameResultDto> expected)
    {
        _databaseService.GetClient("SBO2024").GetGameResults().Returns(expected);
    }

    private void Given2023GameResults(List<GameResultDto> expected)
    {
        _databaseService.GetClient("SBO2023").GetGameResults().Returns(expected);
    }

    private void GivenMainGameResults(List<GameResultDto> expected)
    {
        _databaseService.GetClient("LiveCasinoMain").GetGameResults().Returns(expected);
    }

    private void Given2024GameResults(params GameResultDto[] expected) => Given2024GameResults(expected.ToList());
    private void Given2023GameResults(params GameResultDto[] expected) => Given2023GameResults(expected.ToList());
    private void GivenMainGameResults(params GameResultDto[] expected) => GivenMainGameResults(expected.ToList());
}