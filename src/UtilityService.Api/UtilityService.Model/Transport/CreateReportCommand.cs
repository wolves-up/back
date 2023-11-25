using UtilityService.Model.Model;

namespace UtilityService.Model.Transport;

public class CreateReportCommand
{
    public string Title { get; set; }
    public string Message { get; set; }
    public Guid[]? ContentIds { get; set; }
    public string[] Tags { get; set; }
    public Guid ResponsibleServiceId { get; set; }
    public Location Location { get; set; }
}