using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels.Commands;

namespace ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            this.LoadTakeQuizView();

            // Hook up Commands to associated methods
            this.LoadTakeQuizViewCommand = new DelegateCommand(o => this.LoadTakeQuizView());
            this.LoadStatisticsViewCommand = new DelegateCommand(o => this.LoadStatisticsView());
            this.LoadMakeQuizViewCommand = new DelegateCommand(o => this.LoadMakeQuizView());
        }

        public ICommand LoadTakeQuizViewCommand { get; private set; }
        public ICommand LoadStatisticsViewCommand { get; private set; }
        public ICommand LoadMakeQuizViewCommand { get; private set; }

        // ViewModel that is currently bound to the ContentControl
        private BaseViewModel _currentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                this.OnPropertyChanged("CurrentViewModel");
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
    }
}
