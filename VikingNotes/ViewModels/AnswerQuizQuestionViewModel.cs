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
        private Answer selectedAnswer { get; set; }

        IUnitOfWork Data = new UnitOfWork();

        public ICommand QuestionAnswerClick { get; set; }
        public ICommand NextQuestionClick { get; set; }
        public ICommand PrevQuestionClick { get; set; }

        private int answerCount; //TODO: use to keep track of when we have answered all questions, to lessen the amount of work to do in EndQuiz.

        //TODO: this should be made into something that is stored in a Question, to make access possible in converters, and easier to associate with given question.
        private bool[,] _answersGiven; //[x,0] = is answer given, [x, 1] = is answer correct

        public AnswerQuizQuestionViewModel (Quiz quiz)
        {
            selectedQuiz = quiz;
            questions = selectedQuiz.Questions.ToList();
            _answersGiven = new bool[questions.Count, 2];
            CurrentQuestion = questions[0];
            GetAllAnswers();

            NextQuestionClick = new Command(NextQuestionClickFunc, CanExecute);
            PrevQuestionClick = new Command(PrevQuestionClickFunc, CanExecute);
            QuestionAnswerClick = new Command(QuestionAnswerClickFunc, CanExecute);
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

                //frontloading or load answers upon selection change.
                Answers = currentQuestion.Answers.ToList(); //if frontloaded questions
                //GetAnswers(); //load Answers when question is selected

                RaisePropertyChanged("CurrentQuestion");
            }
        }

        public List<Answer> Answers //Testing TODO: test if this implementation is reasonable
        {
            get { return CurrentQuestion.Answers.ToList(); }
            set
            {
                CurrentQuestion.Answers = value;
                RaisePropertyChanged("Answers");
            }
        }

        public Answer SelectedAnswer
        {
            get { return selectedAnswer; }
            set
            {
                selectedAnswer = value;
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
            if (SelectedAnswer == null) return;


            foreach (var answer in Answers)
            {
                if (SelectedAnswer.AnswerID == answer.AnswerID)
                {
                    LogAnswer();
                    break;
                }
            }

            if (currentQuestionIndex == selectedQuiz.Questions.Count) EndQuiz(); //TODO: update to reflect that jumping through is possible
            else GoToNextQuestion();
        }

        private void NextQuestionClickFunc(object obj)
        {
            if (currentQuestionIndex < SelectedQuiz.Questions.Count)
            {
                GoToNextQuestion();
            }
        }

        private void PrevQuestionClickFunc(object obj)
        {
            GoToPrevQuestion();
        }

        #endregion

        private bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Save selected answer locally.
        /// </summary>
        private void LogAnswer()
        {
            int index = 0;
            foreach (var question in SelectedQuiz.Questions)
            {
                if (question.QuestionID == CurrentQuestion.QuestionID) break;

                index++;
            }

            _answersGiven[index, 0] = true;

            if (SelectedAnswer.IsCorrect == "1") _answersGiven[currentQuestionIndex, 1] = true;
            else _answersGiven[currentQuestionIndex, 1] = false;
        }

        /// <summary>
        /// Push answers up to the web, and go to other view
        /// </summary>
        private void EndQuiz()
        {

        }

        #region HelperFunctions

        /// <summary>
        /// Moves to first Question in the quiz that hasn't been answered.
        /// </summary>
        private void GoToNextQuestion()
        {
            for (int i = 0; i < _answersGiven.Length; i++)
            {
                if (_answersGiven[i, 0] == false)
                {
                    CurrentQuestion = SelectedQuiz.Questions.ElementAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// Moves to the question at the index below the currently selected question.
        /// </summary>
        private void GoToPrevQuestion()
        {
            int index = 0;
            foreach (var question in SelectedQuiz.Questions)
            {
                if (question == CurrentQuestion) break;

                index++;
            }

            CurrentQuestion = SelectedQuiz.Questions.ElementAt(index - 1);
        }

        #endregion

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