using UtilityService.Api.DataSources.Managers;
using UtilityService.Api.Services.Converters;
using UtilityService.Model.Model.News;
using UtilityService.Model.Transport;

namespace UtilityService.Api.Services;

public interface INewsCommentService
{
    Task CreateOrUpdate(NewsCommentCommand command);
    Task<NewsComment> GetCommentById(Guid newsCommentId);
    Task<NewsComment[]> GetCommentsByNews(Guid newsId, int limit = 1000, int skip = 0);
    Task<NewsComment[]> GetCommentsByUser(Guid userId);
    Task SendToArchive(Guid commentId);
}

public class NewsCommentService : INewsCommentService
{
    private readonly INewsCommentsManager _commentsManager;
    private readonly NewsCommentConverter _converter;

    public NewsCommentService(INewsCommentsManager commentsManager, NewsCommentConverter converter)
    {
        _commentsManager = commentsManager;
        _converter = converter;
    }

    public Task CreateOrUpdate(NewsCommentCommand command)
    {
        throw new NotImplementedException();
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
}