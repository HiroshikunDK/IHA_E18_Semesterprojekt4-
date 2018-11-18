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
            LoginService = new LoginService(this);
            User = new UserRepository(LoginService);
            Answer = new AnswerRepository(LoginService);
            Catagory = new Repository<Catagory>(LoginService);
            Question = new Repository<Question>(LoginService);
            Quiz = new QuizRepository(LoginService);
            Study = new Repository<Study>(LoginService);
            UserType = new Repository<UserType>(LoginService);
            Course = new Repository<Course>(LoginService);
            Faculty = new Repository<Faculty>(LoginService);
            Rating = new Repository<Rating>(LoginService);
            Semester = new Repository<Semester>(LoginService);
            
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
