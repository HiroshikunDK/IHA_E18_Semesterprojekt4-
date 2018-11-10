using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Core.Repositories;
using Newtonsoft.Json;
using RESTfullWebApi.Models;

namespace DAL.Presistence.Repositories
{
    public class UserRepository : Repository<Userr>, IUserRepository
    {
        public async Task<List<Userr>> TryLoginUser(string username, string password)
        {
            string uri = "api/Userr?username=" + username + "&password=" + password;
            string responseString = await Client().GetStringAsync(uri);
            var respons = JsonConvert.DeserializeObject<List<Userr>>(responseString);
            return respons;
        }
    }
}
