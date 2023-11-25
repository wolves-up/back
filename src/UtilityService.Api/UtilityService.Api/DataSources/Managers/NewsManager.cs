using UtilityService.Api.DataSources.Model;

namespace UtilityService.Api.DataSources.Managers;

public class NewsManager : EntityManager<NewsEntity>
{
    public NewsManager(IMongoDataBaseConnectionManager mongoDataBaseConnectionManager) : base(mongoDataBaseConnectionManager)
    {
    }
}