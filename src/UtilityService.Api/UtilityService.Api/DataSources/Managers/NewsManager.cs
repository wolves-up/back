using MongoDB.Driver;
using UtilityService.Api.DataSources.Model;

namespace UtilityService.Api.DataSources.Managers;

public interface INewsManager : IEntityManager<NewsEntity>
{
    Task<List<NewsEntity>> Take(FilterDefinition<NewsEntity> newsFilter, int limit = 25, int skip = 0);
}

public class NewsManager : EntityManager<NewsEntity>, INewsManager
{
    public NewsManager(IMongoDataBaseConnectionManager mongoDataBaseConnectionManager) : base(
        mongoDataBaseConnectionManager)
    {
        var indexKey = Builders<NewsEntity>.IndexKeys.Descending(x => x.ResponsibleServiceId);
        var secondIndexKey = Builders<NewsEntity>.IndexKeys.Descending(x => x.CreateDate);
        var tagsIndexKey = Builders<NewsEntity>.IndexKeys.Descending(x => x.Tags);
        var combinedIndexKey = Builders<NewsEntity>.IndexKeys.Combine(
            indexKey,
            secondIndexKey,
            tagsIndexKey
        );
        _collection.Indexes.CreateOne(new CreateIndexModel<NewsEntity>(combinedIndexKey));
    }

    // todo: нужно накрутить фильтров
    public async Task<List<NewsEntity>> Take(FilterDefinition<NewsEntity> newsFilter, int limit = 25, int skip = 0)
    {
        var sort = Builders<NewsEntity>.Sort.Ascending(x => x.CreateDate);
        var result = await _collection.FindAsync(newsFilter, new FindOptions<NewsEntity>()
        {
            Sort = sort,
            Skip = skip,
            Limit = limit,
        }).ConfigureAwait(false);

        return await result.ToListAsync()
            .ConfigureAwait(false);
    }
}