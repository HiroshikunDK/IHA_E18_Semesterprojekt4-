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
    public class TestFaculty
    {
        private IUnitOfWork data = new DAL.Presistence.UnitOfWork() { };        

        [SetUp]
        public void setupTestEnviorment()
        {
            Faculty testFaculty = new Faculty()
            {
                FacultyID = 1,
                Name = "Science And Technology"
            };
        }

        [Test]
        public async void testGetAllAsync()
        {
            List<Faculty> resultList = new List<Faculty>();

            resultList = await data.Faculty.GetAllAsync();

            Assert.That(resultList.Count, Is.EqualTo(4)); // hvertfald i mit test script
        }

        [Test]
        public async void testGetAsync()
        {
            Faculty faculty = new Faculty();

            faculty = await data.Faculty.GetAsync(1);

            Assert.That(faculty.FacultyID, Is.EqualTo(1));
            Assert.That(faculty.Name, Is.EqualTo("Science And Technology"));

        }

        [Test]
        public async void testAddUpdateRemove()
        {
            Faculty testNewFaculty = new Faculty()
            {
                FacultyID = 999,
                Name = "Gøjl og ballade - Humaniora"
            };

            await data.Faculty.Add(testNewFaculty); //Gemmer bruger

            Faculty resultfaculty = await data.Faculty.GetAsync(999); //henter Quiz med ID 2

            Assert.That(resultfaculty.FacultyID, Is.EqualTo(999));
            Assert.That(resultfaculty.Name, Is.EqualTo("Gøjl og ballade - Humaniora"));
            //update testes her

            Faculty testChangedFaculty = new Faculty()
            {
                FacultyID = 999,
                Name = "Seriøs Humaniora... *pffft"
            };

            await data.Faculty.Update(999, testChangedFaculty); //ændre quizzen 

            Faculty assertFaculty = await data.Faculty.GetAsync(999); //henter den ændrede Quiz

            Assert.That(assertFaculty.FacultyID, Is.EqualTo(999));
            Assert.That(assertFaculty.Name, Is.EqualTo("Seriøs Humaniora... *pffft"));

            //delete testes her 

            List<Faculty> resultList = new List<Faculty>();

            resultList = await data.Faculty.GetAllAsync();

            int currentSize = resultList.Count;

            data.Faculty.Remove(999);

            resultList = await data.Faculty.GetAllAsync();

            Assert.That(resultList.Count, Is.EqualTo(currentSize-1));
        }
    }
}
