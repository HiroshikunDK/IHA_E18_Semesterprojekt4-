using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using ViewModels;
using DAL.Core;
using DAL.Core.Repositories;
using RESTfullWebApi.Models;

namespace NUnit.Framework
{
    [TestFixture]
    public class TestQuizzes
    {
        private IUnitOfWork data = new DAL.Presistence.UnitOfWork() { };

        [SetUp]
        public void setupTestEnviorment()
        {


            Userr testUser = new Userr()
            {
                UserID = 3, //bruges fordi der ligger quizzes til dette bruger ID
                UserName = "Test Bruger", 
                Password = "1337M4CH1N3",
                EmailAdress = "testmail@test.com", 
                UserTypeID = 1337,
                StudyID = 1337, 
                StudentNumber = "1337", 
                Salt = "No scooped by a 9 year old" 
            };

            data.LoginService.User = testUser;
        }

        [Test]
        public async void testGetQuizzesByUserID() //costom kald 
        {
            YourStatisticsViewModel TestModel = new YourStatisticsViewModel(data)
            {
                Userr = data.LoginService.User
            };

            Assert.That(TestModel.Userr.UserID, Is.EqualTo(3));

            TestModel.Quizzes = await data.Quiz.GetQuizzesByUserID(TestModel.Userr.UserID);

            Assert.That(TestModel.Quizzes.Count, Is.EqualTo(13));
        }
        [Test]
        public async void testGetAllAsync()
        {
            YourStatisticsViewModel TestModel = new YourStatisticsViewModel(data)
            {
                Userr = data.LoginService.User
            };

            Assert.That(TestModel.Userr.UserID, Is.EqualTo(3));

            TestModel.Quizzes = await data.Quiz.GetAllAsync();

            Assert.That(TestModel.Quizzes.Count, Is.EqualTo(15));
        }

        [Test]
        public async void testGetAsync()
        {
            YourStatisticsViewModel TestModel = new YourStatisticsViewModel(data)
            {
                Userr = data.LoginService.User
            };

            Assert.That(TestModel.Userr.UserID, Is.EqualTo(3)); // Sikre rigtige bruger

            TestModel.Quiz = await data.Quiz.GetAsync(2); //henter Quiz med ID 2

            Assert.That(TestModel.Quiz.Name, Is.EqualTo("unit testing"));
        }

        [Test]
        public async void testAddUpdateRemove()
        {
            YourStatisticsViewModel TestModel = new YourStatisticsViewModel(data)
            {
                Userr = data.LoginService.User
            };

            Assert.That(TestModel.Userr.UserID, Is.EqualTo(3)); // Sikre rigtige bruger

            Quiz testQuiz = new Quiz()
            {
               Name = "UnitTest",
               QuizID = 999,
            };

            await data.Quiz.Add(testQuiz); //Gemmer Quiz

            TestModel.Quiz = await data.Quiz.GetAsync(999); //henter Quiz med ID 2

            Assert.That(TestModel.Quiz.Name, Is.EqualTo("UnitTest"));

            //update testes her

            Quiz testChangedQuiz = new Quiz()
            {
                Name = "UnitTest1",
                QuizID = 999,
            };

            await data.Quiz.Update(999, testChangedQuiz); //ændre quizzen 

            TestModel.Quiz = await data.Quiz.GetAsync(999); //henter den ændrede Quiz

            Assert.That(TestModel.Quiz.Name, Is.EqualTo("UnitTest1"));
            
            //delete testes her 

            TestModel.Quizzes = await data.Quiz.GetAllAsync();

            int currentSize = TestModel.Quizzes.Count;

            data.Quiz.Remove(999);

            TestModel.Quizzes = await data.Quiz.GetAllAsync();

            Assert.That(TestModel.Quizzes.Count, Is.EqualTo(currentSize-1));
        }
    }
}
