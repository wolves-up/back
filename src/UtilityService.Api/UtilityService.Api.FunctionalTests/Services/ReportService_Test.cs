using AutoFixture;
using FluentAssertions;
using NSubstitute;
using UtilityService.Api.DataSources.Managers;
using UtilityService.Api.FunctionalTests.DI;
using UtilityService.Model.Model.Reports;
using UtilityService.Model.Transport;

namespace UtilityService.Api.FunctionalTests.Services;

[TestFixture]
internal class ReportService_Test
{
	private IReportService _reportService;
	private Fixture _fixture;
	private IReportManager _reportManager;

	[SetUp]
	public void SetUp()
	{
		_fixture = new Fixture();
		_reportService = (IReportService)TestContainer.Container.GetService(typeof(IReportService));
		_reportManager = (IReportManager)TestContainer.Container.GetService(typeof(IReportManager));
	}

	[Test]
	public async Task DeleteAll()
	{
		var reportsToDelete = (await _reportService.GetAllReports())
				.Where(x => (x.Tags ?? new string[] { }).Any(y => y.Length == Guid.NewGuid().ToString().Length))
			;
		foreach (var report in reportsToDelete)
		{
			await _reportManager.DeleteById(report.Id);
		}
	}

	[Test]
	public async Task ShouldAddReports()
	{
		var reportCommand = _fixture.Create<CreateReportCommand>();
		var report = await _reportService.Create(reportCommand, Guid.NewGuid());

		report.Id.Should().NotBe(Guid.Empty);
	}

	[Test]
	public async Task ShouldFindReportsOfUser()
	{
		var userId = Guid.NewGuid();
		var reports = new List<Report>();
		for (var i = 0; i < 10; i++)
		{
			var reportCommand = _fixture.Create<CreateReportCommand>();
			var report = await _reportService.Create(reportCommand, userId);
			reports.Add(report);
		}

		var findResult = await _reportService.FindUserReports(userId);

		findResult.OrderBy(x => x.Id).Should().BeEquivalentTo(reports.OrderBy(x => x.Id), 
			x => 
			x.ComparingByMembers<Report>()
				.Excluding(x => x.CreationDate));
	}
}
