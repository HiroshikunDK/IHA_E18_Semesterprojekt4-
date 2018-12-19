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
        IUserRepository User { get; set; }
        IAnswerRepository Answer { get; }
        IRepository<Question> Question { get; }
        IQuizRepository Quiz { get; }
        IRepository<Study> Study { get; }
        IRepository<UserType> UserType { get; }
        IRepository<Course> Course { get; }
        IRepository<Faculty> Faculty { get; }
        IRatingRepository Rating { get; }
        IRepository<Semester> Semester { get; }
        ILoginService LoginService { get; }
        IRepository<SelectedAnswer> SelectedAnswer { get; }
        IRepository<QuizUserStatistic> QuizUserStatistic { get; }
    }
}
