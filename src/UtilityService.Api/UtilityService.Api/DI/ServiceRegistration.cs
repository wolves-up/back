using UtilityService.Api.Configuration;
using UtilityService.Api.DataSources.Managers;
using UtilityService.Api.DataSources;
using UtilityService.Api.Services;

namespace UtilityService.Api.DI;

public static class ServiceRegistration
{
	public static void RegisterServices(IServiceCollection services, ILogger log)
	{

		services.AddSingleton<ILogger>(c => log);
		services.AddSingleton<IConfigProvider>(new ConfigProvider());
		services.AddSingleton<IMongoDataBaseConnectionManager, MongoDbConnectionManager>();
		services.AddSingleton<IUserManager, UserManager>();
		services.AddSingleton<IReportManager, ReportManager>();
		services.AddSingleton<IReportCommentManager, ReportCommentManager>();
		services.AddSingleton<IUtilityServiceManager, UtilityServiceManager>();

		services.AddSingleton<IReportService, StubReportService>();

		services.AddSingleton<IReportService, StubReportService>();
	}
}
