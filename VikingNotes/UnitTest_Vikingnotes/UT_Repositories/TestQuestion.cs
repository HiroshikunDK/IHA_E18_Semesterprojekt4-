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
    public class TestQuestion
    {
        private IUnitOfWork data = new DAL.Presistence.UnitOfWork() { };
        private List<Answer> listAnswers = new List<Answer>();
        

        [SetUp]
        public void setupTestEnviorment()
        {
            Question testQuestion = new Question()
            {
               QuestionID = 4,
               Question1 = "Hvem er grundlæggeren af LINUX ?",
               QuizID = 3,
               WrongCount = 0,
               CorrectCount = 0
            };
        }

        [Test]
        public async void testGetAllAsync()
        {
            List<Question> resultList = new List<Question>();

            resultList = await data.Question.GetAllAsync();

            Assert.That(resultList.Count, Is.EqualTo(19)); // hvertfald i mit test script
        }

        [Test]
        public async void testGetAsync()
        {
            Question User = new Question();

            User = await data.Question.GetAsync(4);

            Assert.That(User.QuestionID, Is.EqualTo(4));
            Assert.That(User.Question1, Is.EqualTo("Hvem er grundlæggeren af LINUX ?"));
            Assert.That(User.QuizID, Is.EqualTo(3));
            Assert.That(User.WrongCount, Is.EqualTo(0));
            Assert.That(User.CorrectCount, Is.EqualTo(0));
        }

        [Test]
        public async void testAddUpdateRemove()
        {
            Question testNewQuestion = new Question()
            {
                QuestionID = 999,
                Question1 = "Hvorfor UnitTest?",
                QuizID = 3,
                WrongCount = 1,
                CorrectCount = 1
            };

            await data.Question.Add(testNewQuestion); //Gemmer bruger

            Question resultQuestion = await data.Question.GetAsync(999); //henter Quiz med ID 2

            Assert.That(resultQuestion.QuestionID, Is.EqualTo(999));
            Assert.That(resultQuestion.Question1, Is.EqualTo("Hvorfor UnitTest?"));
            Assert.That(resultQuestion.QuizID, Is.EqualTo(3));
            Assert.That(resultQuestion.WrongCount, Is.EqualTo(1));
            Assert.That(resultQuestion.CorrectCount, Is.EqualTo(1));
            //update testes her

            Question testChangedQuestion = new Question()
            {
                QuestionID = 999,
                Question1 = "Hvorfor ikke UnitTest?",
                QuizID = 3,
                WrongCount = 2,
                CorrectCount = 2
            };

            await data.Question.Update(999, testChangedQuestion); //ændre quizzen 

            Userr assertUser = await data.User.GetAsync(999); //henter den ændrede Quiz

            Assert.That(resultQuestion.QuestionID, Is.EqualTo(999));
            Assert.That(resultQuestion.Question1, Is.EqualTo("Hvorfor ikke UnitTest?"));
            Assert.That(resultQuestion.QuizID, Is.EqualTo(3));
            Assert.That(resultQuestion.WrongCount, Is.EqualTo(2));
            Assert.That(resultQuestion.CorrectCount, Is.EqualTo(2));
            //delete testes her 

            List<Question> resultList = new List<Question>();

            resultList = await data.Question.GetAllAsync();

            int currentSize = resultList.Count;

            data.Question.Remove(999);

            resultList = await data.Question.GetAllAsync();

            Assert.That(resultList.Count, Is.EqualTo(currentSize-1));
        }
    }
}
