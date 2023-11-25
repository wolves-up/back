using System.Linq.Expressions;
using MongoDB.Driver;
using UtilityService.Api.DataSources.Model;

namespace UtilityService.Api.DataSources.Managers;

public interface IReportManager : IEntityManager<ReportEntity>
{
	Task<List<ReportEntity>> FindBy(FilterDefinition<ReportEntity> filter, int limit = 1000, int skip = 0);
	Task<List<ReportEntity>> FindByUserId(Guid userId);
	Task<List<ReportEntity>> FindByUtilityServiceId(Guid serviceId);
}

public class ReportManager : EntityManager<ReportEntity>, IReportManager
{
	public ReportManager(IMongoDataBaseConnectionManager mongoDataBaseConnectionManager) 
		: base(mongoDataBaseConnectionManager)
	{
		var indexes = new[]
		{
			new CreateIndexModel<ReportEntity>(Builders<ReportEntity>.IndexKeys.Ascending(x => x.CreationDate)),
			new CreateIndexModel<ReportEntity>(Builders<ReportEntity>.IndexKeys.Ascending(x => x.ResponsibleServiceId)),
			new CreateIndexModel<ReportEntity>(Builders<ReportEntity>.IndexKeys.Ascending(x => x.UserId)),
		};
		_collection.Indexes.CreateMany(indexes);
	}

	public async Task<List<ReportEntity>> FindBy(FilterDefinition<ReportEntity> filter, int limit = 1000, int skip = 0)
	{
		var result = await _collection.FindAsync(filter, new FindOptions<ReportEntity>()
		{
			Sort = Builders<ReportEntity>.Sort.Ascending(x => x.CreationDate),
			Limit = limit,
			Skip = skip
		}).ConfigureAwait(false);
		return await result.ToListAsync().ConfigureAwait(false);
	}

	public async Task<List<ReportEntity>> FindByUserId(Guid userId)
	{
		var result = await _collection.FindAsync(x => x.UserId == userId).ConfigureAwait(false);
		return await result.ToListAsync().ConfigureAwait(false);
	}

	public async Task<List<ReportEntity>> FindByUtilityServiceId(Guid serviceId)
	{
		var result = await _collection.FindAsync(x => x.ResponsibleServiceId == serviceId).ConfigureAwait(false);
		return await result.ToListAsync().ConfigureAwait(false);
	}
}
