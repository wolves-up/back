namespace UtilityService.Model.Transport;

public class NewsCommentCommand
{
    public Guid Id { get; set; }
    public Guid NewsId { get; set; }
    public string Message { get; set; }
    public byte[]? Content { get; set; }
}