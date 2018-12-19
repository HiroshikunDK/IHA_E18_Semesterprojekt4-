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
            UserType testUT = new UserType()
            {
                UserTypeID = 1,
                Type = "AlmindeligBruger"
            };
        }

        [Test]
        public async void testGetAllAsync()
        {
            List<UserType> resultList = new List<UserType>();

            resultList = await data.UserType.GetAllAsync();

            Assert.That(resultList.Count, Is.EqualTo(3)); // hvertfald i mit test script
        }

        [Test]
        public async void testGetAsync()
        {
            UserType ut = new UserType();

            ut = await data.UserType.GetAsync(1);

            Assert.That(ut.UserTypeID, Is.EqualTo(1));
            Assert.That(ut.Type, Is.EqualTo("AlmindeligBruger"));
        }

        [Test]
        public async void testAddUpdateRemove()
        {
            UserType testNewUT = new UserType()
            {
                UserTypeID = 999,
                Type = "SuperBruger"
            };

            await data.UserType.Add(testNewUT); //Gemmer bruger

            UserType resultUT = await data.UserType.GetAsync(999); //henter Quiz med ID 2

            Assert.That(resultUT.UserTypeID, Is.EqualTo(999));
            Assert.That(resultUT.Type, Is.EqualTo("SuperBruger"));
            //update testes her

            UserType testChangedUT = new UserType()
            {
                UserTypeID = 999,
                Type = "MiddelmådigBruger"
            };

            await data.UserType.Update(999, testChangedUT); //ændre quizzen 

            UserType assertUT = await data.UserType.GetAsync(999); //henter den ændrede Quiz

            Assert.That(assertUT.UserTypeID, Is.EqualTo(999));
            Assert.That(assertUT.Type, Is.EqualTo("SuperBruger"));
            //delete testes her 

            List<UserType> resultList = new List<UserType>();

            resultList = await data.UserType.GetAllAsync();

            int currentSize = resultList.Count;

            data.UserType.Remove(999);

            resultList = await data.UserType.GetAllAsync();

            Assert.That(resultList.Count, Is.EqualTo(currentSize-1));
        }
    }
}
