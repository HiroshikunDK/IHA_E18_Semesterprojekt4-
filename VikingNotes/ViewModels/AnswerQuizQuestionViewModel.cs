using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class AnswerQuizQuestionViewModel : BaseViewModel
    {
        private Quiz selectedQuiz { get; set; }
        private List<Question> questions { get; set; }
        private Question currentQuestion { get; set; }
        private List<Answer> answers { get; set; } //TODO: Remove this, once it is tested without 

        IUnitOfWork Data = new UnitOfWork();

        public ICommand QuestionAnswerClick { get; set; }
        public ICommand NextQuestionClick { get; set; }
        public ICommand PrevQuestionClick { get; set; }

        private int currentQuestionIndex;
        private int answerCount;

        //TODO: this should be made into something that is stored in a Question, to make access possible in converters, and easier to associate with given question.
        private bool[,] _answersGiven; //[x,0] = is answer given, [x, 1] = is answer correct

        public AnswerQuizQuestionViewModel (Quiz quiz)
        {
            selectedQuiz = quiz;
            questions = selectedQuiz.Questions.ToList();
            _answersGiven = new bool[questions.Count, 2];
            CurrentQuestion = questions[0];
            GetAllAnswers();
        }

        #region Properties
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
                Answers = currentQuestion.Answers.ToList();
                //GetAnswers();
                RaisePropertyChanged("CurrentQuestion");
            }
        }

        public List<Answer> Answers //Testing TODO: Remove once it has been tested properly
        {
            get { return answers; }
            set
            {
                answers = value;

                //answers = new List<Answer>(); //TODO: Testing
                //answers.Add(new Answer() { Answer1 = "lol" });

                RaisePropertyChanged("Answers");
            }
        }

        public bool[,] AnswersGiven
        {
            get { return _answersGiven; }
        }

        #endregion


        #region DataAcess

        /// <summary>
        /// Get answers for a single question.
        /// </summary>
        private async void GetAnswers()
        {
            int id = Convert.ToInt32(CurrentQuestion.QuestionID);
            //Answers = (await Data.Answer.GetAllAsync()).ToList();
            Answers = (await Data.Answer.GetAnswerByQuestionID(CurrentQuestion.QuestionID));
        }

        /// <summary>
        /// Used for frontloading all answers to the questions in the quiz.
        /// </summary>
        private async void GetAllAnswers()
        {
            foreach (var question in SelectedQuiz.Questions)
            {
                Answers = (await Data.Answer.GetAnswerByQuestionID(question.QuestionID));
            }
        }

        #endregion

        #region CommandFunctions

        private void QuestionAnswerClickFunc(object parameter)
        {
            //string selectedAnswer = ((Button)obj).Content.ToString();
            int id = Convert.ToInt32(parameter);


            foreach (var answer in Answers)
            {
                if (id == answer.AnswerID)
                {
                    LogAnswer(answer);
                    break;
                }
            }

            if (currentQuestionIndex == selectedQuiz.Questions.Count) EndQuiz(); //TODO: update to reflect that jumping through is possible
            else
            {
                currentQuestionIndex++;
                currentQuestion = selectedQuiz.Questions.ElementAt(currentQuestionIndex);
            }
        }

        private void NextQuestionClickFunc(object obj)
        {
            if (currentQuestionIndex < SelectedQuiz.Questions.Count)
            {
                currentQuestionIndex++;
                currentQuestion = selectedQuiz.Questions.ElementAt(currentQuestionIndex);
            }
        }

        private void PrevQuestionClickFunc(object obj)
        {
            if (currentQuestionIndex > 0)
            {
                currentQuestionIndex--;
                currentQuestion = selectedQuiz.Questions.ElementAt(currentQuestionIndex);
            }
        }

        #endregion

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
            _answersGiven[currentQuestionIndex, 1] = false; //selectedAnswer.IsCorrect; //TODO: why is IsCorrect a string? and make this set itself to the state of IsCorrect (true/false)
        }

        /// <summary>
        /// Push answers up to the web, and go to other view
        /// </summary>
        private void EndQuiz()
        {

        }
    }

    //TODO: these are currently out of use, due to not being favourable above other solutions, or otherwise just not functioning.
    #region ValueConverters

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
            var context = (ViewModels.AnswerQuizQuestionViewModel)value;
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

    /// <summary>
    /// Takes the question and returns a brush based on it being answered
    /// </summary>
    public class QuestionToAnswerStatusBrush : IValueConverter
    {
        SolidColorBrush unansweredBrush = new SolidColorBrush(Colors.Black);
        SolidColorBrush answeredBrush = new SolidColorBrush(Colors.SteelBlue);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var question = (RESTfullWebApi.Models.Question)value;

            //TODO: add something to Question to allow the program to know if it has been answered correctly
            //return question.IsAnswered ? answeredBrush : unansweredBrush; 

            return unansweredBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    #endregion
}