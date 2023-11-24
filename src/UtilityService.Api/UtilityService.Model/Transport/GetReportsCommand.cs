namespace UtilityService.Model.Transport;

public class GetReportsCommand
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid? UserId { get; set; }
    public Guid? ResponsibleServiceId { get; set; }
    public int? Take { get; set; }
    public int? Skip { get; set; }
    public Status[]? Statuses { get; set; }
}