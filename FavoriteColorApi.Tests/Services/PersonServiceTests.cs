using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FavoriteColorApi.Services;
using FavoriteColorApi.Services.DataLoader;
using FavoriteColorApi.Models;

namespace FavoriteColorApi.Tests.Services
{
    [TestClass]
    public class PersonServiceTests
    {
        private readonly PersonService _personService = new PersonService(
                new CsvDataLoader("..\\..\\..\\..\\FavoriteColorApi\\Data\\sample-input.csv"));

        public TestContext TestContext { get; set; }

        [TestMethod]
        public void GetPersons_ReturnsExpectedList()
        {
            var persons = this._personService.GetAll();

            Assert.IsNotNull(persons, "The returned list must not be null.");
            Assert.AreEqual(10, persons.Count, "The list should contain exactly 10 items.");

            foreach (var person in persons)
            {
                this.WritePersonToConsole(person);
            }
        }

        [TestMethod]
        public void GetPersonById_ReturnsExpectedObject()
        {
            var numbers = Enumerable.Range(1, 9).OrderBy(_ => Random.Shared.Next()).ToList();

            foreach (int id in numbers)
            {
                var person = this._personService.GetById(id);

                Assert.IsNotNull(person, $"The returned object with id {id} must not be null.");

                this.WritePersonToConsole(person);
            }
        }

        [TestMethod]
        public void GetPersonsByRandomColorId_ReturnsExpectedObject()
        {           
            int colorId = Random.Shared.Next(1, 8);
            var persons = this._personService.GetPersonsByColor(colorId);

            foreach (Person person in  persons)
            {
                Assert.IsNotNull(person.Color, $"The returned subobject must not be null.");

                this.WritePersonToConsole(person);
            }
        }

        [TestMethod]
        public void GetPersonsByColorIdWhite_ReturnsExpectedObject()
        {
            int colorId = 7;
            var persons = this._personService.GetPersonsByColor(colorId);

            Assert.AreEqual(0, persons.Count, "The list should contain exactly 0 items.");
        }

        private void WritePersonToConsole(Person person)
        {
            this.TestContext.WriteLine("Id: " + person.Id.ToString());
            this.TestContext.WriteLine("LastName: " + person.LastName);
            this.TestContext.WriteLine("Name: " + person.Name);
            this.TestContext.WriteLine("ZipCode: " + person.ZipCode);
            this.TestContext.WriteLine("City: " + person.City);
            this.TestContext.WriteLine("Color: " + person.Color?.ToString());
            this.TestContext.WriteLine(string.Empty);
        }
    }
}
