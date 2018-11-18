using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Core;
using DAL.Presistence;
using RESTfullWebApi.Models;
using ViewModels.Commands;

namespace ViewModels
{
    public class YourStatisticsViewModel : BaseViewModel
    {
        private Userr currentUser { get; set; }
        private Quiz currentQuiz { get; set; }
        private Rating currentRating { get; set; }
        private double quizRating { get; set; }
        private double totalRating { get; set; }

        private List<Rating> ListOfRating { get; set; }
        private List<Quiz> listOfQuizzes { get; set; }
        private IUnitOfWork Data;

        public YourStatisticsViewModel(IUnitOfWork data )
        {
            Data = data;
            currentUser = (Userr)Data.User;
            getRelevantQuizList(currentUser.UserID);


        }

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


        public ICollection<Quiz> Quizzes
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

        public async void getRelevantQuizList(long UserID)
        {
          listOfQuizzes = await Data.Quiz.GetQuizzesByUserID(currentUser.UserID);
        }

        public async void getRelevantRatingList(long quizID)
        {
            //ListOfRating = await Data.Rating.;
        }




    }
}
