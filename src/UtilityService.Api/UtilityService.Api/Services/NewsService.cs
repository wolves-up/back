using MongoDB.Driver;
using UtilityService.Api.DataSources.Managers;
using UtilityService.Api.DataSources.Model;
using UtilityService.Api.Services.Converters;
using UtilityService.Model.Model.News;
using UtilityService.Model.Transport;

namespace UtilityService.Api.Services;

public class NewsService : INewsService
{
    private readonly INewsManager _newsManager;
    private readonly NewsConverter _newsConverter;

    public NewsService(
        INewsManager newsManager,
        NewsConverter newsConverter
    )
    {
        _newsManager = newsManager;
        _newsConverter = newsConverter;
    }

    public async Task<Guid> CreateOrUpdateNews(CreateNewsCommand command)
    {
        var newsId = command.Id;
        var existingNews = (await _newsManager.FindByIds(new[] {newsId})).SingleOrDefault();
        var newsEntity = _newsConverter.ToEntity(command, command.HeaderContentId, command.BodyContentIds);
        if (existingNews is not null)
        {
            await _newsManager.Update(newsEntity);
        }
        else
        {
            await _newsManager.Add(newsEntity);
        }
        return newsEntity.Id;
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

    public async Task<News[]> GetAll()
    {
        return (await _newsManager.GetAll()).Select(_newsConverter.ToNews).ToArray();
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