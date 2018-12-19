using RESTfullWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using DAL.Core;
using DAL.Presistence;
using ViewModels.Commands;

namespace ViewModels
{
    public class MakeNewQuizViewModel : BaseViewModel
    {

        public ICommand GemogNaeste { get; set; }
        public ICommand GemogForrige { get; set; }
        public ICommand GemMCQ { get; set; }


        private IList<Study> _studyList { set; get; }
        private IList<Faculty> _FacultyList { set; get; }
        private IList<Semester> _semesterList { set; get; }
        private IList<Course> _courseList { set; get; }
        private IList<Quiz> _quizList { set; get; }

        private IList<int> _answerCountList;

        private int answerCount;

        private int buttonRowIndex;

        private string quizName { get; set; }

        private string description { get; set; }

        private Quiz CurrentQuiz = new Quiz();
        private Question newQuestion { get; set; }

        private int selectedQuestionIndex { get; set; }

        private int questionListIndex { get; set; }

        private Userr user { get; set; }
        private IUnitOfWork Data { get; set; }

        private Study selectedStudy;
        private Semester selectedSemester;
        private Course selectedCourse;
        private Faculty selectedFaculty;

        private Question selectedQuestion;




        public MakeNewQuizViewModel(IUnitOfWork data)
        {
            Data = data;
            user = Data.LoginService.User;
            
            GetStudies();
            

            ButtonRowIndex = 9;
            AnswerCountList = new List<int>(){ 4, 6, 8};
            AnswerCount = 4;
            
            QuestionList = new List<Question>();
            QuestionName = "";
            QuizName = "";
            selectedQuestionIndex = 0;
            QuestionCounter = "Spørgsmål " + (selectedQuestionIndex + 1) + "/" + (QuestionList.Count + 1);

            GemogNaeste = new Command(Naeste, CanExecute);
            GemogForrige = new Command(Forrige, CanExecuteForrige);
            GemMCQ = new Command(GEMMCQ, CanExecuteSave);         
        }

        private void ClearQuestionsBoxes()
        {
            QuestionName = "";
            SvarMul1 = "";
            SvarMul2 = "";
            SvarMul3 = "";
            SvarMul4 = "";

            SvarMul1IsCorrect = false;
            SvarMul2IsCorrect = false;
            SvarMul3IsCorrect = false;
            SvarMul4IsCorrect = false;
        }
        private void Naeste(object o)
        {
            selectedQuestionIndex++;
            //if (selectedQuestionIndex == QuestionList.Count)
            //{
            //    QuestionName = "";
            //    SvarMul1 = "";
            //    SvarMul2 = "";
            //    SvarMul3 = "";
            //    SvarMul4 = "";

            //    QuestionCounter = "Spørgsmål " + (selectedQuestionIndex + 1) + "/" + (QuestionList.Count + 1);
            //    selectedQuestion = null;
            //    RaisePropertyChanged("SelectedQuestion");
            //}
            //else 
            if (selectedQuestionIndex > QuestionList.Count)
            {
                newQuestion = new Question();
                newQuestion.Answers.Add(new Answer() { Answer1 = svarMul1, IsCorrect = isCorrectString(SvarMul1IsCorrect) });
                newQuestion.Answers.Add(new Answer() { Answer1 = svarMul2, IsCorrect = isCorrectString(SvarMul2IsCorrect) });
                newQuestion.Answers.Add(new Answer() { Answer1 = svarMul3, IsCorrect = isCorrectString(SvarMul3IsCorrect) });
                newQuestion.Answers.Add(new Answer() { Answer1 = svarMul4, IsCorrect = isCorrectString(SvarMul4IsCorrect) });

                newQuestion.Question1 = QuestionName;

                ClearQuestionsBoxes();

                //CurrentQuiz.Questions.Add(newQuestion);
                List<Question> tempList = new List<Question>(questionList);

                tempList.Add(newQuestion);

                QuestionList = tempList;

                QuestionCounter = "Spørgsmål " + (selectedQuestionIndex + 1) + "/" + (QuestionList.Count + 1);
                selectedQuestion = null;
                RaisePropertyChanged("SelectedQuestion");
            }
            else
            {
                newQuestion = new Question();
                newQuestion.Answers.Add(new Answer() { Answer1 = svarMul1, IsCorrect = isCorrectString(SvarMul1IsCorrect) });
                newQuestion.Answers.Add(new Answer() { Answer1 = svarMul2, IsCorrect = isCorrectString(SvarMul2IsCorrect) });
                newQuestion.Answers.Add(new Answer() { Answer1 = svarMul3, IsCorrect = isCorrectString(SvarMul3IsCorrect) });
                newQuestion.Answers.Add(new Answer() { Answer1 = svarMul4, IsCorrect = isCorrectString(SvarMul4IsCorrect) }); ;

                newQuestion.Question1 = QuestionName;

                List<Question> tempList = new List<Question>(QuestionList);

                tempList[selectedQuestionIndex - 1] = newQuestion;

                QuestionList = tempList;

                if (selectedQuestionIndex == QuestionList.Count)
                {
                    QuestionCounter = "Spørgsmål " + (selectedQuestionIndex + 1) + "/" + (QuestionList.Count + 1);
                    selectedQuestion = null;
                    RaisePropertyChanged("SelectedQuestion");
                    ClearQuestionsBoxes();

                }
                else
                {
                    QuestionCounter = "Spørgsmål " + (selectedQuestionIndex + 1) + "/" + QuestionList.Count;
                    selectedQuestion = QuestionList[selectedQuestionIndex];
                    RaisePropertyChanged("SelectedQuestion");
                    QuestionName = SelectedQuestion.Question1;
                    List<Answer> tempAnswerList = QuestionList[selectedQuestionIndex].Answers.ToList();
                    SvarMul1 = tempAnswerList[0].Answer1;
                    SvarMul2 = tempAnswerList[1].Answer1;
                    SvarMul3 = tempAnswerList[2].Answer1;
                    SvarMul4 = tempAnswerList[3].Answer1;

                    SvarMul1IsCorrect = isCorrectBool(tempAnswerList[0].IsCorrect);
                    SvarMul2IsCorrect = isCorrectBool(tempAnswerList[1].IsCorrect);
                    SvarMul3IsCorrect = isCorrectBool(tempAnswerList[2].IsCorrect);
                    SvarMul4IsCorrect = isCorrectBool(tempAnswerList[3].IsCorrect);
                }
            }

        }

