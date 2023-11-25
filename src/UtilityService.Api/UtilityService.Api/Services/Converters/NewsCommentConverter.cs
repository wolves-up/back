using UtilityService.Model;
using UtilityService.Model.Model.News;
using UtilityService.Model.Transport;

namespace UtilityService.Api.Services.Converters;

public class NewsCommentConverter
{
    public NewsCommentEntity ToEntity(NewsCommentCommand command, Guid[]? contentIds)
        => new()
        {
            Id = command.Id,
            Message = command.Message,
            Status = Status.New,
            ContentIds = contentIds,
            CreationDate = DateTime.Now,
            LikeCount = 0,
            NewsId = command.NewsId
        };    
    
    public NewsComment ToNewsComment(NewsCommentEntity entity)
        => new()
        {
            Id = entity.Id,
            Message = entity.Message,
            ContentIds = entity.ContentIds,
            Status = entity.Status,
            CreationDate = entity.CreationDate,
            LikeCount = entity.LikeCount,
            NewsId = entity.Id,
        };
}