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
    public class MakeNewQuizViewModel : BaseViewModel
    {
        private IList<Study> _studyList { set; get; }
        private IList<Faculty> _FacultyList { set; get; }
        private IList<Semester> _semesterList { set; get; }
        private IList<Course> _courseList { set; get; }
        private IList<Quiz> _quizList { set; get; }
        private IUnitOfWork Data = new UnitOfWork();

        private string quizName { get; set; }
        public string QuizName
        {
            get { return quizName; }
            set { quizName = value; }
        }

        private string questionName { get; set; }

        public string QuestionName
        {
            get { return questionName;}
            set { questionName = value; }

        }


        private IList<Question> questions { get; set; }
        public IList<Question> Questions
        {
            get { return questions;}
            set { questions = value; }
        }


        private IList<Question> svarMuligheder { get; set; }
        public IList<Question> SvarMuligheder
        {
            get { return svarMuligheder; }
            set { svarMuligheder = value; }
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

        public IList<Faculty> FacultyList
        {
            get { return _FacultyList; }
            set { _FacultyList = value; }

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
                RaisePropertyChanged("QuizList");
            }
        }


        public ICommand GemogNaeste { get; set; }
        public ICommand GemogForrige { get; set; }
        public ICommand GemMCQ { get; set; }

        public ICommand NySvarmulighed { get; set; }




        public ICommand SelectFaculityCommand { get; set; }

        public MakeNewQuizViewModel()
        {
            GemogNaeste = new Command(Naeste,CanExecute);
            GemogForrige = new Command(Forrige, CanExecute);
            GemMCQ = new Command(GEMMCQ, CanExecute);
            SelectFaculityCommand = new Command(SelectFaculity, CanExecute);
            NySvarmulighed = new DelegateCommand(nySvar,CanExecute);
            GetFaculties();

        }


        public async void GetFaculties()
        {
            FacultyList = await Data.Faculty.GetAllAsync();
        }

        public async void SelectFaculity(object parameter)
        {
            StudyList = new List<Study>();
            SemesterList = new List<Semester>();
            CourseList = new List<Course>();
            int id = Convert.ToInt32(parameter);
            StudyList = (await Data.Study.GetAllAsync()).FindAll(s => s.FacultyID == id);
            // .Result.Where(s => s.FacultyID == id).ToList()
        }


        private bool CanExecute(object o)
        {
            return true;
        }

        private void Naeste(object o)
        {
            
        }

        private void Forrige(object o)
        {

        }

        private void GEMMCQ(object o)
        {

        }

        private Study selectedStudy;
        private Semester selectedSemester;
        private Course selectedCourse;
        private Faculty selectedFaculty;
        public Faculty SelectedFaculty
        {
            get { return selectedFaculty; }
            set
            {
                selectedFaculty = value;

            }
        }

        public Course SelectedCourse
        {
            get { return selectedCourse; }
            set
            {
                selectedCourse = value;

            }
        }

        public Study SelectedStudy
        {
            get { return selectedStudy; }
            set
            {
                selectedStudy = value;
                SelectStudy();
            }
        }
        public Semester SelectedSemester
        {
            get { return selectedSemester; }
            set
            {
                selectedSemester = value;
                SelectSemester();
            }
        }

        public async void SelectStudy()
        {

            if (SelectedStudy == null)
            {
                return;
            }
            SemesterList = new List<Semester>();
            CourseList = new List<Course>();
            QuizList = new List<Quiz>();
            int id = Convert.ToInt32(SelectedStudy.StudyID);
            SemesterList = (await Data.Semester.GetAllAsync()).FindAll(s => s.StudyID == id);
        }

        public async void SelectSemester()
        {

            if (SelectedSemester == null)
            {
                return;
            }
            CourseList = new List<Course>();
            QuizList = new List<Quiz>();
            int id = Convert.ToInt32(SelectedSemester.SemesterID);
            CourseList = (await Data.Course.GetAllAsync()).FindAll(s => s.SemesterID == id);
        }


        private string SvarMul1 { get; set; }
        public string SvatMul1 { get; set; }


    }
}
