namespace UtilityService.Model.Model.Reports;

public class ReportComment
{
    public Guid Id { get; set; }
    public Guid ReportId { get; set; }
    public string Message { get; set; }
    public Guid[] ContentIds { get; set; }
    public DateTime CreationDate { get; set; }
}