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
            Catagory = new Repository<Catagory>();
            Question = new Repository<Question>();
            Quiz = new QuizRepository();
            Study = new Repository<Study>();
            UserType = new Repository<UserType>();
            Course = new Repository<Course>();
            Faculty = new Repository<Faculty>();
            Rating = new Repository<Rating>();
            Semester = new Repository<Semester>();
            
        }

        private void AddAuthTokens(object o, UserLoggedInEventArg args)
        {
            User.SetAuthToken(args.User.AuthToken);
            Answer.SetAuthToken(args.User.AuthToken);
            Catagory.SetAuthToken(args.User.AuthToken);
            Question.SetAuthToken(args.User.AuthToken);
            Quiz.SetAuthToken(args.User.AuthToken);
            Study.SetAuthToken(args.User.AuthToken);
            UserType.SetAuthToken(args.User.AuthToken);
            Course.SetAuthToken(args.User.AuthToken);
            Faculty.SetAuthToken(args.User.AuthToken);
            Rating.SetAuthToken(args.User.AuthToken);
            Semester.SetAuthToken(args.User.AuthToken);
        }

        public IUserRepository User { get; set; }
        public IAnswerRepository Answer { get; }
        public IRepository<Catagory> Catagory { get; }
        public IRepository<Question> Question { get; }
        public IQuizRepository Quiz { get; }
        public IRepository<Study> Study { get; }
        public IRepository<UserType> UserType { get; }
        public IRepository<Course> Course { get; }
        public IRepository<Faculty> Faculty { get; }
        public IRepository<Rating> Rating { get; }
        public IRepository<Semester> Semester { get; }
        public ILoginService LoginService { get; }
    }
}
