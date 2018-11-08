using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Core.Repositories;

namespace DAL.Core
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        IAnswerRepository Answer { get; }
        ICatagoryRepository Catagory { get; }
        IQuestionRepository Question { get; }
        IQuizRepository Quiz { get; }
        IStudyRepository Study { get; }
        IUserTypeRepository UserType { get; }
        ICourseRepository Course { get;  }
        IFacultyRepository Faculty { get; }
        IRatingRepository Rating { get; }
        ISemesterRepository Semester { get; }
    }
}
