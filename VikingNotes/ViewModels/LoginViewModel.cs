using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        private List<Study> studyList { get; set; }
        private UserType userTypeStandard { get; set; }

        private List<string> Usernames { get; set; }

        public LoginViewModel(IUnitOfWork data, ILoginService loginservice)
        {
            loginService = loginservice;
            Data = data;

            Username = "Virkman";
            Password = "nicholas";
            RePassword = "nicholas";
            Email = "nvirkman@gmail.com";
            StudyID = "11786";

            Usernames = new List<string>();

            GetStudyList();
            GetStandardUserType();
            GetUsernames();

            IsRegistrering = false;
            ButtonContent = "Login";
            CheckBoxClick = new Command(CheckBoxClickFunc, canExecute);
            ButtonClick = new Command(ButtonClickFunc, CanButtonExecute); 
            
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public void UpdateCanExecute(object sender, KeyEventArgs e)
        {

        }

        private async void ButtonClickFunc(object obj)
        {
            if (IsRegistrering)
            {
                if (Usernames.Count == 0)
                {
                    MessageBox.Show("Undersøger om username er ledig.\nHvis dette gentager sig, kan der mangle forbindelse til serveren");
                    return;
                }
                foreach (var names in Usernames)
                {
                    if (Username == names.Trim())
                    {
                        MessageBox.Show("Username " + Username + " exists!");
                        return;
                    }
                }
                Userr newUser = new Userr();
                newUser.UserName = username;
                newUser.Password = password;
                newUser.EmailAdress = email;
                newUser.StudyID = study.StudyID;
                newUser.StudentNumber = studyID;
                newUser.UserTypeID = userTypeStandard.UserTypeID;

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

        static string pattern = "[0-9a-zA-Z]";
        Regex regex = new Regex(pattern);

        public string Username
        {
            get { return username; }
            set
            {
                RaisePropertyChanged("Username");

                if (value.Length <= 3)
                {
                    List<string> errors = new List<string>
                    {
                        "Username is min. 4 char or letters!"
                    };
                    SetErrors("Username", errors);
                   
                }
                else if (Usernames != null && IsRegistrering)
                {
                    foreach (var _username in Usernames)
                    {
                        if (value == _username.Trim())
                        {
                            List<string> errors = new List<string>
                            {
                                "Username exists!"
                            };
                            SetErrors("Username", errors);
                            break;
                        }
                        ClearErrors("Username");
                    }

                }

                if (!IsRegistrering)
                {
                    ClearErrors("Username");
                }
                username = value;        
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                RaisePropertyChanged("Password");
                if (value.Length < 4)
                {
                    List<string> errors = new List<string>
                    {
                        "Password is min. 4 symbols!"
                    };
                    SetErrors("Password", errors);
                }
                else
                {
                    ClearErrors("Password");
                }
                password = value;
            }
        }
        public string RePassword
        {
            get { return rePassword; }
            set
            {
                
                RaisePropertyChanged("RePassword");
                if (value == password)
                {
                    ClearErrors("RePassword");
                }
                else
                {
                    List<string> errors = new List<string>
                    {
                        "Password dont match!"
                    };
                    SetErrors("RePassword", errors);
                }
                rePassword = value;
            }
        }
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                RaisePropertyChanged("Email");
                if (value.Contains("@") && value.Contains("."))
                {
                    ClearErrors("Email");
                }
                else
                {
                    List<string> errors = new List<string>
                    {
                        "Email is not valid!"
                    };
                    SetErrors("Email", errors);
                }
            }
        }

        public Study Study
        {
            get { return study; }
            set
            {
                study = value;
                RaisePropertyChanged("Study");
                if (Study == null)
                {
                    List<string> errors = new List<string>
                    {
                        "Study needs to be selceted!"
                    };
                    SetErrors("Study", errors);
                }
                ClearErrors("Study");
            }
        }
        public string StudyID
        {
            get { return studyID; }
            set
            {
                studyID = value;
                RaisePropertyChanged("StudyID");
                foreach (char c in value)
                {
                    if (c < '0' || c > '9')
                    {
                        List<string> errors = new List<string>
                        {
                            "StudyID kan only be in numbers!"
                        };
                        SetErrors("StudyID", errors);
                        break;
                    }
                    ClearErrors("StudyID");
                }
            }
        }
        public List<Study> StudyList
        {
            get { return studyList; }
            set
            {
                studyList = value;
                RaisePropertyChanged("StudyList");
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

        public async void GetStudyList()
        {
            StudyList = await Data.Study.GetAllAsync();
        }

        public async void GetStandardUserType()
        {
            userTypeStandard = await Data.UserType.GetAsync(1);
        }

        public async void GetUsernames()
        {
            foreach (var users in await Data.User.GetAllAsync())
            {
                Usernames.Add(users.UserName);
            }
        }

    }
}
