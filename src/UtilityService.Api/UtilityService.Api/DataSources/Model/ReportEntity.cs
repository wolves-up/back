using UtilityService.Model;

namespace UtilityService.Api.DataSources.Model;

public class ReportEntity : EntityBase
{
	public Guid UserId { get; set; }
	public string Message { get; set; }
	public Guid[] ContentIds { get; set; }
	public string[] Tags { get; set; }
	public Guid ResponsibleServiceId { get; set; }
	public Status Status { get; set; }
	public DateTime CreationDate { get; set; }
	public Location Location { get; set; }
}
