using admete.Controllers;
using admete.MockedDatabase;
using admete.Models;

namespace admete.Repos;

public interface ILeekieService
{
    List<GameResultDto> GameResults(Period period, int productType);
}