using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using DAL.Core;
using RESTfullWebApi.Models;

namespace ViewModels
{
    public class TopBarViewModel : BaseViewModel
    {
        //public event PropertyChangedEventHandler PropertyChanged;
        private DateTime _now;
        private Userr user { get; set; }
        private string username { get; set; }
        private IUnitOfWork Data;

        public TopBarViewModel(IUnitOfWork data)
        {
            _now = DateTime.Now;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            data.LoginService.UserLoggedIn += SetUser;
        }

        public DateTime CurrentDateTime
        {
            get { return _now; }
            private set
            {
                _now = value;
                RaisePropertyChanged("CurrentDateTime");
            }
        }

        public Userr User
        {
            get { return user; }
            private set
            {
                user = value;
                RaisePropertyChanged("User");
                Username = user.UserName;
            }
        }

        public string Username
        {
            get { return username; }
            private set
            {
                username = value;
                RaisePropertyChanged("Username");
            }
        }


        void timer_Tick(object sender, EventArgs e)
        {
            CurrentDateTime = DateTime.Now;
        }

        void SetUser(object sender, UserLoggedInEventArg args)
        {
            User = args.User;
        }

    }
}
