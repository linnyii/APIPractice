using admete.Enums;
using admete.MockedDatabase;
using admete.Models;
using admete.Repos;
using admete.Responses;
using Microsoft.AspNetCore.Mvc;

namespace admete.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeekieController(ILeekieService leekieService) : ControllerBase
{
    [HttpGet("GameResults")]
    public ActionResult<List<GameResultDto>> GameResults([FromQuery]GameResultRequest gameResultRequest)
    {
        if (gameResultRequest.InvalidStartDate())
        {
            return BadRequest(new ErrorResponse(EnumErrorCode.InvalidStartDate));
        }
        if (gameResultRequest.InvalidDate())
        {
            return BadRequest(new ErrorResponse(EnumErrorCode.InvalidDate));
        }
        
        var period = new Period()
        {
            Start = gameResultRequest.StartTime,
            End = gameResultRequest.EndTime
        };
        return leekieService.GameResults(period, gameResultRequest.ProductType);
    }
}