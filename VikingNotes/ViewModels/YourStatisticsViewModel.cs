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

        public YourStatisticsViewModel(IUnitOfWork data = null)
        {
            Data = data;
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
   
        
        public ICollection<Rating> RatingsList
        {
            
            get { return ListOfRating; }
            set
            {
                //if (value != currentRating.Rating1)
                //{
                //    _superModel.Debtors = value;
                //    NotifyPropertyChanged();
                //}
            }
        }




    }
}
