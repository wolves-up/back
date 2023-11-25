using UtilityService.Model.Model.Reports;
using UtilityService.Model.Transport;

public interface IReportService
{
    Task<Report> GetReportById(Guid reportId);
    Task<Report[]> GetReports(GetReportsCommand getReportsCommand);

    Task<Report[]> FindUtilityServiceReports(Guid utilityServiceId);
    Task<Report[]> FindUserReports(Guid userId);

    Task<Report> Create(CreateReportCommand createReportCommand, Guid userId);
    Task Update(UpdateReportCommand updateReportCommand);
}