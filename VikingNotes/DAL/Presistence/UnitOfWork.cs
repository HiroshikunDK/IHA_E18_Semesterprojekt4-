using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DAL.Core;
using DAL.Core.Repositories;
using DAL.Presistence.Repositories;
using RESTfullWebApi.Models;

namespace DAL.Presistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork()
        {
            LoginService = new LoginService(this);
            LoginService.UserLoggedIn += AddAuthTokens;
            User = new UserRepository();
            Answer = new AnswerRepository();
            Question = new Repository<Question>();
            Quiz = new QuizRepository();
            Study = new Repository<Study>();
            UserType = new Repository<UserType>();
            Course = new Repository<Course>();
            Faculty = new Repository<Faculty>();
            Rating = new RatingRepository();
            Semester = new Repository<Semester>();
            SelectedAnswer = new Repository<SelectedAnswer>();
            QuizUserStatistic = new Repository<QuizUserStatistic>();
            
        }

        private void AddAuthTokens(object o, UserLoggedInEventArg args)
        {
            User.SetAuthToken(args.User.AuthToken);
            Answer.SetAuthToken(args.User.AuthToken);
            Question.SetAuthToken(args.User.AuthToken);
            Quiz.SetAuthToken(args.User.AuthToken);
            Study.SetAuthToken(args.User.AuthToken);
            UserType.SetAuthToken(args.User.AuthToken);
            Course.SetAuthToken(args.User.AuthToken);
            Faculty.SetAuthToken(args.User.AuthToken);
            Rating.SetAuthToken(args.User.AuthToken);
            Semester.SetAuthToken(args.User.AuthToken);
            SelectedAnswer.SetAuthToken(args.User.AuthToken);
            QuizUserStatistic.SetAuthToken(args.User.AuthToken);
        }

        public IUserRepository User { get; set; }
        public IAnswerRepository Answer { get; }
        public IRepository<Question> Question { get; }
        public IQuizRepository Quiz { get; }
        public IRepository<Study> Study { get; }
        public IRepository<UserType> UserType { get; }
        public IRepository<Course> Course { get; }
        public IRepository<Faculty> Faculty { get; }
        public IRatingRepository Rating { get; }
        public IRepository<Semester> Semester { get; }
        public ILoginService LoginService { get; }
        public IRepository<SelectedAnswer> SelectedAnswer { get; }
        public IRepository<QuizUserStatistic> QuizUserStatistic { get; }
    }
}
