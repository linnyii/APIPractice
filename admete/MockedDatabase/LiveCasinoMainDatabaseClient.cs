namespace admete.MockedDatabase;

internal class LiveCasinoMainDatabaseClient : IDatabaseClient
{
    public IEnumerable<GameResultDto> GetGameResults() =>
    [
        new GameResultDto
        {
            ProductType = 5, GameId = 101, StationName = "Sic Bo A", StartTime = DateTime.Now.AddMinutes(-10),
            GameDetail = "111"
        },
        new GameResultDto
        {
            ProductType = 5, GameId = 103, StationName = "Sic Bo B", StartTime = DateTime.Now.AddMinutes(10),
            GameDetail = "333"
        },
        new GameResultDto
        {
            ProductType = 5, GameId = 104, StationName = "Sic Bo B", StartTime = DateTime.Now, GameDetail = "222"
        },
        new GameResultDto
        {
            ProductType = 5, GameId = 102, StationName = "Sic Bo A", StartTime = DateTime.Now, GameDetail = "444"
        },
        new GameResultDto
        {
            ProductType = 1, GameId = 105, StationName = "Baccarat A", StartTime = DateTime.Now.AddMinutes(-10),
            GameDetail = "D13;H6;;H1;C13;H10;"
        },
        new GameResultDto
        {
            ProductType = 1, GameId = 106, StationName = "Baccarat B", StartTime = DateTime.Now.AddMinutes(10),
            GameDetail = "S10;H4;D3;S3;C1;D3;"
        },
        new GameResultDto
        {
            ProductType = 1, GameId = 107, StationName = "Baccarat B", StartTime = DateTime.Now,
            GameDetail = "C3;C8;S3;S4;H11;C11;"
        },
        new GameResultDto
        {
            ProductType = 1, GameId = 108, StationName = "Baccarat A", StartTime = DateTime.Now,
            GameDetail = "D9;H12;;H9;S9;;"
        },
        new GameResultDto
        {
            ProductType = 3, GameId = 108, StationName = "Roulette A", StartTime = DateTime.Now.AddMinutes(-10),
            GameDetail = "5"
        },
        new GameResultDto
            { ProductType = 3, GameId = 109, StationName = "Roulette B", StartTime = DateTime.Now.AddMinutes(10) },
    ];
}
