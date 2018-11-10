using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RESTfullWebApi.Models;

namespace ViewModels.Services.Interfaces
{
    public class UserLoggedInEventArg : EventArgs
    {
        public Userr User { get; set; }
        public ILoginService self { get; set; }

        public UserLoggedInEventArg(ILoginService sef, Userr user)
        {
            self = sef;
            User = user;
        }
    }

    public interface ILoginService
    {
        event EventHandler<UserLoggedInEventArg> UserLoggedIn;
        event EventHandler<EventArgs> UserFailedToLogIn;

        Userr User { get; set; }
        void VertifyLogin(string username, string password);
    }
}
