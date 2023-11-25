using MongoDB.Driver;
using UtilityService.Api.DataSources.Managers;
using UtilityService.Api.DataSources.Model;
using UtilityService.Api.Services.Converters;
using UtilityService.Model.Model;
using UtilityService.Model.Model.News;
using UtilityService.Model.Transport;

namespace UtilityService.Api.Services;

public class NewsService : INewsService
{
    private readonly INewsManager _newsManager;
    private readonly NewsConverter _newsConverter;
    private readonly IContentService _contentService;

    public NewsService(
        INewsManager newsManager,
        NewsConverter newsConverter,
        IContentService contentService
    )
    {
        _newsManager = newsManager;
        _newsConverter = newsConverter;
        _contentService = contentService;
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

    public async Task<ShortNews[]> TakeActualNews(int count, int skip, NewsFilter newsFilter)
    {
        var filter = Builders<NewsEntity>.Filter.And(CreateFilter(newsFilter));
        var news = await _newsManager.Take(filter,count, skip);
        return news.Select(x => _newsConverter.ToShortNews(x)).ToArray();
    }

    public async Task<News> GetNewsById(Guid id)
    {
        return _newsConverter.ToNews(await _newsManager.GetById(id));
    }

    public Task SendToArchive(Guid id)
    {
        throw new NotImplementedException();
    }

    //оптимизировать перезапись на точно такие же ids
    private async Task DeleteNewsContent(Guid headerContentId, Guid[]? contentIds)
    {
        await _contentService.Delete(headerContentId);

        if (contentIds is not null || contentIds!.Any())
        {
            await _contentService.DeleteRange(contentIds);
        }
    }

    private async Task SaveNewsContent(Content headerContent, Content[]? content)
    {
        await _contentService.Add(headerContent);

        if (content is not null || content!.Any())
        {
            await _contentService.AddRange(content);
        }
    }

    private IEnumerable<FilterDefinition<NewsEntity>> CreateFilter(NewsFilter filter)
    {
        if (filter.Title != null)
            yield return Builders<NewsEntity>.Filter.Where(x =>
                x.Title.ToLower().Contains(filter.Title.ToLower()));
        if (filter.Type != null)
            yield return Builders<NewsEntity>.Filter.Where(x =>
                x.Type == filter.Type);
        if (filter.ResponsibleServiceId != null)
            yield return Builders<NewsEntity>.Filter.Where(x =>
                x.ResponsibleServiceId == filter.ResponsibleServiceId);
        if (filter.CreateDate != null)
            yield return Builders<NewsEntity>.Filter.Where(x =>
                x.CreateDate >= filter.CreateDate);
        if (filter.Status != null)
            yield return Builders<NewsEntity>.Filter.Where(x =>
                x.Status == filter.Status);
        // if (filter.Location != null)
        if (filter.Tags != null)
            yield return Builders<NewsEntity>.Filter.Where(x =>
                filter.Tags.All(t => x.Tags.Contains(t)));
    }
}