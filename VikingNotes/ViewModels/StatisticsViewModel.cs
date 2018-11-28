using DAL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels.Commands;

namespace ViewModels
{
    public class StatisticsViewModel : BaseViewModel
    {
        public StatisticsViewModel(IUnitOfWork data)
        {
            Data = data;
            this.LoadAllStatisticsView();

            // Hook up Commands to associated methods
            this.LoadAllStatisticsViewCommand = new DelegateCommand(o => this.LoadAllStatisticsView());
            this.LoadYourStatisticsViewCommand = new DelegateCommand(o => this.LoadYourStatisticsView());
        }
        private IUnitOfWork Data;
        public ICommand LoadAllStatisticsViewCommand { get; private set; }
        public ICommand LoadYourStatisticsViewCommand { get; private set; }

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

        private void LoadAllStatisticsView()
        {
            CurrentViewModel = new AllStatisticsViewModel();
        }

        private void LoadYourStatisticsView()
        {
            CurrentViewModel = new YourStatisticsViewModel(Data);
        }
    }
}
