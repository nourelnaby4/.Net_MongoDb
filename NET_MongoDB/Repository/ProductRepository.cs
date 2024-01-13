using Microsoft.Extensions.Options;
using NET_MongoDB.Config;
using NET_MongoDB.Models;
using NET_MongoDB.Services;

namespace NET_MongoDB.Repository
{
    public class ProductRepository : BaseRepository<Product>, IBaseRepository<Product>
    {
        public ProductRepository(IOptions<DatabaseSetting> dbSetting) : base(dbSetting)
        {
        }
    }
}
