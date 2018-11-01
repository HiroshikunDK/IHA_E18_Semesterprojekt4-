using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels.Commands;

namespace ViewModels
{
    public class MakeQuizViewModel : BaseViewModel
    {
        public MakeQuizViewModel()
        {
            this.LoadMakeNewQuizView();

            // Hook up Commands to associated methods
            this.LoadMakeNewQuizViewCommand = new DelegateCommand(o => this.LoadMakeNewQuizView());
            this.LoadEditRemoveQuizViewCommand = new DelegateCommand(o => this.LoadEditRemoveQuizView());
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
            CurrentViewModel = new MakeNewQuizViewModel();
        }

        private void LoadEditRemoveQuizView()
        {
            CurrentViewModel = new EditRemoveQuizViewModel();
        }
    }
}
