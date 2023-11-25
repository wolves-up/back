using UtilityService.Api.DataSources.Managers;
using UtilityService.Api.Services.Converters;
using UtilityService.Model.Model;
using UtilityService.Model.Model.News;
using UtilityService.Model.Transport;

namespace UtilityService.Api.Services;

public class NewsCommentService : INewsCommentService
{
    private readonly INewsCommentsManager _commentsManager;
    private readonly NewsCommentConverter _converter;
    private readonly IContentService _contentService;

    public NewsCommentService(
        INewsCommentsManager commentsManager,
        NewsCommentConverter converter,
        IContentService contentService
    )
    {
        _commentsManager = commentsManager;
        _converter = converter;
        _contentService = contentService;
    }

    public async Task<Guid> CreateOrUpdate(NewsCommentCommand command)
    {
        var newsId = command.Id;
        var existingComment = (await _commentsManager.FindByIds(new[] {newsId})).SingleOrDefault();
        if (existingComment is not null)
        {
            await DeleteContent(existingComment.ContentIds);
            await _commentsManager.Update(_converter.ToEntity(command, command.Content?.Select(x => x.Id).ToArray()));
        }
        else
        {
            await _commentsManager.Add(_converter.ToEntity(command, command.Content?.Select(x => x.Id).ToArray()));
        }

        await SaveContent(command.Content);
        return command.Id;
    }

    public Task<NewsComment> GetCommentById(Guid newsCommentId)
    {
        throw new NotImplementedException();
    }

    public async Task<NewsComment[]> GetCommentsByNews(Guid newsId, int limit = 1000, int skip = 0)
    {
        var comments = await _commentsManager.GetCommentsByNewsId(newsId, limit, 0);
        return comments.Select(x => _converter.ToNewsComment(x)).ToArray();
    }

    public Task<NewsComment[]> GetCommentsByUser(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task SendToArchive(Guid commentId)
    {
        throw new NotImplementedException();
    }

    //оптимизировать перезапись на точно такие же ids
    private async Task DeleteContent(Guid[]? contentIds)
    {
        if (contentIds is not null || contentIds!.Any())
        {
            await _contentService.DeleteRange(contentIds);
        }
    }

    private async Task SaveContent(Content[]? content)
    {
        if (content is not null || content!.Any())
        {
            await _contentService.AddRange(content);
        }
    }
}