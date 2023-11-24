using MongoDB.Driver;
using UtilityService.Api.DataSources.Model;

namespace UtilityService.Api.DataSources.Managers;

public interface IReportCommentManager : IEntityManager<ReportCommentEntity>
{
	Task<List<ReportCommentEntity>> GetCommentsByReportId(Guid reportId, int limit = 1000, int skip = 0);
}

public class ReportCommentManager : EntityManager<ReportCommentEntity>, IReportCommentManager
{
	public ReportCommentManager(IMongoDataBaseConnectionManager mongoDataBaseConnectionManager) 
		: base(mongoDataBaseConnectionManager)
	{
		var indexKey = Builders<ReportCommentEntity>.IndexKeys.Descending(x => x.ReportId);
		var secondIndexKey = Builders<ReportCommentEntity>.IndexKeys.Descending(x => x.CreationDate);
		var combinedIndexKey = Builders<ReportCommentEntity>.IndexKeys.Combine(
			indexKey,
			secondIndexKey
		);
		_collection.Indexes.CreateOne(new CreateIndexModel<ReportCommentEntity>(combinedIndexKey));
	}


	public async Task<List<ReportCommentEntity>> GetCommentsByReportId(Guid reportId, int limit = 1000, int skip = 0)
	{
		var filter = Builders<ReportCommentEntity>.Filter.Eq(x => x.ReportId, reportId);
		var sort = Builders<ReportCommentEntity>.Sort.Ascending(x => x.CreationDate);
		var result = await _collection.FindAsync(filter, new FindOptions<ReportCommentEntity>()
		{
			Sort = sort,
			Skip = skip,
			Limit = limit,
		}).ConfigureAwait(false);

		return await result.ToListAsync()
			.ConfigureAwait(false);
	}
}
