using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ViewModels.Commands;

namespace ViewModels
{
    public class MakeNewQuizViewModel : BaseViewModel
    {
        private string quizName { get; set; }

        public string QuizName
        {
            get { return quizName; }
            set { quizName = value; }
        }

        public ICommand GemogNaeste { get; set; }

        public MakeNewQuizViewModel()
        {
            GemogNaeste = new Command(SOmething,CanExecute);
        }


        private bool CanExecute(object o)
        {
            return true;
        }

        private void SOmething(object o)
        {
            MessageBox.Show("Gemtaf");
        }
        
    }
}
