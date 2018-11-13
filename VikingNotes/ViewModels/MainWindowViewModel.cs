using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DAL.Core;
using DAL.Presistence;
using RESTfullWebApi.Models;
using ViewModels.Commands;
using ViewModels.Services.Interfaces;
using ViewModels.Services.Source;

namespace ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ILoginService loginService { get; set; }
        private IUnitOfWork Data { get; set; }

        public ICommand LoadTakeQuizViewCommand { get; private set; }
        public ICommand LoadStatisticsViewCommand { get; private set; }
        public ICommand LoadMakeQuizViewCommand { get; private set; }

        // ViewModel for loginView
        //private BaseViewModel _loginViewModel;

        // ViewModel that is currently bound to the ContentControl
        private BaseViewModel _currentViewModel;

        private BaseViewModel _topBarViewModel;

        public MainWindowViewModel(IUnitOfWork data, ILoginService loginservice, TopBarViewModel topBarVM)
        {
            this.LoadTakeQuizView();
            Data = data;
            loginService = loginservice;
            TopBarViewModel = topBarVM;
         

            // Hook up Commands to associated methods
            this.LoadTakeQuizViewCommand = new DelegateCommand(o => this.LoadTakeQuizView());
            this.LoadStatisticsViewCommand = new DelegateCommand(o => this.LoadStatisticsView());
            this.LoadMakeQuizViewCommand = new DelegateCommand(o => this.LoadMakeQuizView());
        }
        

        public BaseViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                RaisePropertyChanged("CurrentViewModel");
            }
        }

        public BaseViewModel TopBarViewModel
        {
            get { return _topBarViewModel; }
            set
            {
                _topBarViewModel = value;
                RaisePropertyChanged("TopBarViewModel");

            }
        }

        private void LoadTakeQuizView()
        {
            CurrentViewModel = new TakeQuizViewModel();
        }

        private void LoadStatisticsView()
        {
            CurrentViewModel = new StatisticsViewModel();
        }

        private void LoadMakeQuizView()
        {
            CurrentViewModel = new MakeQuizViewModel();
        }
<<<<<<< HEAD

        private void LoadAnswerQuiz()
        {
            CurrentViewModel = new AnswerQuizQuestionViewModel(new Quiz());
        }
=======
>>>>>>> parent of 887cf04... many small changes to work towards getting AnswerQuizQuestionView running
    }
}
