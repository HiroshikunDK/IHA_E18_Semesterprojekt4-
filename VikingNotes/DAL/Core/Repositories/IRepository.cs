using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetAsync(long id);
        Task<List<TEntity>> GetAllAsync();
        void Update(long id, TEntity entity);
        Task<TEntity> Add(TEntity entity);
        void Remove(long id);
    }
}
