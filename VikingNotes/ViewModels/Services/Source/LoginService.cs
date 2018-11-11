using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Core;
using RESTfullWebApi.Models;
using ViewModels.Services.Interfaces;

namespace ViewModels.Services.Source
{
    public class LoginService : ILoginService
    {
        private IUnitOfWork Data;
        private Userr user { get; set; }

        public event EventHandler<UserLoggedInEventArg> UserLoggedIn;

        public event EventHandler<EventArgs> UserFailedToLogIn;

        public LoginService(IUnitOfWork data)
        {
            Data = data;
        }

        public Userr User
        {
            get { return user; }
            set
            {
                user = value;
                UserLoggedIn?.Invoke(this, new UserLoggedInEventArg(this, user));
            }
        }

      

        public async void VertifyLogin(string username, string password)
        {
            List<Userr> userList = await Data.User.TryLoginUser(username, password);
            if (userList.Count == 0)
            {
                UserFailedToLogIn?.Invoke(this, EventArgs.Empty);
                return;
            }
            User = userList.FirstOrDefault();
            UserLoggedIn?.Invoke(this, new UserLoggedInEventArg(this, User));
        }
    }
}
