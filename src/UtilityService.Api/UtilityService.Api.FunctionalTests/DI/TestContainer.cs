using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityService.Api.DI;

namespace UtilityService.Api.FunctionalTests.DI;

public static class TestContainer
{
	public static IServiceProvider Container => _container ??= CreateContainer();

	private static IServiceProvider CreateContainer()
	{
		var services = new ServiceCollection();

		ServiceRegistration.RegisterServices(services, LoggerFactory.Create(c => c.AddSimpleConsole()).CreateLogger("test"));

		return services.BuildServiceProvider();
	}

	private static IServiceProvider? _container;
}
