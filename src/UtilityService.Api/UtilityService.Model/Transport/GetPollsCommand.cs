namespace UtilityService.Model.Transport;

public class GetPollsCommand
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string[] Tags { get; set; }
    public Guid ResponsibleServiceId { get; set; }
    public int? Take { get; set; }
    public int? Skip { get; set; }
}