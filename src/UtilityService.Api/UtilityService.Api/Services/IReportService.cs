using UtilityService.Model.Model.Reports;
using UtilityService.Model.Transport;

public interface IReportService
{
    Task<Report> GetReportById(Guid reportId);
    Task<Report[]> GetReports(GetReportsCommand getReportsCommand);

    Task<Guid[]?> FindUtilityServiceReports(Guid utilityServiceId);
    Task<Guid[]?> FindUserReports(Guid userId);

    Task<Guid> Create(CreateReportCommand createReportCommand, Guid userId);
    Task Update(UpdateReportCommand updateReportCommand);
}