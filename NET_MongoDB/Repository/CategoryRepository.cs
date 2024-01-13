using Microsoft.Extensions.Options;
using NET_MongoDB.Config;
using NET_MongoDB.Models;
using NET_MongoDB.Services;

namespace NET_MongoDB.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepo
    {
        public CategoryRepository(IOptions<DatabaseSetting> dbSetting) : base(dbSetting)
        {
        }
    }
}
