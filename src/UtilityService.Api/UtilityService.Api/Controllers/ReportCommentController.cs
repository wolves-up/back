using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UtilityService.Api.Services;
using UtilityService.Model.Model.Reports;
using UtilityService.Model.Transport;

namespace UtilityService.Api.Controllers;

[Controller]
[Route("reports/comments")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ReportCommentController : ControllerBase
{
    private readonly IReportCommentService _reportCommentService;

    public ReportCommentController(IReportCommentService reportCommentService)
    {
        _reportCommentService = reportCommentService;
    }


    [HttpPost]
    public async Task CreateOrUpdate([FromBody] ReportCommentCommand command)
    {
        await _reportCommentService.CreateOrUpdate(command);
    }

    [HttpGet]
    public async Task<ReportComment> GetCommentById(Guid reportCommentId)
    {
        return await _reportCommentService.GetCommentById(reportCommentId);
    }

    [HttpGet("post/{id}")]
    public async Task<ReportComment[]> GetCommentsByReport(Guid reportId)
    {
        return await _reportCommentService.GetCommentsByReport(reportId);
    }

    [HttpGet("user/{id}")]
    public async Task<ReportComment[]> GetCommentsByUser(Guid userId)
    {
        return await _reportCommentService.GetCommentsByUser(userId);
    }

    [HttpPost("archive")]
	public async Task SendToArchive(Guid id)
    {
        throw new NotImplementedException();
    }
}