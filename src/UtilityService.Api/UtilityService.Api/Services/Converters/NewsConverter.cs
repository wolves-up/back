using UtilityService.Api.DataSources.Model;
using UtilityService.Model.Model.News;
using UtilityService.Model.Transport;

namespace UtilityService.Api.Services.Converters;

public class NewsConverter
{
    public NewsEntity ToEntity(CreateNewsCommand command, Guid headerContentId, Guid[]? contentIds)
        => new()
        {
            Id = command.Id,
            Title = command.Title,
            ShortBody = command.ShortBody,
            Body = command.Body,
            Status = command.Status,
            CreateDate = DateTime.Now,
            LikeCount = 0,
            Location = command.Location,
            ResponsibleServiceId = command.ResponsibleServiceId,
            Tags = command.Tags,
            Type = command.Type,
            HeaderContentId = headerContentId,
            ContentIds = contentIds,
        };

    public News ToNews(NewsEntity entity)
        => new()
        {
            Id = entity.Id,
            Title = entity.Title,
            ShortBody = entity.ShortBody,
            Body = entity.Body,
            Status = entity.Status,
            CreateDate = entity.CreateDate,
            LikeCount = entity.LikeCount,
            Location = entity.Location,
            ResponsibleServiceId = entity.ResponsibleServiceId,
            Tags = entity.Tags,
            Type = entity.Type,
            HeaderContentId = entity.HeaderContentId,
            ContentIds = entity.ContentIds,
        };

    public ShortNews ToShortNews(NewsEntity entity)
        => new()
        {
            Id = entity.Id,
            Title = entity.Title,
            ShortBody = entity.ShortBody,
            Status = entity.Status,
            CreateDate = DateTime.Now,
            LikeCount = entity.LikeCount,
            Location = entity.Location,
            ResponsibleServiceId = entity.ResponsibleServiceId,
            Tags = entity.Tags,
            Type = entity.Type,
            HeaderContentId = entity.HeaderContentId,
        };
}