using UtilityService.Model.Model.Reports;
using UtilityService.Model.Transport;

namespace UtilityService.Api.Services;

public interface IReportCommentService
{
    Task CreateOrUpdate(ReportCommentCommand command);
    Task<ReportComment> GetCommentById(Guid reportCommentId);
    Task<ReportComment[]> GetCommentsByReport(Guid reportId);
    Task<ReportComment[]> GetCommentsByUser(Guid userId);
    Task SendToArchive(Guid id);
}