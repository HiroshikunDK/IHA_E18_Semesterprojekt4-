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
    public class TestStudy
    {
        private IUnitOfWork data = new DAL.Presistence.UnitOfWork() { };        

        [SetUp]
        public void setupTestEnviorment()
        {
            Study testCourse = new Study()
            {
                StudyID = 1,
                Name = "IKT Ingeniør",
                FacultyID = 1
            };
        }

        [Test]
        public async void testGetAllAsync()
        {
            List<Study> resultList = new List<Study>();

            resultList = await data.Study.GetAllAsync();

            Assert.That(resultList.Count, Is.EqualTo(3)); // hvertfald i mit test script
        }

        [Test]
        public async void testGetAsync()
        {
            Study study = new Study();

            study = await data.Study.GetAsync(1);

            Assert.That(study.StudyID, Is.EqualTo(1));
            Assert.That(study.Name, Is.EqualTo("IKT Ingeniør"));
            Assert.That(study.FacultyID, Is.EqualTo(1));
        }

        [Test]
        public async void testAddUpdateRemove()
        {
            Study testNewStudy = new Study()
            {
                StudyID = 999,
                Name = "Computer Science cand.",
                FacultyID = 1
            };

            await data.Study.Add(testNewStudy); //Gemmer bruger

            Study resultStudy = await data.Study.GetAsync(999); //henter Quiz med ID 2

            Assert.That(resultStudy.StudyID, Is.EqualTo(999));
            Assert.That(resultStudy.Name, Is.EqualTo("Computer Science cand."));
            Assert.That(resultStudy.FacultyID, Is.EqualTo(1));
            //update testes her

            Study testChangedStudy = new Study()
            {
                StudyID = 999,
                Name = "IKT PHD",
                FacultyID = 2
            };

            await data.Study.Update(999, testChangedStudy); //ændre quizzen 

            Study assertStudy = await data.Study.GetAsync(999); //henter den ændrede Quiz

            Assert.That(assertStudy.StudyID, Is.EqualTo(999));
            Assert.That(assertStudy.Name, Is.EqualTo("IKT PHD"));
            Assert.That(assertStudy.FacultyID, Is.EqualTo(2));
            //delete testes her 

            List<Study> resultList = new List<Study>();

            resultList = await data.Study.GetAllAsync();

            int currentSize = resultList.Count;

            data.Study.Remove(999);

            resultList = await data.Study.GetAllAsync();

            Assert.That(resultList.Count, Is.EqualTo(currentSize-1));
        }
    }
}
