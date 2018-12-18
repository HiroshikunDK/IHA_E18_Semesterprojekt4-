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
    public class TestSemester
    {
        private IUnitOfWork data = new DAL.Presistence.UnitOfWork() { };        

        [SetUp]
        public void setupTestEnviorment()
        {
            Semester testSemester = new Semester()
            {
              SemesterID = 1,
              SemesterNumber = "1",
              StudyID = 1
            };
        }

        [Test]
        public async void testGetAllAsync()
        {
            List<Semester> resultList = new List<Semester>();

            resultList = await data.Semester.GetAllAsync();

            Assert.That(resultList.Count, Is.EqualTo(17)); // hvertfald i mit test script
        }

        [Test]
        public async void testGetAsync()
        {
            Semester semester = new Semester();

            semester = await data.Semester.GetAsync(1);

            Assert.That(semester.SemesterID, Is.EqualTo(1));
            Assert.That(semester.SemesterNumber, Is.EqualTo("1"));
            Assert.That(semester.StudyID, Is.EqualTo(1));
        }

        [Test]
        public async void testAddUpdateRemove()
        {
            Semester testNewSemester = new Semester()
            {
                SemesterID = 999,
                SemesterNumber = "13",
                StudyID = 1
            };

            await data.Semester.Add(testNewSemester); //Gemmer bruger

            Semester resultsemester = await data.Semester.GetAsync(999); //henter Quiz med ID 2

            Assert.That(resultsemester.SemesterID, Is.EqualTo(999));
            Assert.That(resultsemester.SemesterNumber, Is.EqualTo("13"));
            Assert.That(resultsemester.StudyID, Is.EqualTo(1));
            //update testes her

            Semester testChangedSemester = new Semester()
            {
                SemesterID = 999,
                SemesterNumber = "11",
                StudyID = 2
            };

            await data.Semester.Update(999, testChangedSemester); //ændre quizzen 

            Semester assertSemester = await data.Semester.GetAsync(999); //henter den ændrede Quiz

            Assert.That(resultsemester.SemesterID, Is.EqualTo(999));
            Assert.That(resultsemester.SemesterNumber, Is.EqualTo("11"));
            Assert.That(resultsemester.StudyID, Is.EqualTo(2));
            //delete testes her 

            List<Semester> resultList = new List<Semester>();

            resultList = await data.Semester.GetAllAsync();

            int currentSize = resultList.Count;

            data.Semester.Remove(999);

            resultList = await data.Semester.GetAllAsync();

            Assert.That(resultList.Count, Is.EqualTo(currentSize-1));
        }
    }
}
