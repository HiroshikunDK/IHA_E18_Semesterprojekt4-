using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System.Collections.Generic;
using ViewModels;
using DAL.Core;
using DAL.Core.Repositories;
using RESTfullWebApi.Models;

namespace NUnit.Framework
{
    [TestFixture]
    public class TestUser
    {
        private IUnitOfWork data = new DAL.Presistence.UnitOfWork() { };
        private Quiz testQuiz = new Quiz()
        {
            QuizID = 2,
            Description = "TestDescribtion",
            Name = "TestQuiz"
        };

        [SetUp]
        public void setupTestEnviorment()
        {


            Userr testUser = new Userr()
            {
                UserID = 999, //bruges fordi der ligger quizzes til dette bruger ID
                UserName = "Test Bruger", 
                Password = "1337M4CH1N3",
                EmailAdress = "testmail@test.com", 
                UserTypeID = 1,
                StudyID = 1337, 
                StudentNumber = "1337", 
                Salt = "No scooped by a 9 year old" 
            };

            data.LoginService.User = testUser;
        }

        [Test]
        public void TestConstructor()
        {
            LoginViewModel TestModel = new LoginViewModel(data)
            { };

            Assert.That(data.LoginService.User.UserID, Is.EqualTo(2));
        }

        [Test]
        public async void testGetAllAsync()
        {
            LoginViewModel TestModel = new LoginViewModel(data)
            { };

            Assert.That(data.LoginService.User.UserID, Is.EqualTo(2));

            List<Userr> resultList = new List<Userr>();

            resultList = await data.User.GetAllAsync();

            Assert.That(resultList.Count, Is.EqualTo(7)); // dette er et gæt
        }

        [Test]
        public async void testGetAsync()
        {
            LoginViewModel TestModel = new LoginViewModel(data)
            { };

            Assert.That(data.LoginService.User.UserID, Is.EqualTo(2));

            Userr User = new Userr();

            User = await data.User.GetAsync(1);

            Assert.That(User.UserID, Is.EqualTo(1));
            Assert.That(User.UserName, Is.EqualTo("SvendTheMan"));
            Assert.That(User.Password, Is.EqualTo("123456"));
            Assert.That(User.EmailAdress, Is.EqualTo("Svenden@gmail.com"));
            Assert.That(User.UserTypeID, Is.EqualTo(1));
            Assert.That(User.StudyID, Is.EqualTo(1));
            Assert.That(User.StudentNumber, Is.EqualTo("201110642"));
            Assert.That(User.AuthToken, Is.EqualTo("s34242d2"));
            Assert.That(User.Salt, Is.EqualTo("asdadsads"));
        }

        [Test]
        public async void testAddUpdateRemove()
        {
            AnswerQuizQuestionViewModel TestModel = new AnswerQuizQuestionViewModel(testQuiz)
            { };

            Assert.That(TestModel.SelectedQuiz.QuizID, Is.EqualTo(2)); // Sikre rigtige bruger

            Userr testUser = new Userr()
            {
                UserID = 999, 
                UserName = "Test Bruger",
                Password = "1337M4CH1N3",
                EmailAdress = "testmail@test.com",
                StudentNumber = "1337",
                Salt = "No scooped by a 9 year old"
            };

            await data.User.Add(testUser); //Gemmer bruger

            Userr resultUser = await data.User.GetAsync(999); //henter Quiz med ID 2

            Assert.That(resultUser.UserID, Is.EqualTo(999));
            Assert.That(resultUser.UserName, Is.EqualTo("Test Bruger"));
            Assert.That(resultUser.Password, Is.EqualTo("1337M4CH1N3"));
            Assert.That(resultUser.EmailAdress, Is.EqualTo("testmail@test.com"));
            Assert.That(resultUser.StudentNumber, Is.EqualTo("1337"));
            Assert.That(resultUser.Salt, Is.EqualTo("No scooped by a 9 year old"));

            //update testes her

            Userr testChangedUser = new Userr()
            {
                UserID = 999,
                UserName = "Test Bruger1",
                Password = "1337M4CH1N35",
                EmailAdress = "testmail@test1.com",
                StudentNumber = "13375",
                Salt = "No scooped by a 8 year old"
            };

            await data.User.Update(999, testChangedUser); //ændre quizzen 

            Userr assertUser = await data.User.GetAsync(999); //henter den ændrede Quiz

            Assert.That(assertUser.UserID, Is.EqualTo(999));
            Assert.That(assertUser.UserName, Is.EqualTo("Test Bruger1"));
            Assert.That(assertUser.Password, Is.EqualTo("1337M4CH1N35"));
            Assert.That(assertUser.EmailAdress, Is.EqualTo("testmail@test1.com"));
            Assert.That(assertUser.StudentNumber, Is.EqualTo("13375"));
            Assert.That(assertUser.Salt, Is.EqualTo("No scooped by a 8 year old"));

            //delete testes her 

            List<Userr> resultList = new List<Userr>();

            resultList = await data.User.GetAllAsync();

            int currentSize = resultList.Count;

            data.User.Remove(999);

            resultList = await data.User.GetAllAsync();

            Assert.That(resultList.Count, Is.EqualTo(currentSize-1));
        }
    }
}
