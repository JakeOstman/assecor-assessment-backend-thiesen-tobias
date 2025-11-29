namespace FavoriteColorApi.Services.DataLoader
{
    using System.Globalization;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using CsvHelper;
    using CsvHelper.Configuration;
    using FavoriteColorApi.Models;

    public class CsvDataLoader
    {
        private readonly string _filePath;
        private readonly int expectedFieldsPerRecord;

        public CsvDataLoader(string filePath, int expectedFieldsPerRecord = 4)
        {
            this._filePath = filePath;
            this.expectedFieldsPerRecord = expectedFieldsPerRecord;
        }

        public List<Person> LoadPersons()
        {
            var result = new List<Person>();

            using var reader = new StreamReader(this._filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };

            using var parser = new CsvParser(reader, config);
            string[]? pending = null;
            int rownumber = 0;

            while (parser.Read())
            {
                var record = parser.Record;

                if (record == null)
                {
                    break;
                }

                if (pending != null)
                {
                    var combinedRecord = CombineRows(pending, record);
                    TryMapAndAdd(combinedRecord, this.expectedFieldsPerRecord, result, rownumber);
                    pending = null;

                    rownumber++;
                    continue;
                }

                if (record.Length >= this.expectedFieldsPerRecord)
                {
                    TryMapAndAdd(record, this.expectedFieldsPerRecord, result, rownumber);
                    rownumber++;
                    continue;
                }

                pending = record;
            }

            if (pending != null)
            {
                TryMapAndAdd(pending, this.expectedFieldsPerRecord, result, rownumber);
                rownumber++;
            }

            return result;
        }

        private static string[] CombineRows(string[] a, string[] b)
        {
            var list = new List<string>(a.Length + b.Length);
            list.AddRange(a);
            list.AddRange(b);
            return list.ToArray();
        }

        private static void TryMapAndAdd(string[] fields, int expectedFields, List<Person> output, int personId)
        {
            try
            {
                if (fields.Length < expectedFields)
                {
                    var tmp = new string[expectedFields];
                    Array.Copy(fields, tmp, fields.Length);
                    for (int i = fields.Length; i < expectedFields; i++)
                    {
                        tmp[i] = string.Empty;
                    }

                    fields = tmp;
                }

                ColorNameProvider colorProvider = new ColorNameProvider();
                int colorId = int.TryParse(fields[3], NumberStyles.Integer, CultureInfo.InvariantCulture, out colorId) ? colorId : 0;
                var p = new Person
                {
                    Id = personId,
                    LastName = fields.Length > 0 ? fields[0] : string.Empty,
                    Name = fields.Length > 1 ? fields[1].TrimStart() : string.Empty,
                    Color = colorProvider.GetColorName(colorId),
                };

                if (fields.Length > 2)
                {
                    string[] zipCodeAndCity = new string[2];
                    zipCodeAndCity = SplitZipCodeAndCity(fields[2]);
                    p.ZipCode = zipCodeAndCity[0];
                    p.City = zipCodeAndCity[1];
                }

                output.Add(p);
            }
            catch (Exception ex)
            {
                // TODO handle exception
            }
        }

        private static string[] SplitZipCodeAndCity(string zipCodeAndCity)
        {
            string[] splittedZipCodeAndCity = new string[2];
            var match = Regex.Match(zipCodeAndCity.TrimStart(), @"^(?<zipCode>\d{5})\s+(?<city>.+)$");

            if (match.Success)
            {
                splittedZipCodeAndCity[0] = match.Groups["zipCode"].Value;
                splittedZipCodeAndCity[1] = match.Groups["city"].Value;
            }

            return splittedZipCodeAndCity;
        }
    }

}
