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
        public ICommand GetRatingInfo { get; set; }

        private IUnitOfWork Data = new UnitOfWork();

        public YourStatisticsViewModel(IUnitOfWork data)
        {
           
            //GetRatingsCommand = new Command(GetRatingCommandClickFunc, canExecute);
            GetRatingInfo = new Command(GetRatingInfoClickFunc, canExecute);
            Data = data;
            currentUser = Data.LoginService.User;
            getRelevantQuizList(3);

            currentRating = new Rating();
            Quizzes = new List<Quiz>();

        }


        #region Propeties

        private void getUser()
        {
            currentUser = Data.LoginService.User;
        }

        public Userr Userr
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
                RaisePropertyChanged("Quiz");
                GetRatingCommandClickFunc();
            }
        }

        public Rating CurrentRating
        {
            get { return currentRating; }
            set
            {
                currentRating = value;
                RaisePropertyChanged("CurrentRating");
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
                RaisePropertyChanged("TotalRating");
            }
        }

        public List<Quiz> Quizzes
        {
            get { return listOfQuizzes; }
            set
            {
               listOfQuizzes = value;
                RaisePropertyChanged("Quizzes");  
            }
        }
        private void getQuizzes()
        {
            
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

        private void GetRatingCommandClickFunc()
        {
            long ID = Quiz.QuizID;

            //foreach (var item in Quizzes)
            //{
            //    if (item.QuizID == ID)
            //    {
            //        Quiz = item;
            //    }
            //}

            if (Ratings != null)
            {
                Ratings.Clear();
                getRelevantRatingList(ID);
                TotalRating = 0;

                foreach (var item in Ratings)
                {
                    TotalRating += item.Rating1;
                }
                TotalRating = TotalRating / Ratings.Count();
            }
            else
            {
                TotalRating = 0.00;
                CurrentRating.Reason = "No comment was found";
                CurrentRating.Rating1 = 0;
            }
            
        }

        private void GetRatingInfoClickFunc(object ratingID)
        {

            int ID = Convert.ToInt32(ratingID);

            if (Ratings != null)
            {
                foreach (var item in Ratings)
                {
                    if (item.RatingID == ID)
                    {
                        CurrentRating = item;
                    }
                }
            }
            else
            {
                TotalRating = 0.00;
                CurrentRating.Reason = "No comment was found";
                CurrentRating.Rating1 = 0;
            }
        }

        private bool canExecute(object parameter)
        {
            return true;
        }

        #endregion

        #region Database_calls

        public async void getRelevantQuizList(long UserID)
        {
            Quizzes = new List<Quiz>();
            Quizzes = await Data.Quiz.GetQuizzesByUserID(3);
        }

        public async void getRelevantRatingList(long quizID)
        {
            Ratings = new List<Rating>();
            Ratings = await Data.Rating.GetRatingByQuizID(quizID);
        }

        #endregion



    }
}