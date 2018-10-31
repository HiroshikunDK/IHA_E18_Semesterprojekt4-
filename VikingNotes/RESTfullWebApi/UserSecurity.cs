using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RESTfullWebApi.Models;

namespace RESTfullWebApi
{
    public class UserSecurity
    {
        public static bool Login(string username, string password)
        {
            using (VikingNoteDBEntities entities = new VikingNoteDBEntities())
            {
                return entities.Userrs.Any(user => user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                                                   user.Password == password);
            }
        }
    }
}