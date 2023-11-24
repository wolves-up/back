using MongoDB.Driver;
using UtilityService.Api.DataSources.Model;

namespace UtilityService.Api.DataSources.Managers;

public interface IUtilityServiceManager : IEntityManager<UtilityServiceEntity>
{
	Task<List<UtilityServiceEntity>> FindByType(string type);
	Task<UtilityServiceEntity> FindByInn(string inn);
}

public class UtilityServiceManager : EntityManager<UtilityServiceEntity>, IUtilityServiceManager
{
	public UtilityServiceManager(IMongoDataBaseConnectionManager mongoDataBaseConnectionManager) 
		: base(mongoDataBaseConnectionManager)
	{
		var indexes = new[]
		{
			new CreateIndexModel<UtilityServiceEntity>(Builders<UtilityServiceEntity>.IndexKeys.Ascending(x => x.Inn)),
			new CreateIndexModel<UtilityServiceEntity>(Builders<UtilityServiceEntity>.IndexKeys.Ascending(x => x.Type)),
		};
		_collection.Indexes.CreateMany(indexes);
	}


	public async Task<List<UtilityServiceEntity>> FindByType(string type)
	{
		var result = await _collection
			.FindAsync(x => x.Type == type)
			.ConfigureAwait(false);
		return await result.ToListAsync()
			.ConfigureAwait(false);
	}

	public async Task<UtilityServiceEntity> FindByInn(string inn)
	{
		var result = await _collection
			.FindAsync(x => x.Inn == inn)
			.ConfigureAwait(false);
		return await result.SingleAsync()
			.ConfigureAwait(false);
	}
}
