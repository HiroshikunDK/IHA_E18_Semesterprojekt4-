﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Core;
using DAL.Presistence.Repositories;
using RESTfullWebApi.Models;

namespace DAL.Presistence
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
            user = new Userr();
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
            //UserLoggedIn?.Invoke(this, new UserLoggedInEventArg(this, userList.FirstOrDefault()));
            User = userList.FirstOrDefault();
        }
    }
}