        private void Forrige(object o)
        {
            if (selectedQuestionIndex == QuestionList.Count)
            {
                if (QuestionName != "")
                {
                    newQuestion = new Question();
                    newQuestion.Answers.Add(new Answer() { Answer1 = svarMul1, IsCorrect = isCorrectString(SvarMul1IsCorrect) });
                    newQuestion.Answers.Add(new Answer() { Answer1 = svarMul2, IsCorrect = isCorrectString(SvarMul2IsCorrect) });
                    newQuestion.Answers.Add(new Answer() { Answer1 = svarMul3, IsCorrect = isCorrectString(SvarMul3IsCorrect) });
                    newQuestion.Answers.Add(new Answer() { Answer1 = svarMul4, IsCorrect = isCorrectString(SvarMul4IsCorrect) });

                    newQuestion.Question1 = QuestionName;

                    //CurrentQuiz.Questions.Add(newQuestion);
                    List<Question> tempList = new List<Question>(questionList);

                    tempList.Add(newQuestion);

                    QuestionList = tempList;
                }
            }
            else if (selectedQuestion != null)
            {
                newQuestion = new Question();
                newQuestion.Answers.Add(new Answer() { Answer1 = svarMul1, IsCorrect = isCorrectString(SvarMul1IsCorrect) });
                newQuestion.Answers.Add(new Answer() { Answer1 = svarMul2, IsCorrect = isCorrectString(SvarMul2IsCorrect) });
                newQuestion.Answers.Add(new Answer() { Answer1 = svarMul3, IsCorrect = isCorrectString(SvarMul3IsCorrect) });
                newQuestion.Answers.Add(new Answer() { Answer1 = svarMul4, IsCorrect = isCorrectString(SvarMul4IsCorrect) });

                newQuestion.Question1 = QuestionName;

                List<Question> tempList = new List<Question>(QuestionList);

                tempList[selectedQuestionIndex] = newQuestion;

                QuestionList = tempList;
            }

            selectedQuestionIndex = selectedQuestionIndex - 1;
            selectedQuestion = QuestionList[selectedQuestionIndex];
            RaisePropertyChanged("SelectedQuestion");
            QuestionName = QuestionList[selectedQuestionIndex].Question1;
            List<Answer> tempAnswerList = QuestionList[selectedQuestionIndex].Answers.ToList();
            SvarMul1 = tempAnswerList[0].Answer1;
            SvarMul2 = tempAnswerList[1].Answer1;
            SvarMul3 = tempAnswerList[2].Answer1;
            SvarMul4 = tempAnswerList[3].Answer1;

            SvarMul1IsCorrect = isCorrectBool(tempAnswerList[0].IsCorrect);
            SvarMul2IsCorrect = isCorrectBool(tempAnswerList[1].IsCorrect);
            SvarMul3IsCorrect = isCorrectBool(tempAnswerList[2].IsCorrect);
            SvarMul4IsCorrect = isCorrectBool(tempAnswerList[3].IsCorrect);

            QuestionCounter = "Spørgsmål " + (selectedQuestionIndex + 1) + "/" + QuestionList.Count;
        }

