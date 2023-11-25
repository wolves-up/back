using MongoDB.Driver;
using UtilityService.Api.DataSources.Model;

namespace UtilityService.Api.DataSources.Managers;

public interface IEntityManager<T> where T : IEntity
{
	public Task Add(T entity);
	public Task<T> GetById(Guid id);
	public Task DeleteById(Guid id);
	public Task Update(T entity);
	public Task<List<T>> FindByIds(Guid[] ids);
	public Task<List<T>> GetAll();
}

public class EntityManager<T> : IEntityManager<T>
	where T : IEntity
{
	public EntityManager(IMongoDataBaseConnectionManager mongoDataBaseConnectionManager)
	{
		var database = mongoDataBaseConnectionManager.MongoDatabase;
		_collection = database.GetCollection<T>(typeof(T).FullName);
	}

	public async Task Add(T entity)
	{
		await _collection.InsertOneAsync(entity).ConfigureAwait(false);
	}

	public async Task<T> GetById(Guid id)
	{
		return await (await _collection.FindAsync(x => x.Id == id).ConfigureAwait(false))
			.SingleAsync().ConfigureAwait(false);
	}

	public async Task DeleteById(Guid id)
	{
		var result = await _collection.DeleteOneAsync(x => x.Id == id).ConfigureAwait(false);
		if (result.DeletedCount != 0)
			throw new KeyNotFoundException($"Не найден элемент с номером {id}");
	}

	public async Task Update(T entity)
	{
		await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity).ConfigureAwait(false);
	}

	public async Task<List<T>> FindByIds(Guid[] ids)
	{
		var result = await _collection.FindAsync(x => ids.Contains(x.Id)).ConfigureAwait(false);
		return await result.ToListAsync().ConfigureAwait(false);
	}

	public async Task<List<T>> GetAll()
	{
		var result = await _collection.FindAsync(Builders<T>.Filter.Empty).ConfigureAwait(false);
		return await result.ToListAsync().ConfigureAwait(false);
	}

	protected readonly IMongoCollection<T> _collection;
}