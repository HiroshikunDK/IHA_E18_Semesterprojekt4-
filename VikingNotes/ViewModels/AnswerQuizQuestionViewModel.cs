using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using RESTfullWebApi.Models;
using ViewModels.Commands;

namespace ViewModels
{
    public class AnswerQuizQuestionViewModel : BaseViewModel
    {
        private Quiz selectedQuiz { get; set; }
        private Question currentQuestion { get; set; }
        private List<Answer> answers { get; set; } //TODO: Remove this, once it is tested without

        public ICommand QuestionAnswerClick { get; set; }

        private int currentQuestionIndex;
        private int answerCount;

        private bool[,] _answersGiven; //[x,0] = is answer given, [x, 1] = is answer correct

        public AnswerQuizQuestionViewModel ()
        {
            selectedQuiz = new Quiz();
            selectedQuiz.Questions.Add(new Question(){CorrectCount = 0, Question1 = "spørgsmål 1"});
            selectedQuiz.Questions.Add(new Question(){CorrectCount = 0, Question1 = "spørgsmål 2"});

            currentQuestion = new Question();
            _answersGiven = new bool[selectedQuiz.Questions.Count, 2];

            //Testing TODO remove this once it has been tested properly
            currentQuestion.Question1 = "test spørgsmål: ";
            Answers = new List<Answer>();
            Answers.Add(new Answer(){Answer1 = "mulighed 1"});
            Answers.Add(new Answer(){Answer1 = "mulighed 2"});
            Answers.Add(new Answer(){Answer1 = "mulighed 3"});
            Answers.Add(new Answer(){Answer1 = "mulighed 4"}); //kommenter ud for at teste

            QuestionAnswerClick = new Command(QuestionAnswerClickFunc, canExecute);
        }


        public Quiz SelectedQuiz
        {
            get { return selectedQuiz; }
            set
            {
                selectedQuiz = value;
            }
        }
        public Question CurrentQuestion
        {
            get { return currentQuestion; }
            set
            {
                currentQuestion = value;
            }
        }

        public List<Answer> Answers //Testing TODO: Remove once it has been tested properly
        {
            get { return answers; }
            set { answers = value; }
        }

        private void QuestionAnswerClickFunc(object obj)
        {
            string selectedAnswer = ((Button)obj).Content.ToString();

            foreach (var answer in Answers)
            {
                if (selectedAnswer.Equals(answer.Answer1))
                {
                    LogAnswer(answer);
                }
            }

            if (currentQuestionIndex == selectedQuiz.Questions.Count) EndQuiz(); //do something to handle when quiz is over
            else
            {
                currentQuestionIndex++;
                currentQuestion = selectedQuiz.Questions.ElementAt(currentQuestionIndex);
            }
        }
        private bool canExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Save selected answer locally.
        /// </summary>
        private void LogAnswer(Answer selectedAnswer)
        {
            _answersGiven[currentQuestionIndex, 0] = true;
            _answersGiven[currentQuestionIndex, 1] = false; //selectedAnswer.IsCorrect; //TODO: why is IsCorrect a string?
        }

        /// <summary>
        /// Push answers up to the web, and go to other view
        /// </summary>
        private void EndQuiz()
        {

        }
    }


    public class IntToAnswerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var context = (ViewModels.AnswerQuizQuestionViewModel)value;

            int answerNumber = Int16.Parse((string)parameter);

            return answerNumber < context.Answers.Count ? context.Answers[answerNumber].Answer1 : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IntToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var context = (ViewModels.AnswerQuizQuestionViewModel) value;
            //int cnt = context.CurrentQuestion.Answers.Count;
            int cnt = context.Answers.Count; //testing due to not being able to run, has to use a local TODO: remove this

            int answerNumber = Int16.Parse((string)parameter);

            return answerNumber < cnt ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}