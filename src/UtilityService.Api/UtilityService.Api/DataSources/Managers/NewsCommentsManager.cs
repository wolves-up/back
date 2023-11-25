using UtilityService.Model.Model.News;

namespace UtilityService.Api.DataSources.Managers;

public class NewsCommentsManager : EntityManager<NewsCommentEntity>
{
    public NewsCommentsManager(IMongoDataBaseConnectionManager mongoDataBaseConnectionManager) : base(mongoDataBaseConnectionManager)
    {
    }
}