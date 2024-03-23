using admete.MockedDatabase;
using admete.Models;

namespace admete.Repos;

public class LeekieService(IDatabaseService _databaseService, TimeProvider _timeProvider) : ILeekieService
{
    private const int CutoffDays = -7;

    public List<GameResultDto> GameResults(Period period, int productType)
    {
        var mainRepo = _databaseService.GetClient("LiveCasinoMain");
        var sbo2024 = _databaseService.GetClient("SBO2024");
        var sbo2023 = _databaseService.GetClient("SBO2023");

        var now = _timeProvider.GetLocalNow();
        var cutoffDate = now.AddDays(CutoffDays).DateTime;

        var mainPeriod = new Period()
        {
            Start = cutoffDate,
            End = now.AddDays(1).DateTime
        };
        var sbo2024Period = new Period()
        {
            Start = DateTime.Parse("2024-01-01"),
            End = cutoffDate
        };
        var sbo2023Period = new Period()
        {
            Start = DateTime.Parse("2023-01-01"),
            End = DateTime.Parse("2024-01-01")
        };

        var result = new List<GameResultDto>();
        if (period.IsOverlapping(mainPeriod))
        {
            result.AddRange(mainRepo.GetGameResults());
        }

        if (period.IsOverlapping(sbo2024Period))
        {
            result.AddRange(sbo2024.GetGameResults());
        }

        if (period.IsOverlapping(sbo2023Period))
        {
            result.AddRange(sbo2023.GetGameResults());
        }

        return result
            .Where(x =>
                x.StartTime >= period.Start
                && x.StartTime < period.End
                && x.ProductType == productType
            )
            .OrderBy(x => x.StartTime)
            .ThenBy(x => x.GameId)
            .ToList();
    }
}