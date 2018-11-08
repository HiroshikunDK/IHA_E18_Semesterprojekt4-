using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Core.Repositories;
using RESTfullWebApi.Models;

namespace DAL.Presistence.Repositories
{
    public class UserRepository : Repository<Userr>, IUserRepository
    {
    }
}
