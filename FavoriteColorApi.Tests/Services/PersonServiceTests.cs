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
        [TestMethod]
        public void GetPersons_ReturnsExpectedList()
        {
            var service = new PersonService(
                new CsvDataLoader("m:\\source\\Angular ASP\\Assecor\\assecor-assessment-backend\\FavoriteColorApi\\Data\\sample-input.csv"));

            var result = service.GetAll();

            Assert.IsNotNull(result, "The returned list must not be null.");
            Assert.AreEqual(3, result.Count, "The list should contain exactly 10 items.");
        }
    }
}
