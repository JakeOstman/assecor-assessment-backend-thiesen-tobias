using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FavoriteColorApi.Services;
using FavoriteColorApi.Services.DataLoader;

namespace FavoriteColorApi.Tests.Services
{
    [TestClass]
    public class PersonServiceTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void GetPersons_ReturnsExpectedList()
        {
            var service = new PersonService(
                new CsvDataLoader("..\\..\\..\\..\\FavoriteColorApi\\Data\\sample-input.csv"));

            var result = service.GetAll();

            Assert.IsNotNull(result, "The returned list must not be null.");
            Assert.AreEqual(10, result.Count, "The list should contain exactly 10 items.");

            foreach (var person in result)
            {
                this.TestContext.WriteLine("Id = " + person.Id.ToString());
                this.TestContext.WriteLine("LastName = " + person.LastName);
                this.TestContext.WriteLine("Name = " + person.Name);
                this.TestContext.WriteLine("ZipCode = " + person.ZipCode);
                this.TestContext.WriteLine("City = " + person.City);
                this.TestContext.WriteLine("ColorId = " + person.Color.ToString());
                this.TestContext.WriteLine(string.Empty);
            }
        }
    }
}
