using UtilityService.Model.Model;

namespace UtilityService.Model.Transport;

public class CreateInfrastructureObjectCommand
{
	public string Name { get; set; }
	public string Text { get; set; }
	public string UtilityServiceName { get; set; }
	public Location Location { get; set; }
}