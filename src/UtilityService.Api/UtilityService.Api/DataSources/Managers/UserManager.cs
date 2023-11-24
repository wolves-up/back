using MongoDB.Driver;
using UtilityService.Api.DataSources.Model;

namespace UtilityService.Api.DataSources.Managers;

public interface IUserManager : IEntityManager<UserEntity>
{
	Task<UserEntity> GetByEmail(string email);
	Task<UserEntity?> FindByEmail(string email);
}

public class UserManager : EntityManager<UserEntity>, IUserManager
{
	public UserManager(IMongoDataBaseConnectionManager mongoDataBaseConnectionManager)
		: base(mongoDataBaseConnectionManager)
	{
		var indexKey = Builders<UserEntity>.IndexKeys.Ascending(x => x.Requisites.EmailAddress);
		_collection.Indexes.CreateOne(new CreateIndexModel<UserEntity>(indexKey));
	}

	public async Task<UserEntity> GetByEmail(string email)
	{
		var userEntity =
			await _collection.FindAsync(
				new ExpressionFilterDefinition<UserEntity>(x => x.Requisites.EmailAddress == email))
				.ConfigureAwait(false);
		return await userEntity.SingleAsync()
			.ConfigureAwait(false);
	}

	public async Task<UserEntity?> FindByEmail(string email)
	{
		var userEntity =
			await _collection.FindAsync(
					new ExpressionFilterDefinition<UserEntity>(x => x.Requisites.EmailAddress == email))
				.ConfigureAwait(false);
		return await userEntity.SingleOrDefaultAsync()
			.ConfigureAwait(false);
	}
}
