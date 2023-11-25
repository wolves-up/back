using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using UtilityService.Api.FunctionalTests.DI;
using UtilityService.Api.Services;
using UtilityService.Model.Model.News;
using UtilityService.Model.Model.Reports;
using UtilityService.Model.Transport;

namespace UtilityService.Api.FunctionalTests.Services;

[TestFixture]
public class NewsService_Test
{
	private INewsService _newsService;
	private Fixture _fixture;

	[SetUp]
	public void SetUp()
	{
		_fixture = new Fixture();
		_newsService = (INewsService)TestContainer.Container.GetService(typeof(INewsService));
	}

	[Test]
	public async Task ShouldAddReports()
	{
		var news = await _newsService.CreateOrUpdateNews(new CreateNewsCommand()
		{
			Body = "Произойдет некоторое событие в некотором городе!",
			CreateDate = DateTime.UtcNow,
			ShortBody = "Это короткое описание новости!",
			Type = NewsType.Incident,
		});

		news.Should().NotBe(Guid.Empty);
	}
}
