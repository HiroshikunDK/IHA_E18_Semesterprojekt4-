using DAL.Presistence;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DAL.Core;
using View.Views;
using ViewModels;

namespace View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private LoginView loginView { get; set; }
        private LoginViewModel loginVM { get; set; }

        private TopBarView topBarView { get; set; }

        private MainWindow mainWindow { get; set; }
        private MainWindowViewModel mainWindowVM { get; set; }
        private IUnitOfWork Data { get; set; }

        private AnswerQuizQuestionView answerView { get; set; }
        private TakeQuizView takeView { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            // Run startup code first
            base.OnStartup(e);
            Data = new UnitOfWork();

            // Create Windows
            loginView = new LoginView();
            loginVM = new LoginViewModel(Data);

            topBarView = new TopBarView();

            answerView = new AnswerQuizQuestionView();

            takeView = new TakeQuizView();

            

            mainWindow = new MainWindow();
            mainWindowVM = new MainWindowViewModel(Data, topBarView, answerView, takeView);
            mainWindow.DataContext = mainWindowVM;

            Data.LoginService.UserLoggedIn += LoginSuccesfull;
            Data.LoginService.UserFailedToLogIn += LoginFailed;


            loginView.DataContext = loginVM;
            loginView.Show();           
            
        }

        public void LoginSuccesfull(object o, UserLoggedInEventArg args)
        {
            loginView.Close();
            string outPut = "Login Sucessfully completed!\nWelcome to VikingNote 1.0 " + args.User.UserName + "\nYou have made " +
                            args.User.Quizs.Count + " quizzes so far";
            MessageBox.Show(outPut);
            mainWindow.Show();
        }

        public void LoginFailed(object o, EventArgs args)
        {
            MessageBox.Show("Username or password is wrong!");
        }
    }
}
