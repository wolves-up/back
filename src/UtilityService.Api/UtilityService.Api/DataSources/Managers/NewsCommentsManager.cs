using MongoDB.Driver;
using UtilityService.Model.Model.News;

namespace UtilityService.Api.DataSources.Managers;

public interface INewsCommentsManager : IEntityManager<NewsCommentEntity>
{
    Task<List<NewsCommentEntity>> GetCommentsByNewsId(Guid newsId, int limit = 1000, int skip = 0);
}

public class NewsCommentsManager : EntityManager<NewsCommentEntity>, INewsCommentsManager
{
    public NewsCommentsManager(IMongoDataBaseConnectionManager mongoDataBaseConnectionManager) : base(
        mongoDataBaseConnectionManager)
    {
        var indexKey = Builders<NewsCommentEntity>.IndexKeys.Descending(x => x.NewsId);
        var secondIndexKey = Builders<NewsCommentEntity>.IndexKeys.Descending(x => x.CreationDate);
        var combinedIndexKey = Builders<NewsCommentEntity>.IndexKeys.Combine(
            indexKey,
            secondIndexKey
        );
        _collection.Indexes.CreateOne(new CreateIndexModel<NewsCommentEntity>(combinedIndexKey));
    }

    public async Task<List<NewsCommentEntity>> GetCommentsByNewsId(Guid newsId, int limit = 1000, int skip = 0)
    {
        var filter = Builders<NewsCommentEntity>.Filter.Eq(x => x.NewsId, newsId);
        var sort = Builders<NewsCommentEntity>.Sort.Ascending(x => x.CreationDate);
        var result = await _collection.FindAsync(filter, new FindOptions<NewsCommentEntity>()
        {
            Sort = sort,
            Skip = skip,
            Limit = limit,
        });

        return await result.ToListAsync();
    }
}