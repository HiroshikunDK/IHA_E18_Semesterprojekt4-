using RESTfullWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DAL.Core;
using DAL.Presistence;
using ViewModels.Commands;

namespace ViewModels
{
    public class TakeQuizViewModel : BaseViewModel
    {
        private IList<Study> _studyList { set; get; }
        private IList<Semester> _semesterList { set; get; }
        private IList<Course> _courseList { set; get; }
        private IList<Quiz> _quizList { set; get; }

        public ICommand SelectFaculityCommand { get; set; }

        private IUnitOfWork Data = new UnitOfWork();

        public TakeQuizViewModel()
        {
            SelectFaculityCommand = new Command(SelectFaculity, canExecute);
        }


        public IList<Study> StudyList
        {
            get { return _studyList; }
            set
            {
                _studyList = value;
                RaisePropertyChanged("StudyList");
            }
        }

        public IList<Semester> SemesterList
        {
            get { return _semesterList; }
            set
            {
                _semesterList = value;
                RaisePropertyChanged("SemesterList");
            }
        }
        public IList<Course> CourseList
        {
            get { return _courseList; }
            set
            {
                _courseList = value;
                RaisePropertyChanged("CourseList");
            }
        }
        public IList<Quiz> QuizList
        {
            get { return _quizList; }
            set
            {
                _quizList = value;
                RaisePropertyChanged("QuizyList");
            }
        }

        private bool canExecute(object parameter)
        {
            return true;
        }

        public async void SelectFaculity(object parameter)
        {
            int id = Convert.ToInt32(parameter);
            StudyList = (await Data.Study.GetAllAsync()).FindAll(s => s.FacultyID == id);
               // .Result.Where(s => s.FacultyID == id).ToList()
        }
    }
}
