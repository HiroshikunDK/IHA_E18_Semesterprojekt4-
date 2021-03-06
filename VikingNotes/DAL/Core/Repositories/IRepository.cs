﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetAsync(long id);
        Task<List<TEntity>> GetAllAsync();
        Task<HttpResponseMessage> Update(long id, TEntity entity);
        Task<TEntity> Add(TEntity entity);
        void Remove(long id);

        HttpClient Client();
        void SetAuthToken(string authT);
    }
}
