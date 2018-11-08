using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Core;
using DAL.Core.Repositories;
using DAL.Presistence.Repositories;

namespace DAL.Presistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork()
        {
            User = new UserRepository();
            Answer = new AnswerRepository();
            Catagory = new CatagoryRepository();
            Question = new QuestionRepository();
            Quiz = new QuizRepository();
            Study = new StudyRepository();
            UserType = new UserTypeRepository();
            Course = new CourseRepository();
            Faculty = new FacultyRepository();
            Rating = new RatingRepository();
            Semester = new SemesterRepository();
        }

        public IUserRepository User { get; }
        public IAnswerRepository Answer { get; }
        public ICatagoryRepository Catagory { get; }
        public IQuestionRepository Question { get; }
        public IQuizRepository Quiz { get; }
        public IStudyRepository Study { get; }
        public IUserTypeRepository UserType { get; }
        public ICourseRepository Course { get; }
        public IFacultyRepository Faculty { get; }
        public IRatingRepository Rating { get; }
        public ISemesterRepository Semester { get; }
    }
}
