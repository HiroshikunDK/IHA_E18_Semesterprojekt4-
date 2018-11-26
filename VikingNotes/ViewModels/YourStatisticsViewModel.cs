using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Core;
using DAL.Presistence;
using RESTfullWebApi.Models;
using ViewModels.Commands;
using System.Windows.Input;


namespace ViewModels
{
    public class YourStatisticsViewModel : BaseViewModel
    {
        private Userr currentUser { get; set; }
        private Quiz currentQuiz { get; set; }
        private Rating currentRating { get; set; }
        private double quizRating { get; set; }
        private double totalRating { get; set; }

        private List<Rating> listOfRatings { get; set; }
        private List<Quiz> listOfQuizzes { get; set; }

        public ICommand GetRatingsCommand { get; set; }

        private IUnitOfWork Data = new UnitOfWork();

        public YourStatisticsViewModel(/*IUnitOfWork data*/ )
        {
            GetRatingsCommand = new Command(GetRatingCommandClickFunc, canExecute);
            //Data = data;
            //currentUser = (Userr)Data.User;
            getRelevantQuizList(3);


        }


        #region Propeties

        private void getUser()
        {
            currentUser = Data.LoginService.User;
        }

        public Userr UserID
        {
            get { return currentUser; }
            set
            {
                currentUser = value;
                RaisePropertyChanged("userName");
            }
        }

        public Quiz Quiz
        {
            get { return currentQuiz; }
            set
            {
                currentQuiz = value;
                RaisePropertyChanged("currentQuiz");
            }
        }

        public Rating Rating
        {
            get { return currentRating; }
            set
            {
                currentRating = value;
                RaisePropertyChanged("currentRating");
            }
        }

        public double QuizRating
        {
            get { return quizRating; }
            set
            {
                quizRating = value;
                RaisePropertyChanged("QuizChanged");
            }

        }

        public double TotalRating
        {
            get { return totalRating; }
            set
            {
                totalRating = value;
                RaisePropertyChanged("TotalRatingChanged");
            }
        }

        public List<Quiz> Quizzes
        {
            get { return listOfQuizzes; }
            set
            {
                if (value != listOfQuizzes)
                {
                    RaisePropertyChanged("listOfQuizzes");
                }
            }
        }

        public List<Rating> Ratings
        {
            get { return listOfRatings; }
            set
            {
                if (value != listOfRatings)
                {
                    RaisePropertyChanged("listOfRatings");
                }
            }
        }

        #endregion

        #region Commands

        private void GetRatingCommandClickFunc(object obj)
        {
            if (currentQu)
            {
                currentQuestionIndex--;
                currentQuestion = selectedQuiz.Questions.ElementAt(currentQuestionIndex);
            }
        }

        private bool canExecute(object parameter)
        {
            return true;
        }
        
        #endregion

        public async void getRelevantQuizList(long UserID)
        {
          listOfQuizzes = await Data.Quiz.GetQuizzesByUserID(3);
        }

        public async void getRelevantRatingList(long quizID)
        {
            //ListOfRating = await Data.Rating.;
        }




    }
}
