using UtilityService.Api.DataSources.Managers;
using UtilityService.Model.Model.Reports;
using UtilityService.Model.Transport;

namespace UtilityService.Api.Services;

public class ReportService : IReportService
{
	private readonly IReportManager _reportManager;

	public ReportService(IReportManager reportManager)
	{
		_reportManager = reportManager;
	}

	public Task<Report> GetReportById(Guid reportId)
	{
		throw new NotImplementedException();
	}

	public Task<Report[]> GetReports(GetReportsCommand getReportsCommand)
	{
		throw new NotImplementedException();
	}

	public Task<Report[]> FindUtilityServiceReports(Guid utilityServiceId)
	{
		throw new NotImplementedException();
	}

	public Task<Report[]> FindUserReports(Guid userId)
	{
		throw new NotImplementedException();
	}

	public Task<Report> Create(CreateReportCommand createReportCommand, Guid userId)
	{
		throw new NotImplementedException();
	}

	public Task Update(UpdateReportCommand updateReportCommand)
	{
		throw new NotImplementedException();
	}
}
