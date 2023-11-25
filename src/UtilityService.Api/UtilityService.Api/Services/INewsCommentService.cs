using UtilityService.Model;
using UtilityService.Model.Model.News;
using UtilityService.Model.Transport;

namespace UtilityService.Api.Services;

public interface INewsCommentService
{
    Task CreateOrUpdate(NewsCommentCommand command);
    Task<NewsComment> GetCommentById(Guid newsCommentId);
    Task<NewsComment[]> GetCommentsByNews(Guid newsId);
    Task<NewsComment[]> GetCommentsByUser(Guid userId);
    Task SendToArchive(Guid commentId);
}