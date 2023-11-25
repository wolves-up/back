using MongoDB.Driver;
using UtilityService.Api.DataSources.Model;
using UtilityService.Model.Model.News;

namespace UtilityService.Api.DataSources.Managers;

public interface INewsManager : IEntityManager<NewsEntity>
{
    Task<List<NewsEntity>> Take(int limit = 25, int skip = 0, NewsFilter? newsFilter = null);
}

public class NewsManager : EntityManager<NewsEntity>, INewsManager
{
    public NewsManager(IMongoDataBaseConnectionManager mongoDataBaseConnectionManager) : base(
        mongoDataBaseConnectionManager)
    {
    }

    // todo: нужно накрутить фильтров
    public async Task<List<NewsEntity>> Take(int limit = 25, int skip = 0, NewsFilter? newsFilter = null)
    {
        var filter = MongoDB.Driver.Builders<NewsEntity>.Filter.Empty;
        var sort = MongoDB.Driver.Builders<NewsEntity>.Sort.Ascending(x => x.CreateDate);
        var result = await _collection.FindAsync(filter, new MongoDB.Driver.FindOptions<NewsEntity>()
        {
            Sort = sort,
            Skip = skip,
            Limit = limit,
        }).ConfigureAwait(false);

        return await result.ToListAsync()
            .ConfigureAwait(false);
    }
}