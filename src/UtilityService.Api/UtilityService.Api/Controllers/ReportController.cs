using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using UtilityService.Model.Transport;

namespace UtilityService.Api.Controllers;

public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateReportCommand createReportCommand)
    {
        await _reportService.Create(createReportCommand, 
            new Guid(User.Claims.First(x => x.Type == JwtClaimTypes.Id).Value));

        return Ok();
    }

    // TODO Проверять доступ
    [HttpPut]
    public async Task<IActionResult> Update(UpdateReportCommand updateReportCommand)
    {
        await _reportService.Update(updateReportCommand);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get(GetReportsCommand getReportsCommand)
    {
        var result = await _reportService.GetReports(getReportsCommand);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var result = await _reportService.GetReportById(new Guid(id));

        return Ok(result);
    }
    
    [HttpGet("/user/{userId}")]
    public async Task<IActionResult> FindForUser(string userId)
    {
        var result = await _reportService.FindUserReports(new Guid(userId));

        return Ok(result);
    }
    
    [HttpGet("/utility/{serviceId}")]
    public async Task<IActionResult> Find(string serviceId)
    {
        var result = await _reportService.FindUtilityServiceReports(new Guid(serviceId));

        return Ok(result);
    }
    //
}