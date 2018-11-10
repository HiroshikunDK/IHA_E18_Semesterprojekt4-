using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using DAL.Core;
using DAL.Presistence;
using RESTfullWebApi.Models;
using ViewModels.Commands;
using ViewModels.Services.Interfaces;
using ViewModels.Services.Source;

namespace ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private bool isRegistrering { get; set; }
        private string buttonContent { get; set; }

        private ILoginService loginService { get; set; }
        private IUnitOfWork Data { get; set; }

        public ICommand CheckBoxClick { get; set; }
        public ICommand ButtonClick { get; set; }

        private string username { get; set; }
        private string password { get; set; }
        private string rePassword { get; set; }
        private string email { get; set; }
        private Study study { get; set; }
        private string studyID { get; set; }

        public LoginViewModel(IUnitOfWork data, ILoginService loginservice)
        {
            Username = "";
            Password = "";
            RePassword = "";
            Email = "";
            StudyID = "";

            loginService = loginservice;
            IsRegistrering = false;
            ButtonContent = "Login";
            CheckBoxClick = new Command(CheckBoxClickFunc, canExecute);
            ButtonClick = new Command(ButtonClickFunc, CanButtonExecute); 
            Data = data;
        }

        public void UpdateCanExecute(object sender, KeyEventArgs e)
        {

        }

        private async void ButtonClickFunc(object obj)
        {
            if (IsRegistrering)
            {
                Userr newUser = new Userr();
                newUser.UserName = username;
                newUser.Password = password;
                newUser.EmailAdress = email;
                newUser.Study = study;
                newUser.StudentNumber = studyID;
                newUser.UserTypeID = 1;

                Data.User.Add(newUser);
                MessageBox.Show("User succefully registrede! Welcome " + username + ", du logges nu ind");
                loginService.User = newUser;
                return;
            }
            loginService.VertifyLogin(username, password);

        }

        private bool CanButtonExecute(object parameter)
        {
            if (IsRegistrering)
            {
                if (StudyID.ToString().Length >= 4 && Password.Length >= 3 && Password.Length >= 8 && Password == RePassword && Email.Contains("@") &&
                    Email.Contains(".") && Study != null)
                {
                    return true;
                }
            }
            else if(Username.Length >= 3 && password.Length >= 8 )
            {
                return true;
            }
            return false;
        }


        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                RaisePropertyChanged("Username");
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                RaisePropertyChanged("Password");
            }
        }
        public string RePassword
        {
            get { return rePassword; }
            set
            {
                rePassword = value;
                RaisePropertyChanged("RePassword");
            }
        }
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                RaisePropertyChanged("Email");
            }
        }

        public Study Study
        {
            get { return study; }
            set
            {
                study = value;
                RaisePropertyChanged("Study");
            }
        }
        public string StudyID
        {
            get { return studyID; }
            set
            {
                studyID = value;
                RaisePropertyChanged("StudyID");
            }
        }

        private void CheckBoxClickFunc(object obj)
        {
            ButtonContent = isRegistrering ? "Registrer" : "Login";
        }

        private bool canExecute(object parameter)
        {
            return true;
        }

        public bool IsRegistrering
        {
            get { return isRegistrering; }
            set { isRegistrering = value;
                RaisePropertyChanged("IsRegistrering");
            }
        }

        public string ButtonContent
        {
            get { return buttonContent; }
            set
            {
                buttonContent = value;
                RaisePropertyChanged("ButtonContent");
            }
        }
    }
}
