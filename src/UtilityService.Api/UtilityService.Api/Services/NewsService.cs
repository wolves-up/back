using UtilityService.Api.DataSources.Managers;
using UtilityService.Api.Services.Converters;
using UtilityService.Model.Model;
using UtilityService.Model.Model.News;
using UtilityService.Model.Transport;

namespace UtilityService.Api.Services;

public class NewsService : INewsService
{
    private readonly NewsManager _newsManager;
    private readonly ContentManager _contentManager;
    private readonly NewsConverter _newsConverter;
    private readonly ContentConverter _contentConverter;

    public NewsService(
        NewsManager newsManager,
        ContentManager contentManager,
        NewsConverter newsConverter,
        ContentConverter contentConverter)
    {
        _newsManager = newsManager;
        _contentManager = contentManager;
        _newsConverter = newsConverter;
        _contentConverter = contentConverter;
    }

    public async Task<Guid> CreateOrUpdateNews(CreateNewsCommand command)
    {
        var newsId = command.Id;
        var existingNews = (await _newsManager.FindByIds(new[] {newsId})).SingleOrDefault();
        if (existingNews is not null)
        {
            await DeleteNewsContent(existingNews.HeaderContentId, existingNews.ContentIds);
            await _newsManager.Update(_newsConverter.ToEntity(command, command.HeaderContent.Id,
                command.BodyContent?.Select(x => x.Id).ToArray())
            );
        }
        else
        {
            await _newsManager.Add(_newsConverter.ToEntity(command, command.HeaderContent.Id,
                command.BodyContent?.Select(x => x.Id).ToArray())
            );
        }

        await SaveNewsContent(command.HeaderContent, command.BodyContent);
        return command.Id;
    }

    public Task<ShortNews> TakeActualNews(int count, int skip, NewsFilter filter)
    {
        throw new NotImplementedException();
    }

    public Task SendToArchive(Guid id)
    {
        throw new NotImplementedException();
    }

    //оптимизировать перезапись на точно такие же ids
    private async Task DeleteNewsContent(Guid headerContentId, Guid[]? contentIds)
    {
        var tasks = new List<Task> {_contentManager.DeleteById(headerContentId)};
        if (contentIds is not null || contentIds!.Any())
        {
            tasks.AddRange(contentIds.Select(x => _contentManager.DeleteById(x)));
        }

        await Task.WhenAll(tasks);
    }

    private async Task SaveNewsContent(Content headerContent, Content[]? content)
    {
        var tasks = new List<Task> {_contentManager.Add(_contentConverter.ToEntity(headerContent))};
        if (content is not null || content!.Any())
        {
            tasks.AddRange(content.Select(x => _contentManager.Add(_contentConverter.ToEntity(x))));
        }

        await Task.WhenAll(tasks);
    }
}