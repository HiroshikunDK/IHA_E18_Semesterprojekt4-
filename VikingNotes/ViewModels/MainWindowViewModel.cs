using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using DAL.Core;
using DAL.Presistence;
using RESTfullWebApi.Models;
using ViewModels.Commands;

namespace ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private TopBarViewModel topBarVM { get; set; }

        private IUnitOfWork Data { get; set; }

        public ICommand LoadTakeQuizViewCommand { get; private set; }
        public ICommand LoadStatisticsViewCommand { get; private set; }
        public ICommand LoadMakeQuizViewCommand { get; private set; }

        // ViewModel for loginView
        //private BaseViewModel _loginViewModel;

        // ViewModel that is currently bound to the ContentControl
        private BaseViewModel _currentViewModel;

        private BaseViewModel _topBarViewModel;

        private List<ViewBase> viewListe { get; set; }

        private UserControl answerView { get; set; }

        private UserControl takeView { get; set; }

        private UserControl topBarView { get; set; }

        private UserControl makeQuizView { get; set; }

        private UserControl makeNewQuizView { get; set; }

        private TakeQuizViewModel takeQuizVM;

        private bool isDoingQuiz = false;

        public MainWindowViewModel(IUnitOfWork data, UserControl topView, UserControl ansView, UserControl taView, UserControl maQuizView, UserControl maNewQuizView)
        {
            Data = data;
            topBarView = topView;
            answerView = ansView;
            takeView = taView;
            makeQuizView = maQuizView;
            makeNewQuizView = maNewQuizView;

            topBarVM = new TopBarViewModel(Data);
            topBarView.DataContext = topBarVM;
            _topBarViewModel = topBarVM;

            takeQuizVM = new TakeQuizViewModel(answerView);
            takeQuizVM.IsDoingQuiz += IsDoingQuiz;
            this.LoadTakeQuizView();


            // Hook up Commands to associated methods
            //this.LoadTakeQuizViewCommand = new DelegateCommand(o => this.LoadTakeQuizView());
            //this.LoadStatisticsViewCommand = new DelegateCommand(o => this.LoadStatisticsView());
            //this.LoadMakeQuizViewCommand = new DelegateCommand(o => this.LoadMakeQuizView());

            this.LoadTakeQuizViewCommand = new Command(LoadTakeQuizView, canExecute);
            this.LoadStatisticsViewCommand = new Command(LoadStatisticsView, canExecute);
            this.LoadMakeQuizViewCommand = new Command(LoadMakeQuizView, canExecute);

        }

        private bool canExecute(object parameter)
        {
            return !isDoingQuiz;
        }

        private void IsDoingQuiz(object o, EventArgs args)
        {
            isDoingQuiz = true;
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

        private void LoadTakeQuizView(object o = null)
        {
            CurrentViewModel = takeQuizVM;
            takeView.DataContext = CurrentViewModel;
        }

        private void LoadStatisticsView(object o)
        {
            CurrentViewModel = new StatisticsViewModel();
        }

        private void LoadMakeQuizView(object o)
        {
            CurrentViewModel = new MakeQuizViewModel(Data, makeNewQuizView);
            makeQuizView.DataContext = CurrentViewModel;
        }
    }
}
