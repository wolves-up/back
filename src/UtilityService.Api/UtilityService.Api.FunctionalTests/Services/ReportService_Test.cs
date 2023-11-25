using AutoFixture;
using FluentAssertions;
using NSubstitute;
using UtilityService.Api.FunctionalTests.DI;
using UtilityService.Model.Model.Reports;
using UtilityService.Model.Transport;

namespace UtilityService.Api.FunctionalTests.Services;

[TestFixture]
internal class ReportService_Test
{
	private IReportService _reportService;
	private Fixture _fixture;

	[SetUp]
	public void SetUp()
	{
		_fixture = new Fixture();
		_reportService = (IReportService)TestContainer.Container.GetService(typeof(IReportService));
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
