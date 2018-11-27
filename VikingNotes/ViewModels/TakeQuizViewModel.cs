using RESTfullWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DAL.Core;
using DAL.Presistence;
using ViewModels.Commands;

namespace ViewModels
{
    public class TakeQuizViewModel : BaseViewModel
    {
        public string QuizSortBy { get; set; }

        public ICommand SelectFaculityCommand { get; set; }

        public ICommand SelectStudyCommand { get; set; }

        public EventHandler<EventArgs> IsDoingQuiz;

        public EventHandler<EventArgs> FinishedQuiz;

        //public ICommand SelectSemesterCommand { get; set; }
        //public ICommand SelectCourseCommand { get; set; }
        //public ICommand SelectQuizCommand { get; set; }

        private Semester selectedSemester { get; set; }
        private Study selectedStudy { get; set; }
        private Course selectedCourse { get; set; }
        private Quiz selectedQuiz { get; set; }

        private IList<Study> _studyList { set; get; }
        private IList<Semester> _semesterList { set; get; }
        private IList<Course> _courseList { set; get; }
        private IList<Quiz> _quizList { set; get; }

        private IUnitOfWork Data = new UnitOfWork();

        private BaseViewModel quizContent { set; get; }

        private bool isDoingQuiz = false;

        public BaseViewModel QuizContent
        {
            get { return quizContent; }
            set
            {
                quizContent = value;
                RaisePropertyChanged("QuizContent");
            }
        }

        private UserControl answerView { get; set; }

        public TakeQuizViewModel(UserControl ansView)
        {
            _studyList = new List<Study>();
            _semesterList = new List<Semester>();
            _courseList = new List<Course>();
            _quizList = new List<Quiz>();
            SelectFaculityCommand = new Command(SelectFaculity, canExecute);
            answerView = ansView;
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
        public Course SelectedCourse
        {
            get { return selectedCourse; }
            set
            {
                selectedCourse = value;
                SelectCourse();
            }
        }
        public Quiz SelectedQuiz
        {
            get { return selectedQuiz; }
            set
            {
                selectedQuiz = value;
                SelectQuiz(selectedQuiz);
                RaisePropertyChanged("SelectedQuiz");
            }
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
                RaisePropertyChanged("QuizList");
            }
        }

        private bool canExecute(object parameter)
        {
            return !isDoingQuiz;
        }

        public async void SelectFaculity(object parameter)
        {
            StudyList = new List<Study>();
            SemesterList = new List<Semester>();
            CourseList = new List<Course>();
            QuizList = new List<Quiz>();
            int id = Convert.ToInt32(parameter);
            StudyList = (await Data.Study.GetAllAsync()).FindAll(s => s.FacultyID == id);
               // .Result.Where(s => s.FacultyID == id).ToList()
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
                return; }
            CourseList = new List<Course>();
            QuizList = new List<Quiz>();
            int id = Convert.ToInt32(SelectedSemester.SemesterID);
            CourseList = (await Data.Course.GetAllAsync()).FindAll(s => s.SemesterID == id);
        }
        public async void SelectCourse()
        {
            if (SelectedCourse == null)
            {
                return;
            }
            QuizList = new List<Quiz>();
            List<Catagory> catagoriesinList = selectedCourse.Catagories.ToList();
            List<Quiz> tempQuizList = (await Data.Quiz.GetAllAsync());

            QuizList = tempQuizList;

            if (catagoriesinList.Count > 0)
            {
                foreach (var catagory in catagoriesinList)
                {
                    foreach (var quiz in tempQuizList)
                    {
                        if (quiz.Catagory == catagory)
                        {
                            QuizList.Add(quiz);
                        }
                    }
                }
            }
        }

        private void Clear()
        {
            _studyList.Clear();
            _semesterList.Clear();
            _courseList.Clear();
            _quizList.Clear();
        }

        private async void SelectQuiz(Quiz quiz)
        {
            if (quiz != null)
            {
                Quiz quizWithQuestions = quiz;
                quizWithQuestions = await Data.Quiz.GetAsync(quiz.QuizID);

                //int id = 0; //TODO: Getting answers from previous view
                //foreach (var question in quizWithQuestions.Questions)
                //{
                ////    //id = Convert.ToInt32(question.QuestionID);

                ////    //question.Answers = (await Data.Answer.GetAllAsync()).FindAll(a => a.QuestionID == id);
                //    question.Answers = (await Data.Answer.GetAnswerByQuestionID(question.QuestionID));
                //}

                QuizContent = new AnswerQuizQuestionViewModel(quizWithQuestions);
                answerView.DataContext = QuizContent;
                isDoingQuiz = true;
                IsDoingQuiz?.Invoke(this, EventArgs.Empty);

                var quizviewmodel = (AnswerQuizQuestionViewModel)QuizContent;
                quizviewmodel.QuizEndedEvent += HandleQuizEndedEvent;
            }
        }

        private void HandleQuizEndedEvent(object source, QuizEndedEventArgs e)
        {
            MessageBox.Show("it worked", "it worked", MessageBoxButton.OK);
        }

    }
}
