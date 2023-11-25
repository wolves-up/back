using UtilityService.Model.Model.News;
using UtilityService.Model.Transport;

namespace UtilityService.Api.Services;

public interface INewsCommentService
{
    Task<Guid> CreateOrUpdate(NewsCommentCommand command);
    Task<NewsComment> GetCommentById(Guid newsCommentId);
    Task<NewsComment[]> GetCommentsByNews(Guid newsId, int limit = 1000, int skip = 0);
    Task<NewsComment[]> GetCommentsByUser(Guid userId);
    Task SendToArchive(Guid commentId);
}