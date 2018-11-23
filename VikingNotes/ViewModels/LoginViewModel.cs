using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private bool isRegistrering { get; set; }
        private string buttonContent { get; set; }

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
        private List<string> Emails { get; set; }
        private List<string> StudentNumbers{ get; set; }

        public LoginViewModel(IUnitOfWork data)
        {
            Data = data;

            Username = "TesterGuy";
            Password = "12341234";
            RePassword = "12341234";
            Email = "testtest2@test.test";
            StudyID = "342424234";
            Study = new Study();

            Usernames = new List<string>();
            StudentNumbers = new List<string>();
            Emails = new List<string>();

            GetStudyList();
            GetStandardUserType();
            GetUsers();

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

                Userr responsUser = await Data.User.Add(newUser);
                if (responsUser.UserID == 0)
                {
                    MessageBox.Show("User unsuccefully registrede! Check the connection to the server,\nVikingNote will start up in anonymous mode");
                }
                MessageBox.Show("User succefully registrede! Welcome " + username + ", dit userID er " + responsUser.UserID +" du logges nu ind");
                Data.LoginService.User = responsUser;
                return;
            }
            Data.LoginService.VertifyLogin(username, password);

        }

        private bool CanButtonExecute(object parameter)
        {
            if (IsRegistrering)
            {
                if (StudyID != null)
                {
                    if (StudyID.Length >= 4 && Password.Length >= 3 && Password.Length >= 8 && Password == RePassword && Email.Contains("@") &&
                        Email.Contains(".") && Study != null)
                    {
                        return true;
                    }
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
                        if (value == _username)
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
                if (!(value.Contains("@") && value.Contains(".")))
                {
                    List<string> errors = new List<string>
                    {
                        "Email is not valid!"
                    };
                    SetErrors("Email", errors);
                    return;
                    
                }
                else if (Emails != null && IsRegistrering)
                {
                    foreach (var _email in Emails)
                    {
                        if (value == _email)
                        {
                            List<string> errors = new List<string>
                            {
                                "Email exists!"
                            };
                            SetErrors("Email", errors);
                            break;
                        }
                        ClearErrors("Email");
                    }

                }

                if (!IsRegistrering)
                {
                    ClearErrors("Email");
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
                        return;
                    }
                }
                if (StudentNumbers != null && IsRegistrering)
                {
                    foreach (var _studentNos in StudentNumbers)
                    {
                        if (value == _studentNos)
                        {
                            List<string> errors = new List<string>
                            {
                                "StudyID exists!"
                            };
                            SetErrors("StudyID", errors);
                            break;
                        }
                        ClearErrors("StudyID");
                    }
                }
                if (!IsRegistrering)
                {
                    ClearErrors("StudyID");
                }
                studyID = value;
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

        public async void GetUsers()
        {
            foreach (var users in await Data.User.GetAllAsync())
            {
                Usernames.Add(users.UserName.Trim());
                StudentNumbers.Add(users.StudentNumber.Trim());
                Emails.Add(users.EmailAdress.Trim());
            }
        }

    }
}
