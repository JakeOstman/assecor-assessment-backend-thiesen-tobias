using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FavoriteColorApi.Models;
using FavoriteColorApi.Services;
using FavoriteColorApi.Services.DataLoader;
using static FavoriteColorApi.Services.ColorNameProvider;

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
            Assert.AreEqual(10, persons.Count(), "The list should contain exactly 10 items.");

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
        public void GetPersonsByRandomColor_ReturnsExpectedObject()
        {
            var colorNames = Enum.GetValues(typeof(EColorName));
            var randomColor = colorNames.GetValue(Random.Shared.Next(colorNames.Length))!;
            Assert.IsNotNull(randomColor, "The returned object must not be null.");

            EColorName colorEnum = (EColorName)randomColor;
            string randomColorName = colorEnum.ToString();

            var persons = this._personService.GetPersonsByColor(randomColorName);
            
            if (randomColorName.Equals("weiß"))
            {
                Assert.IsTrue(condition: !persons.Any(), "At least one person seems to have white as their favorite color.");
            }
            else
            {
                Assert.IsTrue(condition: persons.Any(), "At least one person should be assigned to the color.");
            }

            foreach (Person person in  persons)
            {
                this.WritePersonToConsole(person);
            }
        }

        [TestMethod]
        public void GetPersonsByColor_ReturnsExpectedObject()
        {
            int colorId = 2;
            bool isColorNameDefined = Enum.IsDefined(typeof(EColorName), colorId);
            Assert.IsTrue(isColorNameDefined, "Der Id-Wert sollte einem gültigen Enum-Wert entsprechen.");

            var colorEnum = (EColorName)colorId;
            Assert.AreEqual(EColorName.grün, colorEnum);

            string colorName = colorEnum.ToString();
            Assert.AreEqual("grün", colorName);

            var persons = this._personService.GetPersonsByColor(colorName);
            Assert.AreEqual(3, persons.Count(), "The list should contain exactly 3 items.");

            foreach (Person person in persons)
            {
                this.WritePersonToConsole(person);
            }
        }

        [TestMethod]
        public void GetPersonsByColorIdWhite_ReturnsExpectedObject()
        {
            string color = "weiß";
            var persons = this._personService.GetPersonsByColor(color);

            Assert.AreEqual(0, persons.Count(), "The list should contain exactly 0 items.");
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
