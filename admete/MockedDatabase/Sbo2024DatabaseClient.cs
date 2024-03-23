namespace admete.MockedDatabase;

internal class Sbo2024DatabaseClient : IDatabaseClient
{
    public IEnumerable<GameResultDto> GetGameResults() =>
    [
        new GameResultDto
        {
            ProductType = 5, GameId = 11, StationName = "Sic Bo A",
            StartTime = DateTime.Now.AddDays(-8).AddMinutes(-10),
            GameDetail = "111"
        },
        new GameResultDto
        {
            ProductType = 5, GameId = 13, StationName = "Sic Bo B", StartTime = DateTime.Now.AddDays(-8).AddMinutes(10),
            GameDetail = "333"
        },
        new GameResultDto
        {
            ProductType = 5, GameId = 14, StationName = "Sic Bo B", StartTime = DateTime.Now.AddDays(-8),
            GameDetail = "222"
        },
        new GameResultDto
        {
            ProductType = 5, GameId = 12, StationName = "Sic Bo A", StartTime = DateTime.Now.AddDays(-8),
            GameDetail = "444"
        },
        new GameResultDto
        {
            ProductType = 1, GameId = 15, StationName = "Baccarat A", StartTime = DateTime.Now.AddDays(-8),
            GameDetail = "S3;C3;;S9;D11;;"
        },
        new GameResultDto
        {
            ProductType = 3, GameId = 16, StationName = "Roulette A", StartTime = DateTime.Now.AddDays(-8),
            GameDetail = "28"
        },
    ];
}
