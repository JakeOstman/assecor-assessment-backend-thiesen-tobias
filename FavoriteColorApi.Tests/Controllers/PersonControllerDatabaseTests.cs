using System;
using System.Collections.Generic;
using System.Linq;
using FavoriteColorApi.Controllers;
using FavoriteColorApi.Data;
using FavoriteColorApi.Models;
using FavoriteColorApi.Repositories;
using FavoriteColorApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FavoriteColorApi.Tests.Controllers
{
    [TestClass]
    public class PersonControllerDatabaseTests
    {
        private readonly PersonController _personController;

        public TestContext TestContext { get; set; } = null!;

        public PersonControllerDatabaseTests()
        {
            var options = new DbContextOptionsBuilder<PersonDbContext>()
                .UseSqlServer("Server=THYRUS\\SQLEXPRESS;Database=FavoriteColorDb;Trusted_Connection=True;TrustServerCertificate=True;")
                .Options;

            var context = new PersonDbContext(options);

            var repository = new SqlPersonRepository(context);
            var service = new PersonService(repository);

            this._personController = new PersonController(service);
        }

        [TestMethod]
        public void GetAllPersons_ReturnsAllPersons()
        {
            var result = this._personController.GetAllPersons();
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var okObjectResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);

            var persons = okObjectResult.Value as IEnumerable<Person>;
            Assert.IsNotNull(persons);
            Assert.IsInstanceOfType(persons, typeof(IEnumerable<Person>));
            Assert.IsTrue(persons.Any());

            foreach (var person in persons)
            {
                Assert.IsNotNull(person);
                Assert.IsFalse(string.IsNullOrEmpty(person.Name));
            }
        }

        [TestMethod]
        public void GetPersonById_ReturnsPerson_WhenFound()
        {
            var result = this._personController.GetPersonById(1);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var okObjectResult = result.Result as OkObjectResult;
            var person = okObjectResult?.Value as Person;

            Assert.IsNotNull(person);
            Assert.AreEqual(1, person.Id);
        }

        [TestMethod]
        public void GetPersonById_ReturnsNotFound_WhenMissing()
        {
            var result = this._personController.GetPersonById(-1);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetPersonByColor_ReturnsPerson_WhenFound()
        {
            string searchedColor = "grün";
            var result = this._personController.GetPersonsByColor(searchedColor);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okObjectResult = result.Result as OkObjectResult;
            var persons = okObjectResult?.Value as IEnumerable<Person>;

            Assert.IsNotNull(persons);
            Assert.IsTrue(persons.All(p => p.Color == searchedColor));
        }

        [TestMethod]
        public void GetPersonByColor_ReturnsNotFound_WhenMissing()
        {
            var result = this._personController.GetPersonsByColor("nichtvorhanden");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }
    }
}
