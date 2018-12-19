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
    public class TestCourse
    {
        private IUnitOfWork data = new DAL.Presistence.UnitOfWork() { };        

        [SetUp]
        public void setupTestEnviorment()
        {
            Course testCourse = new Course()
            {
                CourseID = 1,
                Name ="I4SWD",
                SemesterID = 4   
            };
        }

        [Test]
        public async void testGetAllAsync()
        {
            List<Course> resultList = new List<Course>();

            resultList = await data.Course.GetAllAsync();

            Assert.That(resultList.Count, Is.EqualTo(4)); // hvertfald i mit test script
        }

        [Test]
        public async void testGetAsync()
        {
            Course course = new Course();

            course = await data.Course.GetAsync(1);

            Assert.That(course.CourseID, Is.EqualTo(1));
            Assert.That(course.Name, Is.EqualTo("I4SWD"));
            Assert.That(course.SemesterID, Is.EqualTo(4));
        }

        [Test]
        public async void testAddUpdateRemove()
        {
            Course testNewCourse = new Course()
            {
                CourseID = 999,
                Name = "I4DAB",
                SemesterID = 4
            };

            await data.Course.Add(testNewCourse); //Gemmer bruger

            Course resultCourse = await data.Course.GetAsync(999); //henter Quiz med ID 2

            Assert.That(resultCourse.CourseID, Is.EqualTo(999));
            Assert.That(resultCourse.Name, Is.EqualTo("I4DAB"));
            Assert.That(resultCourse.SemesterID, Is.EqualTo(4));
            //update testes her

            Course testChangedCourse = new Course()
            {
                CourseID = 999,
                Name = "I4IKN",
                SemesterID = 5
            };

            await data.Course.Update(999, testChangedCourse); //ændre quizzen 

            Course assertCourse = await data.Course.GetAsync(999); //henter den ændrede Quiz

            Assert.That(assertCourse.CourseID, Is.EqualTo(999));
            Assert.That(assertCourse.Name, Is.EqualTo("I4IKN"));
            Assert.That(assertCourse.SemesterID, Is.EqualTo(5));
            //delete testes her 

            List<Course> resultList = new List<Course>();

            resultList = await data.Course.GetAllAsync();

            int currentSize = resultList.Count;

            data.Course.Remove(999);

            resultList = await data.Course.GetAllAsync();

            Assert.That(resultList.Count, Is.EqualTo(currentSize-1));
        }
    }
}
