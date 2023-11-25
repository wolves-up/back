using AutoMapper;
using MongoDB.Driver;
using UtilityService.Api.DataSources.Managers;
using UtilityService.Api.DataSources.Model;
using UtilityService.Model.Model.Reports;
using UtilityService.Model.Transport;

namespace UtilityService.Api.Services;

public class ReportService : IReportService
{
	private readonly IReportManager _reportManager;
	private readonly IMapper _mapper;

	public ReportService(IReportManager reportManager, IMapper mapper)
	{
		_reportManager = reportManager;
		_mapper = mapper;
	}

	public async Task<Report> GetReportById(Guid reportId)
	{
		var report = await _reportManager.GetById(reportId)
			.ConfigureAwait(false);

		return _mapper.Map<Report>(report);
	}

	public async Task<Report[]> GetReports(GetReportsCommand getReportsCommand)
	{
		var filter = Builders<ReportEntity>.Filter.And(GetFilter(getReportsCommand));
		var result = await _reportManager.FindBy(filter, getReportsCommand.Take ?? 1000, getReportsCommand.Skip ?? 0)
			.ConfigureAwait(false);
		return _mapper.Map<Report[]>(result);
	}

	private IEnumerable<FilterDefinition<ReportEntity>> GetFilter(GetReportsCommand command)
	{
		if (command.UserId != null)
			yield return Builders<ReportEntity>.Filter.Where(x => x.UserId == command.UserId);
		if (command.StartDate != null)
			yield return Builders<ReportEntity>.Filter.Where(x => x.CreationDate >= command.StartDate);
		if (command.EndDate != null)
			yield return Builders<ReportEntity>.Filter.Where(x => x.CreationDate <= command.EndDate);
		if (command.ResponsibleServiceId != null)
			yield return Builders<ReportEntity>.Filter.Where(x => x.ResponsibleServiceId == command.ResponsibleServiceId);
		if (command.Statuses != null)
			yield return Builders<ReportEntity>.Filter.Where(x => command.Statuses.Any(y => y == x.Status));
	}

	public async Task<Report[]> FindUtilityServiceReports(Guid utilityServiceId)
	{
		var result = await _reportManager.FindByUtilityServiceId(utilityServiceId)
			.ConfigureAwait(false);
		return _mapper.Map<Report[]>(result);
	}

	public async Task<Report[]> FindUserReports(Guid userId)
	{
		var result = await _reportManager.FindByUserId(userId)
			.ConfigureAwait(false);
		return _mapper.Map<Report[]>(result);
	}

	public async Task<Report> Create(CreateReportCommand createReportCommand, Guid userId)
	{
		var reportEntity = new ReportEntity()
		{
			UserId = userId,
			Title = createReportCommand.Title,
			Message = createReportCommand.Message,
			Tags = createReportCommand.Tags,
			ResponsibleServiceId = createReportCommand.ResponsibleServiceId,
			Location = createReportCommand.Location,
			ContentIds = createReportCommand.ContentIds,
			CreationDate = DateTime.UtcNow,
		};
		await _reportManager.Add(reportEntity)
			.ConfigureAwait(false);

		return _mapper.Map<Report>(reportEntity);
	}

	public async Task Update(UpdateReportCommand updateReportCommand)
	{
		var reportEntity = await _reportManager.GetById(updateReportCommand.ReportId)
			.ConfigureAwait(false);

	}
}
