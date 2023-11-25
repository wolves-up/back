using UtilityService.Api.DataSources.Model;

namespace UtilityService.Api.DataSources.Managers;

public interface INewsManager : IEntityManager<NewsEntity>
{
}

public class NewsManager : EntityManager<NewsEntity>, INewsManager
{
    public NewsManager(IMongoDataBaseConnectionManager mongoDataBaseConnectionManager) : base(
        mongoDataBaseConnectionManager)
    {
    }
}