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
    public class TestRatings
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
        public void TestConstructor()
        {
            AnswerQuizQuestionViewModel TestModel = new AnswerQuizQuestionViewModel(testQuiz)
            {};

            Assert.That(TestModel.SelectedQuiz.QuizID, Is.EqualTo(2));
            Assert.That(TestModel.SelectedQuiz.Description, Is.EqualTo("TestDescribtion"));
            Assert.That(TestModel.SelectedQuiz.Name, Is.EqualTo("TestQuiz"));
        }

        [Test]
        public async void testGetRatingsByUserID() 
        {
            AnswerQuizQuestionViewModel TestModel = new AnswerQuizQuestionViewModel(testQuiz)
            { };


            Assert.That(TestModel.SelectedQuiz.QuizID, Is.EqualTo(2));

            List<Rating> resultList = new List<Rating>();

            resultList = await data.Rating.GetRatingByQuizID(TestModel.SelectedQuiz.QuizID);

            Assert.That(resultList, Is.EqualTo(2));
        }
        [Test]
        public async void testGetAllAsync()
        {
            AnswerQuizQuestionViewModel TestModel = new AnswerQuizQuestionViewModel(testQuiz)
            { };

            Assert.That(TestModel.SelectedQuiz.QuizID, Is.EqualTo(2));

            List<Rating> resultList = new List<Rating>();

            resultList = await data.Rating.GetAllAsync();

            Assert.That(resultList.Count, Is.EqualTo(2));
        }
        [Test]
        public async void testGetAsync()
        {
            AnswerQuizQuestionViewModel TestModel = new AnswerQuizQuestionViewModel(testQuiz)
            { };

            Assert.That(TestModel.SelectedQuiz.QuizID, Is.EqualTo(2));

            Rating Rating = new Rating();

            Rating = await data.Rating.GetAsync(1);

            Assert.That(Rating.Reason, Is.EqualTo("den er super god, føler jeg har læst alt op"));
            Assert.That(Rating.RatingID, Is.EqualTo(1));
            Assert.That(Rating.Rating1, Is.EqualTo(5));
            Assert.That(Rating.QuizID, Is.EqualTo(2));
        }

        public async void testAddUpdateRemove()
        {
            AnswerQuizQuestionViewModel TestModel = new AnswerQuizQuestionViewModel(testQuiz)
            { };

            Assert.That(TestModel.SelectedQuiz.QuizID, Is.EqualTo(2)); // Sikre rigtige bruger

            Rating testRating = new Rating()
            {
               Reason = "Mangler UT",
               RatingID = 999,
               Rating1 = 1
            };

            await data.Rating.Add(testRating); //Gemmer Quiz

            Rating newrating = await data.Rating.GetAsync(999); //henter Quiz med ID 2

            Assert.That(newrating.Reason, Is.EqualTo("Mangler UT"));
            Assert.That(newrating.Rating1, Is.EqualTo(1));
            Assert.That(newrating.RatingID, Is.EqualTo(999));

            //update testes her

            Rating testChangedRating = new Rating()
            {
                Reason = "Mangler Flere UT",
                RatingID = 999,
                Rating1 = 2
            };

            await data.Rating.Update(999, testChangedRating); //ændre quizzen 

            Rating assertRating = await data.Rating.GetAsync(999); //henter den ændrede Quiz

            Assert.That(assertRating.Reason, Is.EqualTo("Mangler Flere UT"));
            Assert.That(assertRating.Rating1, Is.EqualTo(2));
            Assert.That(assertRating.RatingID, Is.EqualTo(999));

            //delete testes her 

            List<Rating> resultList = new List<Rating>();

            resultList = await data.Rating.GetAllAsync();

            int currentSize = resultList.Count;

            data.Rating.Remove(999);

            resultList = await data.Rating.GetAllAsync();

            Assert.That(resultList.Count, Is.EqualTo(currentSize-1));
        }
    }
}
