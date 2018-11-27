using RESTfullWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
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


        private int selectedQuestionIndex { get; set; }

        private int questionListIndex { get; set; }

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
            set { quizName = value; }
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
            }
        }

        public IList<Faculty> FacultyList
        {
            get { return _FacultyList; }
            set
            {
                _FacultyList = value;
                RaisePropertyChanged("FacultyList");
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
       


        public ICommand GemogNaeste { get; set; }
        public ICommand GemogForrige { get; set; }
        public ICommand GemMCQ { get; set; }

        public ICommand NySvarmulighed { get; set; }




        public ICommand SelectFaculityCommand { get; set; }
        private Quiz CurrentQuiz=new Quiz();
        private Question newQuestion { get; set; }

        
        public MakeNewQuizViewModel()
        {
            QuestionList = new List<Question>();
            QuestionName = "";
            selectedQuestionIndex = 0;
            QuestionCounter = "Spørgsmål " + (selectedQuestionIndex + 1)  + "/" + (QuestionList.Count + 1);

            GemogNaeste = new Command(Naeste,CanExecute);
            GemogForrige = new Command(Forrige, CanExecuteForrige);
            GemMCQ = new Command(GEMMCQ, CanExecuteSave);
            //SelectFaculityCommand = new Command(SelectFaculity, CanExecute);
            //NySvarmulighed = new DelegateCommand(nySvar,CanExecute);          
           
            GetFaculties();          
            
        }

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


        private List<string> TempList = new List<string>();

        //private void SelectQuestion(Question question)
        //{
        //    if (selectedQuestionIndex == QuestionList.Count)
        //    {
        //        if (QuestionName != "")
        //        {
        //            newQuestion = new Question();
        //            newQuestion.Answers.Add(new Answer() { Answer1 = svarMul1 });
        //            newQuestion.Answers.Add(new Answer() { Answer1 = svarMul2 });
        //            newQuestion.Answers.Add(new Answer() { Answer1 = svarMul3 });
        //            newQuestion.Answers.Add(new Answer() { Answer1 = svarMul4 });

        //            newQuestion.Question1 = QuestionName;

        //            //CurrentQuiz.Questions.Add(newQuestion);
        //            List<Question> tempList = new List<Question>(questionList);

        //            tempList.Add(newQuestion);

        //            QuestionList = tempList;
        //        }               
        //    }
        //    else if (selectedQuestion != null)
        //    {
        //        newQuestion = new Question();
        //        newQuestion.Answers.Add(new Answer() { Answer1 = svarMul1 });
        //        newQuestion.Answers.Add(new Answer() { Answer1 = svarMul2 });
        //        newQuestion.Answers.Add(new Answer() { Answer1 = svarMul3 });
        //        newQuestion.Answers.Add(new Answer() { Answer1 = svarMul4 });

        //        newQuestion.Question1 = QuestionName;

        //        List<Question> tempList = new List<Question>(QuestionList);

        //        tempList[selectedQuestionIndex] = newQuestion;

        //        questionList = tempList;
        //    }

        //    selectedQuestionIndex = questionListIndex;
        //    selectedQuestion = question;
        //    RaisePropertyChanged("SelectedQuestion");
        //    QuestionName = question.Question1;
        //    List<Answer> tempAnswerList = question.Answers.ToList();
        //    SvarMul1 = tempAnswerList[0].Answer1;
        //    SvarMul2 = tempAnswerList[1].Answer1;
        //    SvarMul3 = tempAnswerList[2].Answer1;
        //    SvarMul4 = tempAnswerList[3].Answer1;

        //    QuestionCounter = "Spørgsmål " + (selectedQuestionIndex + 1) + "/" + QuestionList.Count;

        //}

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
                newQuestion.Answers.Add(new Answer() { Answer1 = svarMul1 });
                newQuestion.Answers.Add(new Answer() { Answer1 = svarMul2 });
                newQuestion.Answers.Add(new Answer() { Answer1 = svarMul3 });
                newQuestion.Answers.Add(new Answer() { Answer1 = svarMul4 });

                newQuestion.Question1 = QuestionName;

                QuestionName = "";
                SvarMul1 = "";
                SvarMul2 = "";
                SvarMul3 = "";
                SvarMul4 = "";

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
                newQuestion.Answers.Add(new Answer() { Answer1 = svarMul1 });
                newQuestion.Answers.Add(new Answer() { Answer1 = svarMul2 });
                newQuestion.Answers.Add(new Answer() { Answer1 = svarMul3 });
                newQuestion.Answers.Add(new Answer() { Answer1 = svarMul4 });

                newQuestion.Question1 = QuestionName;

                List<Question> tempList = new List<Question>(QuestionList);
                
                tempList[selectedQuestionIndex - 1] = newQuestion;

                QuestionList = tempList;

                if (selectedQuestionIndex == QuestionList.Count)
                {
                    QuestionCounter = "Spørgsmål " + (selectedQuestionIndex + 1) + "/" + (QuestionList.Count + 1);
                    selectedQuestion = null;
                    RaisePropertyChanged("SelectedQuestion");
                    QuestionName = "";
                    SvarMul1 = "";
                    SvarMul2 = "";
                    SvarMul3 = "";
                    SvarMul4 = "";
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
                    newQuestion.Answers.Add(new Answer() { Answer1 = svarMul1 });
                    newQuestion.Answers.Add(new Answer() { Answer1 = svarMul2 });
                    newQuestion.Answers.Add(new Answer() { Answer1 = svarMul3 });
                    newQuestion.Answers.Add(new Answer() { Answer1 = svarMul4 });

                    newQuestion.Question1 = QuestionName;

                    //CurrentQuiz.Questions.Add(newQuestion);
                    List<Question> tempList = new List<Question>(questionList);

                    tempList.Add(newQuestion);

                    QuestionList = tempList;
                }
            }else if (selectedQuestion != null)
            {
                newQuestion = new Question();
                newQuestion.Answers.Add(new Answer() { Answer1 = svarMul1 });
                newQuestion.Answers.Add(new Answer() { Answer1 = svarMul2 });
                newQuestion.Answers.Add(new Answer() { Answer1 = svarMul3 });
                newQuestion.Answers.Add(new Answer() { Answer1 = svarMul4 });

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

            QuestionCounter = "Spørgsmål " + (selectedQuestionIndex + 1) + "/" + QuestionList.Count;
        }


        public async void GetFaculties()
        {
            FacultyList = await Data.Faculty.GetAllAsync();
            foreach (var faculty in FacultyList)
            {
                faculty.Name = faculty.Name.Trim();
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



        private void GEMMCQ(object o)
        {

        }

        private Study selectedStudy;
        private Semester selectedSemester;
        private Course selectedCourse;
        private Faculty selectedFaculty;

        private Question selectedQuestion;

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
                SelectFaculity(selectedFaculty.FacultyID);
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


        private string svarMul1 { get; set; }
        public string SvarMul1
        {
            get { return svarMul1; }
            set
            {
                svarMul1 = value;
                RaisePropertyChanged("SvarMul1");
            }
        }
        private string svarMul2 { get; set; }
        public string SvarMul2
        {
            get { return svarMul2; }
            set
            {
                svarMul2 = value;
                RaisePropertyChanged("SvarMul2");
            }
        }
        private string svarMul3 { get; set; }
        public string SvarMul3
        {
            get { return svarMul3; }
            set
            {
                svarMul3 = value;
                RaisePropertyChanged("SvarMul3");
            }
        }
        private string svarMul4 { get; set; }
        public string SvarMul4
        {
            get { return svarMul4; }
            set
            {
                svarMul4 = value;
                RaisePropertyChanged("SvarMul4");
            }
        }





    }
}
