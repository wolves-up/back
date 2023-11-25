namespace UtilityService.Model.Model.News;

public class NewsComment
{
    public Guid Id { get; set; }
    public Guid NewsId { get; set; }
    public string Message { get; set; }
    public Guid[] ContentIds { get; set; }
    public DateTime CreationDate { get; set; }
    public Status Status { get; set; }
    public int LikeCount { get; set; }
}