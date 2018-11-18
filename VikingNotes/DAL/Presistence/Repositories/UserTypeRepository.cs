using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DAL.Core;
using DAL.Core.Repositories;
using RESTfullWebApi.Models;

namespace DAL.Presistence.Repositories
{
    public class UserTypeRepository : Repository<UserType>, IUserTypeRepository
    {
        public UserTypeRepository(ILoginService loginService) : base(loginService)
        {
            loginService.UserLoggedIn += SetAuthToken;
        }

        private void SetAuthToken(object o, UserLoggedInEventArg args)
        {
            Client().DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(args.User.AuthToken);
        }
    }
}
