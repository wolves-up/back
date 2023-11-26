using AutoMapper;
using UtilityService.Api.Configuration;
using UtilityService.Api.DataSources.Managers;
using UtilityService.Api.DataSources;
using UtilityService.Api.DataSources.Model;
using UtilityService.Api.Services;
using UtilityService.Api.Services.Converters;
using UtilityService.Model.Model;
using UtilityService.Model.Model.Reports;

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
		services.AddSingleton<IReportService, ReportService>();
		services.AddAutoMapper(ConfigAutoMapper);
		services.AddSingleton<IUtilityStorageService, UtilityStorageService>();

		services.AddSingleton<IInfrastructureObjectManager, InfrastructureObjectManager>();

		AddNewsService(services);
        AddContentService(services);
    }

    private static void AddNewsService(IServiceCollection services)
    {
        services.AddSingleton<INewsService, NewsService>();
        services.AddSingleton<NewsConverter>();
        services.AddSingleton<INewsManager, NewsManager>();
        services.AddSingleton<INewsCommentsManager, NewsCommentsManager>();
        services.AddSingleton<NewsCommentConverter>();
        services.AddSingleton<INewsCommentService, NewsCommentService>();
	}

	private static void ConfigAutoMapper(IMapperConfigurationExpression config)
	{
		config.AddMaps(typeof(ServiceRegistration).Assembly, typeof(User).Assembly);
		config.CreateMap<ReportEntity, Report>(); // TODO: Move to profiles
		config.CreateMap<InfrastructureObjectEntity, InfrastructureObject>();
	}

    private static void AddContentService(IServiceCollection services)
    {
        services.AddSingleton<IContentManager, ContentManager>();
        services.AddSingleton<IContentService, ContentService>();
        services.AddSingleton<ContentConverter>();
    }
}