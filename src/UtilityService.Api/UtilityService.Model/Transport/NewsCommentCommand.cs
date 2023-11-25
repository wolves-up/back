using UtilityService.Model.Model;

namespace UtilityService.Model.Transport;

public class NewsCommentCommand
{
    public Guid Id { get; set; }
    public Guid NewsId { get; set; }
    public string Message { get; set; }
    public Guid[]? ContentIds { get; set; }
}