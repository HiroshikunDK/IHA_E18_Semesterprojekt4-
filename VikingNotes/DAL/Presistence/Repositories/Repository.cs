using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using DAL.Core.Repositories;
using System.Windows;
using DAL.Core;

namespace DAL.Presistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public Repository(ILoginService loginService)
        {
            loginService.UserLoggedIn += SetAuthToken;
        }

        private void SetAuthToken(object o, UserLoggedInEventArg args)
        {
            Client().DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(args.User.AuthToken);
        }

        public static HttpClient Client()
        {
            HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://virkman-001-site1.ctempurl.com/");
            client.BaseAddress = new Uri("http://localhost:57869/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }


        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            string uri = "api/" + typeof(TEntity).Name;
            string responseString = await Client().GetStringAsync(uri);
            var respons = JsonConvert.DeserializeObject<List<TEntity>>(responseString);
            return respons;
        }

        public virtual async Task<TEntity> GetAsync(long id)
        {
            string uri = "api/" + typeof(TEntity).Name + "/" + id;
            string responseString = await Client().GetStringAsync(uri);
            var respons = JsonConvert.DeserializeObject<TEntity>(responseString);
            return respons;
        }

        public virtual async void Update(long id, TEntity entity)
        {
            string uri = "api/" + typeof(TEntity).Name + "/" + id;
            HttpContent content = new StringContent(entity.ToString(), Encoding.UTF8, "application/json");
            await Client().PutAsync(uri, content);
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            string uri = "api/" + typeof(TEntity).Name;
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(entity));
            HttpContent content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await Client().PostAsync(uri, content);
            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                TEntity respons = JsonConvert.DeserializeObject<TEntity>(responseContent);
                return respons;

                // From here on you could deserialize the ResponseContent back again to a concrete C# type using Json.Net
            }
            return null;
        }

        public virtual async void Remove(long id)
        {
            string uri = "api/" + typeof(TEntity).Name + "/" + id;
            await Client().DeleteAsync(uri);
        }
    }
}
