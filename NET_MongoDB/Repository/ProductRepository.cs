using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NET_MongoDB.Config;
using NET_MongoDB.Models;
using NET_MongoDB.Services;

namespace NET_MongoDB.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepo
    {
        private readonly IMongoCollection<Product> _collection;
        private readonly IOptions<DatabaseSetting> _dbSetting;

        public ProductRepository(IOptions<DatabaseSetting> dbSetting) : base(dbSetting)
        {
            _dbSetting = dbSetting;
            var mongoClient = new MongoClient(_dbSetting.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_dbSetting.Value.DatabaseName);
            _collection = mongoDatabase.GetCollection<Product>(_dbSetting.Value.CategoryCollectionName);
        }

        public override async Task<IEnumerable<Product>> GetAllAsync()
        {


            var Pipeline = new BsonDocument[]
            {
                new BsonDocument("$lookup", new BsonDocument()
                {
                    {"$lookup",new BsonDocument
                      {
                        {"from",_dbSetting.Value.CategoryCollectionName },
                        {"localField",nameof(Product.Id) },
                        {"forignField",nameof(Product.CategoryId) },
                        {"as", nameof(Product) },
                      }
                    },
                    {"$project",new BsonDocument
                       {
                        { nameof(Product.Id),1 },
                        { nameof(Product.Name),1 },
                        { nameof(Category.Id),1 },
                        { nameof(Category.Name), $"${nameof(Product)}.{nameof(Category)}.{nameof(Category.Name)}" }
                       }
                    }
                })
            };

            var result= await _collection.Aggregate<Product>(Pipeline).ToListAsync();
            return result;
        }
    }
}
