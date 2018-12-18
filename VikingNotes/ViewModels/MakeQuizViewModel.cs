using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using DAL.Core;
using ViewModels.Commands;

namespace ViewModels
{
    public class MakeQuizViewModel : BaseViewModel
    {
        private IUnitOfWork Data;
        private UserControl makeNewQuizView { get; set; }

        public MakeQuizViewModel(IUnitOfWork data, UserControl maNewQuizView)
        {
            Data = data;
            makeNewQuizView = maNewQuizView;
            this.LoadMakeNewQuizView();

            // Hook up Commands to associated methods
            this.LoadMakeNewQuizViewCommand = new Command(o => this.LoadMakeNewQuizView());
            this.LoadEditRemoveQuizViewCommand = new Command(o => this.LoadEditRemoveQuizView());
        }

        public ICommand LoadMakeNewQuizViewCommand { get; private set; }
        public ICommand LoadEditRemoveQuizViewCommand { get; private set; }

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

        private void LoadMakeNewQuizView()
        {
            CurrentViewModel = new MakeNewQuizViewModel(Data);
            makeNewQuizView.DataContext = CurrentViewModel;
        }

        private void LoadEditRemoveQuizView()
        {
            CurrentViewModel = new EditRemoveQuizViewModel();
        }
    }
}
