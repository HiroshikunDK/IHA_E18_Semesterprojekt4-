using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Core;
using DAL.Core.Repositories;
using DAL.Presistence.Repositories;
using RESTfullWebApi.Models;
using ViewModels.Services.Interfaces;
using ViewModels.Services.Source;

namespace DAL.Presistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork()
        {
            User = new UserRepository();
            Answer = new Repository<Answer>();
            Catagory = new Repository<Catagory>();
            Question = new Repository<Question>();
            Quiz = new Repository<Quiz>();
            Study = new Repository<Study>();
            UserType = new Repository<UserType>();
            Course = new Repository<Course>();
            Faculty = new Repository<Faculty>();
            Rating = new Repository<Rating>();
            Semester = new Repository<Semester>();
            LoginService = new LoginService(this);
        }

        public IUserRepository User { get; }
        public IRepository<Answer> Answer { get; }
        public IRepository<Catagory> Catagory { get; }
        public IRepository<Question> Question { get; }
        public IRepository<Quiz> Quiz { get; }
        public IRepository<Study> Study { get; }
        public IRepository<UserType> UserType { get; }
        public IRepository<Course> Course { get; }
        public IRepository<Faculty> Faculty { get; }
        public IRepository<Rating> Rating { get; }
        public IRepository<Semester> Semester { get; }
        public ILoginService LoginService { get; }
    }
}
