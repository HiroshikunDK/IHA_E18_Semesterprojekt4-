using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Core.Repositories;
using RESTfullWebApi.Models;

namespace DAL.Core
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        IRepository<Answer> Answer { get; }
        IRepository<Catagory> Catagory { get; }
        IRepository<Question> Question { get; }
        IRepository<Quiz> Quiz { get; }
        IRepository<Study> Study { get; }
        IRepository<UserType> UserType { get; }
        IRepository<Course> Course { get; }
        IRepository<Faculty> Faculty { get; }
        IRepository<Rating> Rating { get; }
        IRepository<Semester> Semester { get; }
        ILoginService LoginService { get; }
    }
}
