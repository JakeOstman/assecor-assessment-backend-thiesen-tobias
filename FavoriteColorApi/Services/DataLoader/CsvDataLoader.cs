namespace FavoriteColorApi.Services.DataLoader
{
    using System.Globalization;
    using System.IO;
    using CsvHelper;
    using CsvHelper.Configuration;
    using FavoriteColorApi.Models;

    public class CsvDataLoader
    {
        private readonly string _filePath;

        public CsvDataLoader(string filePath)
        {
            this._filePath = filePath;
        }

        public List<Person> LoadPersons()
        {
            using var reader = new StreamReader(this._filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecords<Person>().ToList();
            return records;
        }
    }

}
