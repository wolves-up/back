namespace UtilityService.Api.DataSources.Model;

public class ReportCommentEntity : EntityBase
{
	public Guid ReportId { get; set; }
	public string Message { get; set; }
	public Guid[] ContentIds { get; set; }
	public DateTime CreationDate { get; set; }
}
