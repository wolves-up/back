using UtilityService.Model.Model;

namespace UtilityService.Api.DataSources.Model;

public class InfrastructureObjectEntity : EntityBase
{
	public string Name { get; set; }
	public string Text { get; set; }
	public string UtilityServiceName { get; set; }
	public Location Location { get; set; }
}
