using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Get(int id);
        Task<IQueryable<TEntity>> GetAll();
        void Update(int id, TEntity entity);
        void Add(TEntity entity);
        void Remove(int id);
    }
}
