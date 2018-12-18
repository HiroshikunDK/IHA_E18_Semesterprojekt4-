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
    public class TestUserType
    {
        private IUnitOfWork data = new DAL.Presistence.UnitOfWork() { };        

        [SetUp]
        public void setupTestEnviorment()
        {
            Answer testAnswer = new Answer()
            {
                AnswerID = 1,
                Answer1 = "GoF State",
                IsCorrect ="1",
                QuestionID = 1
            };
        }

        [Test]
        public async void testGetAllAsync()
        {
            List<Answer> resultList = new List<Answer>();

            resultList = await data.Answer.GetAllAsync();

            Assert.That(resultList.Count, Is.EqualTo(67)); // hvertfald i mit test script
        }

        [Test]
        public async void testGetAsync()
        {
            Answer answer = new Answer();

            answer = await data.Answer.GetAsync(1);

            Assert.That(answer.AnswerID, Is.EqualTo(1));
            Assert.That(answer.Answer1, Is.EqualTo("GoF State"));
            Assert.That(answer.IsCorrect, Is.EqualTo("1"));
            Assert.That(answer.QuestionID, Is.EqualTo(1));

        }

        [Test]
        public async void testAddUpdateRemove()
        {
            Answer testNewAnswer = new Answer()
            {
                AnswerID = 999,
                Answer1 = "Because UnitTest",
                IsCorrect = "1",
                QuestionID = 1
            };

            await data.Answer.Add(testNewAnswer); //Gemmer bruger

            Answer resultAnswer = await data.Answer.GetAsync(999); //henter Quiz med ID 2

            Assert.That(resultAnswer.AnswerID, Is.EqualTo(999));
            Assert.That(resultAnswer.Answer1, Is.EqualTo("Because UnitTest"));
            Assert.That(resultAnswer.IsCorrect, Is.EqualTo("1"));
            Assert.That(resultAnswer.QuestionID, Is.EqualTo(1));
            //update testes her

            Answer testChangedAnswer = new Answer()
            {
                AnswerID = 999,
                Answer1 = "Because UnitTest is wrong",
                IsCorrect = "0",
                QuestionID = 1
            };

            await data.Answer.Update(999, testChangedAnswer); //ændre quizzen 

            Answer assertAnswer = await data.Answer.GetAsync(999); //henter den ændrede Quiz

            Assert.That(assertAnswer.AnswerID, Is.EqualTo(999));
            Assert.That(assertAnswer.Answer1, Is.EqualTo("Because UnitTest is wrong"));
            Assert.That(assertAnswer.IsCorrect, Is.EqualTo("0"));
            Assert.That(assertAnswer.QuestionID, Is.EqualTo(1));
            //delete testes her 

            List<Answer> resultList = new List<Answer>();

            resultList = await data.Answer.GetAllAsync();

            int currentSize = resultList.Count;

            data.Answer.Remove(999);

            resultList = await data.Answer.GetAllAsync();

            Assert.That(resultList.Count, Is.EqualTo(currentSize-1));
        }
    }
}
