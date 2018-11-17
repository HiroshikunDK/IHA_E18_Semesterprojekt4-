using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using DAL.Core;
using DAL.Presistence;
using RESTfullWebApi.Models;
using ViewModels.Commands;
namespace ViewModels
{
    public class AllStatisticsViewModel : BaseViewModel
    {
        private Quiz selectedQuiz { get; set; }
        private Question currentQuestion { get; set; }
        private List<Answer> answers { get; set; } //TODO: Remove this, once it is tested without 

        IUnitOfWork Data = new UnitOfWork();

        public ICommand QuestionAnswerClick { get; set; }
        public ICommand NextQuestionClick { get; set; }
        public ICommand PrevQuestionClick { get; set; }

        private int currentQuestionIndex;
        private int answerCount;
    }
}
