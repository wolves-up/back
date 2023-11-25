using UtilityService.Model.Model.News;

namespace UtilityService.Api.DataSources.Managers;

public interface INewsCommentsManager : IEntityManager<NewsCommentEntity>
{
}

public class NewsCommentsManager : EntityManager<NewsCommentEntity>, INewsCommentsManager
{
    public NewsCommentsManager(IMongoDataBaseConnectionManager mongoDataBaseConnectionManager) : base(
        mongoDataBaseConnectionManager)
    {
    }
}