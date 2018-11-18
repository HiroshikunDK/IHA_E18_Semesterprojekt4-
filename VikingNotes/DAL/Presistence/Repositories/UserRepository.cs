using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DAL.Core.Repositories;
using Newtonsoft.Json;
using RESTfullWebApi.Models;

namespace DAL.Presistence.Repositories
{
    public class UserRepository : Repository<Userr>, IUserRepository
    {
        public async Task<List<Userr>> TryLoginUser(string username, string password)
        {
            string uri = "api/Userr?username=" + username + "&password=" + GetHash.SHA1(password);
            string responseString = await Client().GetStringAsync(uri);
            var respons = JsonConvert.DeserializeObject<List<Userr>>(responseString);
            return respons;
        }

        public override async Task<Userr> Add(Userr user)
        {
            string uri = "api/Userr";
            user.Password = GetHash.SHA1(user.Password);
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(user));
            HttpContent content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await Client().PostAsync(uri, content);
            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                Userr respons = JsonConvert.DeserializeObject<Userr>(responseContent);
                return respons;

                // From here on you could deserialize the ResponseContent back again to a concrete C# type using Json.Net
            }
            return null;
        }
        public async Task<List<Quiz>> GetQuizzesByUserID(long UserID)
        {
            string uri = "api/Quiz?UserID=" + UserID.ToString();
            string responseString = await Client().GetStringAsync(uri);
            var respons = JsonConvert.DeserializeObject<List<Quiz>>(responseString);
            return respons;
        }
    }
}
