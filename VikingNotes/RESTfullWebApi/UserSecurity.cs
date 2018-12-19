using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RESTfullWebApi.Models;

namespace RESTfullWebApi
{
    public class UserSecurity
    {
        public static Userr Authentication(string authToken)
        {
            using (VikingNoteDBEntities entities = new VikingNoteDBEntities())
            {
                return entities.Userrs.FirstOrDefault(user => user.AuthToken == authToken);
            }
        }
    }
}