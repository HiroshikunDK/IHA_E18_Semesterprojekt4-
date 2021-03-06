﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
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

        private Answer[] _selectedAnswers;
        private int _currentQuestionIndex = 0;

        IUnitOfWork Data = new UnitOfWork();

        public EventHandler<QuizEndedEventArgs> QuizEndedEvent;
        public ICommand QuestionAnswerClick { get; set; }
        public ICommand EndQuizClick { get; set; }
        public ICommand NextQuestionClick { get; set; }
        public ICommand PrevQuestionClick { get; set; }

        //Storing the statistics of the answers given to the quiz
        private QuizUserStatistic _results;

        private int _answerCount;

        public AnswerQuizQuestionViewModel (Quiz quiz)
        {
            selectedQuiz = quiz;
            questions = selectedQuiz.Questions.ToList();

            _selectedAnswers = new Answer[questions.Count];
            //for (int i = 0; i < questions.Count; i++)
            //{
            //    _selectedAnswers.Add(null);
            //}

            CurrentQuestion = questions[0]; //TODO: add check that questions isn't empty.
            //GetAllAnswers();

            NextQuestionClick = new Command(NextQuestionClickFunc, CanExecute);
            PrevQuestionClick = new Command(PrevQuestionClickFunc, CanExecute);
            QuestionAnswerClick = new Command(QuestionAnswerClickFunc, CanExecute);
            EndQuizClick = new Command(EndQuizClickFunc, CanExecute);
            

            //long userID = Data.LoginService.User.UserID; //returns 0 every time.
            long userID = 1;
            _results = new QuizUserStatistic(){QuizID = selectedQuiz.QuizID, UserID = userID};

            foreach (var question in questions)
            {
                _results.SelectedAnswers.Add(new SelectedAnswer(){QuestionID = question.QuestionID});
            }

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
                _currentQuestionIndex = questions.IndexOf(currentQuestion);

                //frontloading or load answers upon selection change.
                Answers = currentQuestion.Answers.ToList(); //if frontloaded questions
                //GetAnswers(); //load Answers when question is selected

                RaisePropertyChanged("CurrentQuestion");
                RaisePropertyChanged("SelectedAnswer");
            }
        }

        public List<Answer> Answers
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
            get { return _selectedAnswers[_currentQuestionIndex]; }
            set
            {
                if (value != null)
                {
                    _selectedAnswers[_currentQuestionIndex] = value;
                    //_selectedAnswerFromView = value;
                    AnswerQuestion();
                }
                RaisePropertyChanged("SelectedAnswer"); //Update or Force refresh of selectedAnswer. (force needed when changing Answers)
            }
        }

        #endregion

        #region DataAcess

        /// <summary>
        /// Get answers for a single question.
        /// </summary>
        private async void GetAnswers()
        {
            int id = Convert.ToInt32(CurrentQuestion.QuestionID);
            Answers = (await Data.Answer.GetAnswerByQuestionID(CurrentQuestion.QuestionID));
        }

        private async void AddStatistics()
        {
            await Data.QuizUserStatistic.Add(_results);
        }

        private async void UpdateAllQuestions()
        {
            foreach (var question in questions)
            {
                await Data.Question.Update(question.QuestionID, question); //TODO: Can't await
            }
        }
        /// <summary>
        /// Used for frontloading all answers to the questions in the quiz.
        /// </summary>
        //private async void GetAllAnswers()
        //{
        //    foreach (var question in SelectedQuiz.Questions)
        //    {
        //        Answers = (await Data.Answer.GetAnswerByQuestionID(question.QuestionID));
        //    }
        //}

        #endregion

        #region CommandFunctions

        private void QuestionAnswerClickFunc(object parameter)
        {
            AnswerQuestion();
        }

        private void AnswerQuestion()
        {
            if (_selectedAnswers[_currentQuestionIndex] == null) return;
            //_selectedAnswers[_currentQuestionIndex] = _selectedAnswer;

            int i = 0;
            foreach (var answer in Answers)
            {
                if (_selectedAnswers[_currentQuestionIndex].AnswerID == answer.AnswerID)
                {
                    LogAnswer();
                    break;
                }

                i++;
            }
            GoToNextUnansweredQuestion();
        }

        private void NextQuestionClickFunc(object obj)
        {
            GoToNextUnansweredQuestion();
        }

        private void PrevQuestionClickFunc(object obj)
        {
            GoToPrevQuestion();
        }

        private void EndQuizClickFunc(object obj)
        {
            UserControl currUserControl = (UserControl) obj;

            MessageBoxResult res = MessageBoxResult.OK;

            if (_answerCount != questions.Count)
            {
                res = MessageBox.Show($"You have answered: {_answerCount} out of: {questions.Count} questions. Are you sure that you want to end the quiz before answering the remaining questions?",
                    "Are you sure you want to end the quiz?", MessageBoxButton.OKCancel);
            }
            
            if (res == MessageBoxResult.OK) EndQuiz(currUserControl); //TODO: might be bad coding practice to pass this along, as that means EndQuiz is aware of the view, as opposed to only this command being aware of it.
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
            foreach (var answer in _results.SelectedAnswers)
            {
                if (answer.QuestionID == CurrentQuestion.QuestionID) break;
                index++;
            }

            if (_results.SelectedAnswers.ElementAt(index).IsSelectedCorrect == null)
            {
                _answerCount++;
            }

            _results.SelectedAnswers.ElementAt(index).IsSelectedCorrect = _selectedAnswers[_currentQuestionIndex].IsCorrect;
        }

        /// <summary>
        /// Push answers up to the web, and go to other view
        /// </summary>
        private void EndQuiz(UserControl currUserControl)
        {
            int correctAnswers = 0;

            int i = 0;
            foreach (var answer in _results.SelectedAnswers)
            {
                if (answer.IsSelectedCorrect == "1")
                {
                    correctAnswers++;
                    questions[i].CorrectCount++;
                }
                else questions[i].WrongCount++;

                answer.IsSelectedCorrect = "0"; //null fix
                i++;
            }
            _results.correctPercentage = (correctAnswers * 100) / questions.Count;

            //TODO: Add comparison with the average in percentage.
            MessageBox.Show(
                $"You have answered {correctAnswers} correctly, out of: {questions.Count} questions. Making for {_results.correctPercentage}% correct answers.",
                "Quiz stats", MessageBoxButton.OK);

            AddStatistics();
            UpdateAllQuestions();

            //hide the view
            currUserControl.Visibility = Visibility.Collapsed;
            QuizEndedEvent(this, new QuizEndedEventArgs()); //TODO: add eventual args
        }

        #region HelperFunctions

        /// <summary>
        /// Moves to first Question in the quiz that hasn't been answered.
        /// </summary>
        private void GoToNextUnansweredQuestion()
        {
            bool isEndOfQuiz = true;
            int i = 0;
            foreach (var answer in _results.SelectedAnswers)
            {
                if (answer.IsSelectedCorrect == null)
                {
                    CurrentQuestion = SelectedQuiz.Questions.ElementAt(i);
                    isEndOfQuiz = false;
                    break;
                }
                i++;
            }

            if (isEndOfQuiz)
                MessageBox.Show("You have answered every question in the quiz, you can end the quiz whenever you want.", "Everything has been answered",
                    MessageBoxButton.OK);

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

            if (index != 0) CurrentQuestion = SelectedQuiz.Questions.ElementAt(index - 1);
        }

        #endregion

    }

    public class QuizEndedEventArgs : EventArgs
    {
        //TODO: add eventual args
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