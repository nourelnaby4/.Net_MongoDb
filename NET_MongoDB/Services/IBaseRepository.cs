using NET_MongoDB.Models;
using System.Linq.Expressions;

namespace NET_MongoDB.Services
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetById(Expression<Func<T, bool>> predict);
        Task CreateAsync(T category);
        Task UpdateAsync(Expression<Func<T, bool>> predict,T category);
        Task DeleteAsync(Expression<Func<T, bool>> predict);
    }
}
