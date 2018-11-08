using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using DAL.Core.Repositories;

namespace DAL.Presistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public static HttpClient Client()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:57869/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }


        public async Task<IQueryable<TEntity>> GetAll()
        {
            string uri = "api/" + typeof(TEntity).Name;
            string responseString = await Client().GetStringAsync(uri);
            var respons = JsonConvert.DeserializeObject<IQueryable<TEntity>>(responseString);
            return respons;
        }

        public async Task<TEntity> Get(int id)
        {
            Uri uri = new Uri("api/" + typeof(TEntity).Name + "/" + id);
            string responseString = await Client().GetStringAsync(uri);
            var respons = JsonConvert.DeserializeObject<TEntity>(responseString);
            return respons;
        }

        public async void Update(int id, TEntity entity)
        {
            Uri uri = new Uri("api/" + typeof(TEntity).Name + "/" + id);
            HttpContent content = new StringContent(entity.ToString(), Encoding.UTF8, "application/json");
            await Client().PutAsync(uri, content);
        }

        public async void Add(TEntity entity)
        {
            Uri uri = new Uri("api/" + typeof(TEntity).Name);
            HttpContent content = new StringContent(entity.ToString(), Encoding.UTF8, "application/json");
            await Client().PostAsync(uri, content);
        }

        public async void Remove(int id)
        {
            Uri uri = new Uri("api/" + typeof(TEntity).Name + "/" + id);
            await Client().DeleteAsync(uri);
        }
    }
}
