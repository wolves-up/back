using Microsoft.AspNetCore.Mvc;
using UtilityService.Api.Services;
using UtilityService.Model.Model.Reports;
using UtilityService.Model.Transport;

namespace UtilityService.Api.Controllers;

[Controller]
public class ReportCommentController : ControllerBase
{
    private readonly IReportCommentService _reportCommentService;

    public ReportCommentController(IReportCommentService reportCommentService)
    {
        _reportCommentService = reportCommentService;
    }


    [HttpPost]
    public Task CreateOrUpdate([FromBody] ReportCommentCommand command)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}")]
    public Task<ReportComment> GetCommentById(Guid reportCommentId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("reports/comments/{id}")]
    public Task<ReportComment[]> GetCommentsByReport(Guid reportId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("reports/comments/user/{id}")]
    public Task<ReportComment[]> GetCommentsByUser(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task SendToArchive(Guid id)
    {
        throw new NotImplementedException();
    }
}