using System;
using System.Collections.Generic;
using System.Linq;
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
            LoginService = new LoginService(this);
        }

        public IUserRepository User { get; }
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
