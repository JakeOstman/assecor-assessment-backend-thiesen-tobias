using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FavoriteColorApi.Controllers;
using FavoriteColorApi.Models;
using FavoriteColorApi.Services;
using FavoriteColorApi.Services.DataLoader;
using Microsoft.AspNetCore.Mvc;

namespace FavoriteColorApi.Tests.Controllers
{
    [TestClass]
    public class PersonControllerTests
    {
        private readonly PersonController _personController = new PersonController(new PersonService(
        new CsvDataLoader("..\\..\\..\\..\\FavoriteColorApi\\Data\\sample-input.csv")));

        public TestContext TestContext { get; set; }

        [TestMethod]
        public void GetAllPersons_ReturnsAllPersons()
        {
            var result = this._personController.GetAllPersons();
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<OkObjectResult>(result.Result);

            var okObjectResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);

            var persons = okObjectResult.Value as IEnumerable<Person>;
            Assert.IsNotNull(persons);
            Assert.IsInstanceOfType<IEnumerable<Person>>(persons);
            Assert.AreEqual(10, persons.Count());

            foreach (var person in persons)
            {
                Assert.IsNotNull(person);
                Assert.IsFalse(string.IsNullOrEmpty(person.Name));

                if (person.Id == 0)
                {
                    Assert.AreEqual("Hans", person.Name);
                }

                if (person.Id == 1)
                {
                    Assert.AreEqual("Peter", person.Name);
                }

                if (person.Id == 9)
                {
                    Assert.AreEqual("Klaus", person.Name);
                }
            }
        }

        [TestMethod]
        public void GetPersonById_ReturnsPerson_WhenFound()
        {
            var result = this._personController.GetPersonById(1);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<OkObjectResult>(result.Result);

            var okObjectResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);

            var person = okObjectResult.Value as Person;
            Assert.IsNotNull(person);
            Assert.IsInstanceOfType<Person>(person);

            Assert.AreEqual(1, person.Id);
            Assert.AreEqual("Peter", person.Name);
        }

        [TestMethod]
        public void GetPersonById_ReturnsNotFound_WhenMissing()
        {
            var result = this._personController.GetPersonById(99);

            Assert.IsInstanceOfType<NotFoundResult>(result.Result);
        }

        [TestMethod]
        public void GetPersonByColor_ReturnsPerson_WhenFound()
        {
            string searchedColor = "grün";

            var result = this._personController.GetPersonsByColor(searchedColor);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<OkObjectResult>(result.Result);

            var okObjectResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);

            var persons = okObjectResult.Value as IEnumerable<Person>;
            Assert.IsNotNull(persons);
            Assert.IsInstanceOfType<IEnumerable<Person>>(persons);
            Assert.AreEqual(3, persons.Count());

            foreach (var person in persons)
            {
                Assert.IsNotNull(person);
                Assert.IsFalse(string.IsNullOrEmpty(person.Name));
                Assert.AreEqual(searchedColor, person.Color);
            }

            var personWithId2 = persons.Where(p => p.Id == 2);
            Assert.IsNotNull(personWithId2);
            
            var personWithId7 = persons.Where(p => p.Id == 7);
            Assert.IsNotNull(personWithId7);

            var personWithId10 = persons.Where(p => p.Id == 10);
            Assert.IsNotNull(personWithId10);
        }

        [TestMethod]
        public void GetPersonByColor_ReturnsNotFound_WhenMissing()
        {
            var result = this._personController.GetPersonsByColor("weiß");

            Assert.IsInstanceOfType<NotFoundResult>(result.Result);
        }
    }
}
