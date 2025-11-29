using System;
using System.Collections.Generic;
using System.Linq;
using FavoriteColorApi.Services;
using FavoriteColorApi.Services.DataLoader;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FavoriteColorApiTests.Services
{
    [TestClass]
    public class PersonServiceTests
    {
        [TestMethod]
        public void GetNames_ShouldReturnListOfNames()
        {
            var service = new PersonService(new CsvDataLoader("Data/persons.csv"));
            var result = service.GetAll();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<string>));
            Assert.HasCount(10, result);
            CollectionAssert.Contains(result, "Petersen");
        }
    }
}
