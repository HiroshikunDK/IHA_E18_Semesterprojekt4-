using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RESTfullWebApi.Models;

namespace DAL.Core.Repositories
{
    public interface IUserRepository : IRepository<Userr>
    {
        Task<List<Userr>> TryLoginUser(string username, string password);

        new Task<Userr> Add(Userr user);
    }
}
