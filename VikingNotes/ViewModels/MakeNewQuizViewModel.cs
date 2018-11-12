using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels.Commands;

namespace ViewModels
{
    public class MakeNewQuizViewModel : BaseViewModel
    {
        private string Question = "";
        private string MCQName = "";
        private List<string> AnswerPossibilities;
        private List<bool> Correctness;


        public MakeNewQuizViewModel()
        {

        }


    }
}
