using UtilityService.Model.Model;

namespace UtilityService.Model.Transport;

public class ReportCommentCommand
{
    public Guid? Id { get; set; }
    public Guid? UserId { get; set; }
    public Guid? ResponsibleServiceId { get; set; }
    public Guid ReportId { get; set; }
    public string Message { get; set; }
    public Content[]? Content { get; set; }
}