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
using ViewModels.Services.Interfaces;
using ViewModels.Services.Source;

namespace View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private LoginService loginService { get; set; }
        private LoginView loginView { get; set; }
        private LoginViewModel loginVM { get; set; }
        private TopBarView topBarView { get; set; }
        private TopBarViewModel topBarVM { get; set; }
        private MainWindow mainWindow { get; set; }
        private MainWindowViewModel mainWindowVM { get; set; }
        private IUnitOfWork Data { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            // Run startup code first
            base.OnStartup(e);
            Data = new UnitOfWork();

            // Create Windows
            loginView = new LoginView();
            loginService = new LoginService(Data);
            loginVM = new LoginViewModel(Data, loginService);

            topBarView = new TopBarView();
            topBarVM = new TopBarViewModel(loginService);
            topBarView.DataContext = topBarVM;

            mainWindow = new MainWindow();
            mainWindowVM = new MainWindowViewModel(Data, loginService, topBarVM);
            mainWindow.DataContext = mainWindowVM;

            loginService.UserLoggedIn += LoginSuccesfull;
            loginService.UserFailedToLogIn += LoginFailed;


            loginView.DataContext = loginVM;
            loginView.Show();           
            
        }

        public void LoginSuccesfull(object o, UserLoggedInEventArg args)
        {
            loginView.Close();
            mainWindow.Show();
        }

        public void LoginFailed(object o, EventArgs args)
        {
            MessageBox.Show("Username or password is wrong!");
        }
    }
}
