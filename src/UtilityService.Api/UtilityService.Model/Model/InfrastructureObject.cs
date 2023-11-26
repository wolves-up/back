using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityService.Model.Model;

public class InfrastructureObject
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Text { get; set; }
	public string UtilityServiceName { get; set; }
	public Location Location { get; set; }
}
