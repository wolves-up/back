using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UtilityService.Api.DataSources.Managers;
using UtilityService.Api.Utils;
using UtilityService.Model.Model;
using UtilityService.Model.Model.Reports;
using UtilityService.Model.Transport;

namespace UtilityService.Api.Controllers;

[Controller]
[Route("reports")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ReportController : ControllerBase
{
    public ReportController(IReportService reportService, IUserManager userManager, IReportManager reportManager)
    {
        _reportService = reportService;
        _userManager = userManager;
        _reportManager = reportManager;
    }

    [HttpPost("create")]
    public async Task<Report> Create([FromBody] CreateReportCommand createReportCommand)
    {
        var report = await _reportService.Create(createReportCommand, User.GetUserId())
            .ConfigureAwait(false);
        return report;
    }
    
    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] UpdateReportCommand updateReportCommand)
    {
        var user = await _userManager.GetById(User.GetUserId()); 
        var report = await _reportService.GetReportById(updateReportCommand.ReportId);

        if (report.UserId != user.Id && user.Role == Role.User)
        {
            return Forbid();
        }

        await _reportService.Update(updateReportCommand);

        return Ok();
    }

    [HttpPost("update-status")]
    public async Task<IActionResult> UpdateStatus(Guid reportId, Status status)
    {
        var user = await _userManager.GetById(User.GetUserId());
        var report = await _reportService.GetReportById(reportId);

        if (report.UserId != user.Id && user.Role == Role.User)
        {
            return Forbid();
        }

        await _reportService.ChangeStatus(reportId, status);

        return Ok();
    }

	[HttpGet("search")]
    [HttpPost("search")]
	public async Task<IActionResult> Search([FromBody] GetReportsCommand getReportsCommand)
    {
        var result = await _reportService.GetReports(getReportsCommand);

        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _reportService.GetAllReports();

        return Ok(result);
    }

	[HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _reportService.GetReportById(id);

        return Ok(result);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _reportManager.DeleteById(id);

        return Ok();
    }

	[HttpGet("search/user/{userId}")]
    public async Task<IActionResult> FindForUser(Guid userId)
    {
        var result = await _reportService.FindUserReports(userId);

        return Ok(result);
    }
    
    [HttpGet("search/service/{serviceId}")]
    public async Task<IActionResult> Find(Guid serviceId)
    {
        var result = await _reportService.FindUtilityServiceReports(serviceId);

        return Ok(result);
	}

    private readonly IReportService _reportService;
    private readonly IUserManager _userManager;
    private readonly IReportManager _reportManager;
}