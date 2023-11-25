namespace UtilityService.Model.Transport;

public class UpdateReportCommand
{
    public Guid ReportId { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public string[] Tags { get; set; }
    public Guid ResponsibleServiceId { get; set; }
}