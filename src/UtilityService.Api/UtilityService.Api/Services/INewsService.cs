using UtilityService.Model.Model.News;
using UtilityService.Model.Transport;

namespace UtilityService.Api.Services;

public interface INewsService
{
    Task<Guid> CreateOrUpdateNews(CreateNewsCommand command);
    Task<ShortNews> TakeActualNews(int count, int skip, NewsFilter filter);
    Task SendToArchive(Guid id);
}