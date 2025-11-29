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
using Moq;
using static FavoriteColorApi.Services.ColorNameProvider;

namespace FavoriteColorApi.Tests.Controllers
{
    [TestClass]
    public class PersonControllerTests
    {
        private readonly PersonService _personService = new PersonService(
        new CsvDataLoader("..\\..\\..\\..\\FavoriteColorApi\\Data\\sample-input.csv"));
        private readonly Mock<IPersonService> mockService = new Mock<IPersonService>();

        public TestContext TestContext { get; set; }

        [TestMethod]
        public void GetAllPersons_ReturnsAllPersons()
        {
            this.mockService.Setup(s => s.GetAll()).Returns(new List<Person>
            {
                new Person { Id = 1, Name = "Max" },
                new Person { Id = 2, Name = "Anna" }
            });

            var controller = new PersonController(mockService.Object);
            var persons = controller.GetAllPersons();

            Assert.IsNotNull(persons);
            Assert.AreEqual(2, persons.Count());

            foreach (var person in persons)
            {
                Assert.IsNotNull(person);
                Assert.IsFalse(string.IsNullOrEmpty(person.Name));
            }
        }

        [TestMethod]
        public void GetPersonById_ReturnsPerson_WhenFound()
        {
            this.mockService.Setup(s => s.GetById(1)).Returns(new Person { Id = 1, Name = "Hans" });

            var controller = new PersonController(this.mockService.Object);

            var result = controller.GetPersonById(1);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<OkObjectResult>(result.Result);

            var okObjectResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);

            var person = okObjectResult.Value as Person;
            Assert.IsNotNull(person);
            Assert.IsInstanceOfType<Person>(person);

            Assert.AreEqual(1, person.Id);
            Assert.AreEqual("Hans", person.Name);
        }

        [TestMethod]
        public void GetPersonById_ReturnsNotFound_WhenMissing()
        {
            this.mockService.Setup(s => s.GetById(99)).Returns((Person?)null);

            var controller = new PersonController(this.mockService.Object);
            var result = controller.GetPersonById(99);

            Assert.IsInstanceOfType<NotFoundResult>(result.Result);
        }
    }
}