        private async void GEMMCQ(object o)
        {
            CurrentQuiz.CourseID = SelectedCourse.CourseID;
            CurrentQuiz.Name = QuizName;
            CurrentQuiz.Questions = QuestionList;
            CurrentQuiz.UserID = Data.LoginService.User.UserID;
            CurrentQuiz.Description = Description;
            await Data.Quiz.Add(CurrentQuiz);
            MessageBox.Show("Quiz Saved!");

            CurrentQuiz = new Quiz();

            QuizName = "";
            Description = "";
            QuestionList = new List<Question>();

            selectedQuestionIndex = 0;

            ClearQuestionsBoxes();
        }

        private string isCorrectString(bool isCorrect)
        {
            if (isCorrect)
            {
                return "1";
            }

            return "0";
        }

        private bool isCorrectBool(string isCorrect)
        {
            if (isCorrect == "1")
            {
                return true;
            }
            return false;
        }


        #region Can Execute Functions
        
        private bool CanExecute(object o)
        {
            return true;
        }

        private bool CanExecuteForrige(object o)
        {
            if (selectedQuestionIndex > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CanExecuteSave(object o)
        {
            if (QuizName != "" && QuestionList.Count > 0 && SelectedCourse != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Public Props

        

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                RaisePropertyChanged("Description");
            }
        }


        public int ButtonRowIndex
        {
            get { return buttonRowIndex; }
            set
            {
                buttonRowIndex = value;
                RaisePropertyChanged("ButtonRowIndex");
            }
        }


        public int AnswerCount
        {
            get { return answerCount; }
            set
            {
                answerCount = value;
                RaisePropertyChanged("AnswerCount");
            }
        }


        public IList<int> AnswerCountList
        {
            get { return _answerCountList; }
            set
            {
                _answerCountList = value;
                RaisePropertyChanged("AnswerCountList");
            }
        }

        public int QuestionListIndex
        {
            get { return questionListIndex; }
            set
            {
                questionListIndex = value;
                RaisePropertyChanged("QuestionListIndex");
            }
        }

        private string questionCounter;

        public string QuestionCounter
        {
            get { return questionCounter; }
            set
            {
                questionCounter = value;
                RaisePropertyChanged("QuestionCounter");
            }
        }


        public string QuizName
        {
            get { return quizName; }
            set
            {
                quizName = value;
                RaisePropertyChanged("QuizName");
            }
        }

        private string questionName { get; set; }

        public string QuestionName
        {
            get { return questionName;}
            set
            {
                questionName = value;
                RaisePropertyChanged("QuestionName");
            }

        }

        private IList<Question> questionList { get; set; }

        public IList<Question> QuestionList
        {
            get { return questionList;}
            set
            {
                questionList = value;
                RaisePropertyChanged("QuestionList");
            }
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
                if (selectedStudy == null)
                {
                    SelectedStudy = StudyList.FirstOrDefault(s => s.StudyID == user.StudyID);
                }
            }
        }

        public IList<Faculty> FacultyList
        {
            get { return _FacultyList; }
            set
            {
                _FacultyList = value;
                RaisePropertyChanged("FacultyList");
                if (selectedFaculty == null && SelectedStudy != null)
                {
                    SelectedFaculty = FacultyList.FirstOrDefault(f => f.FacultyID == SelectedStudy.FacultyID);
                }
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


        public Question SelectedQuestion
        {
            get { return selectedQuestion; }
            set
            {
                selectedQuestion = value;
                RaisePropertyChanged("SelectedQuestion");
                //if (QuestionListIndex != -1)
                //{
                //    SelectQuestion(selectedQuestion);
                //}
            }
        }

        public Faculty SelectedFaculty
        {
            get { return selectedFaculty; }
            set
            {
                selectedFaculty = value;
                RaisePropertyChanged("SelectedFaculty");
                //SelectFaculity(selectedFaculty.FacultyID);
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


        public Study SelectedStudy
        {
            get { return selectedStudy; }
            set
            {
                selectedStudy = value;
                RaisePropertyChanged("SelectedStudy");
                GetFaculties();
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

        #endregion

        #region SelectFunctions

        public async void SelectStudy()
        {

            if (SelectedStudy == null)
            {
                return;
            }
            SemesterList = new List<Semester>();
            CourseList = new List<Course>();
           
            int id = Convert.ToInt32(SelectedStudy.StudyID);
            SemesterList = (await Data.Semester.GetAllAsync()).FindAll(s => s.StudyID == id);
            foreach (var semester in SemesterList)
            {
                semester.SemesterNumber = semester.SemesterNumber.Trim();
            }
        }

        public async void SelectSemester()
        {

            if (SelectedSemester == null)
            {
                return;
            }
            CourseList = new List<Course>();
            
            int id = Convert.ToInt32(SelectedSemester.SemesterID);
            CourseList = (await Data.Course.GetAllAsync()).FindAll(s => s.SemesterID == id);
            foreach (var course in CourseList)
            {
                course.Name = course.Name.Trim();
            }
        }

        public async void SelectCourse()
        {

            if (SelectedCourse == null)
            {
                return;
            }
            

            //CourseList = (await Data.Catagory.GetAllAsync()).FindAll(c => c.);
            foreach (var course in CourseList)
            {
                course.Name = course.Name.Trim();
            }
        }

        public async void GetFaculties()
        {
            FacultyList = await Data.Faculty.GetAllAsync();
            foreach (var faculty in FacultyList)
            {
                faculty.Name = faculty.Name.Trim();
            }
        }

        public async void GetStudies()
        {
            StudyList = await Data.Study.GetAllAsync();
            foreach (var study in StudyList)
            {
                study.Name = study.Name.Trim();
            }
        }

        public async void SelectFaculity(long id)
        {
            StudyList = new List<Study>();
            SemesterList = new List<Semester>();
            CourseList = new List<Course>();
            StudyList = (await Data.Study.GetAllAsync()).FindAll(s => s.FacultyID == id);
            // .Result.Where(s => s.FacultyID == id).ToList()
            foreach (var study in StudyList)
            {
                study.Name = study.Name.Trim();
            }
        }

        #endregion

        #region Answer Props


        private string svarMul1 { get; set; }
        private string svarMul2 { get; set; }
        private string svarMul3 { get; set; }
        private string svarMul4 { get; set; }

        private bool svarMul1IsCorrect { get; set; }
        private bool svarMul2IsCorrect { get; set; }
        private bool svarMul3IsCorrect { get; set; }
        private bool svarMul4IsCorrect { get; set; }

        public bool SvarMul1IsCorrect
        {
            get { return svarMul1IsCorrect; }
            set
            {
                svarMul1IsCorrect = value;
                RaisePropertyChanged("SvarMul1IsCorrect");
            }
        }
        public bool SvarMul2IsCorrect
        {
            get { return svarMul2IsCorrect; }
            set
            {
                svarMul2IsCorrect = value;
                RaisePropertyChanged("SvarMul2IsCorrect");
            }
        }
        public bool SvarMul3IsCorrect
        {
            get { return svarMul3IsCorrect; }
            set
            {
                svarMul3IsCorrect = value;
                RaisePropertyChanged("SvarMul3IsCorrect");
            }
        }
        public bool SvarMul4IsCorrect
        {
            get { return svarMul4IsCorrect; }
            set
            {
                svarMul4IsCorrect = value;
                RaisePropertyChanged("SvarMul4IsCorrect");
            }
        }

        public string SvarMul1
        {
            get { return svarMul1; }
            set
            {
                svarMul1 = value;
                RaisePropertyChanged("SvarMul1");
            }
        }
        
        public string SvarMul2
        {
            get { return svarMul2; }
            set
            {
                svarMul2 = value;
                RaisePropertyChanged("SvarMul2");
            }
        }
        
        public string SvarMul3
        {
            get { return svarMul3; }
            set
            {
                svarMul3 = value;
                RaisePropertyChanged("SvarMul3");
            }
        }
        
        public string SvarMul4
        {
            get { return svarMul4; }
            set
            {
                svarMul4 = value;
                RaisePropertyChanged("SvarMul4");
            }
        }

        #endregion
    }
}
