namespace admete.MockedDatabase;

internal class Sbo2023DatabaseClient : IDatabaseClient
{
    public IEnumerable<GameResultDto> GetGameResults() =>
    [
        new GameResultDto
        {
            ProductType = 5, GameId = 1, StationName = "Sic Bo A",
            StartTime = DateTime.Now.AddYears(-1).AddMinutes(-10),
            GameDetail = "111"
        },
        new GameResultDto
        {
            ProductType = 5, GameId = 3, StationName = "Sic Bo B", StartTime = DateTime.Now.AddYears(-1).AddMinutes(10),
            GameDetail = "333"
        },
        new GameResultDto
        {
            ProductType = 5, GameId = 4, StationName = "Sic Bo B", StartTime = DateTime.Now.AddYears(-1),
            GameDetail = "222"
        },
        new GameResultDto
        {
            ProductType = 5, GameId = 2, StationName = "Sic Bo A", StartTime = DateTime.Now.AddYears(-1),
            GameDetail = "444"
        },
        new GameResultDto
        {
            ProductType = 1, GameId = 5, StationName = "Baccarat A",
            StartTime = DateTime.Now.AddYears(-1).AddMinutes(-10),
            GameDetail = "D1;C10;S6;H4;D11;D11;"
        },
        new GameResultDto
        {
            ProductType = 1, GameId = 6, StationName = "Baccarat B",
            StartTime = DateTime.Now.AddYears(-1).AddMinutes(10), GameDetail = "H1;D13;;C11;C8;;"
        },
        new GameResultDto
        {
            ProductType = 3, GameId = 6, StationName = "Roulette B",
            StartTime = DateTime.Now.AddYears(-1).AddMinutes(10), GameDetail = "32"
        },
    ];
}
