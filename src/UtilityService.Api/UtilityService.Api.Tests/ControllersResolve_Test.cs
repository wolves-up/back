using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UtilityService.Api.DI;

namespace UtilityService.Api.Tests;

public class ControllersResolve_Test
{
	[Test]
	public void CheckRegistrations()
	{
		var services = new ServiceCollection();

		ServiceRegistration.RegisterServices(services, LoggerFactory.Create(c => c.AddSimpleConsole()).CreateLogger("test"));

		var controllersAssembly = typeof(ServiceRegistration).Assembly;
		var controllers = controllersAssembly.ExportedTypes.Where(x => typeof(ControllerBase).IsAssignableFrom(x))
			.ToList();

		controllers.ForEach(x => services.AddTransient(x));

		var serviceProvider = services.BuildServiceProvider();


		controllers.Count().Should().BeGreaterThan(0);
		foreach (var controller in controllers)
		{
			serviceProvider.GetRequiredService(controller).Should().NotBeNull();
		}
	}
}