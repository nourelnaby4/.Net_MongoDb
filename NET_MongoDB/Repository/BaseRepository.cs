using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NET_MongoDB.Config;
using NET_MongoDB.Models;
using NET_MongoDB.Services;
using System.Linq.Expressions;

namespace NET_MongoDB.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;
        private readonly IOptions<DatabaseSetting> _dbSetting;

        public BaseRepository(IOptions<DatabaseSetting> dbSetting)
        {
            _dbSetting = dbSetting;
            var mongoClient = new MongoClient(_dbSetting.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_dbSetting.Value.DatabaseName);
            _collection = mongoDatabase.GetCollection<T>(_dbSetting.Value.CategoryCollectionName);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync() =>
            await _collection.Find(_ => true).ToListAsync();

        public virtual async Task<T> GetById(Expression<Func<T, bool>> predict) =>
            await _collection.Find(predict).FirstOrDefaultAsync();

        public virtual async Task CreateAsync(T collection) =>
            await _collection.InsertOneAsync(collection);

        public virtual async Task UpdateAsync(Expression<Func<T, bool>> predict, T collection) =>
            await _collection.ReplaceOneAsync(predict, collection);

        public virtual async Task DeleteAsync(Expression<Func<T, bool>> predict) =>
            await _collection.DeleteOneAsync(predict);



    }
}
