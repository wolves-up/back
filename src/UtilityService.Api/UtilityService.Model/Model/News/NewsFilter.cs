namespace UtilityService.Model.Model.News;

public class NewsFilter
{
    public string? Title { get; set; }
    public NewsType? Type { get; set; }
    public Guid? ResponsibleServiceId { get; set; }
    public DateTime? CreateDate { get; set; }
    public Status? Status { get; set; }
    public Location? Location { get; set; }
    public string[]? Tags { get; set; }
}