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
                    TryMapAndAdd(combinedRecord, this.expectedFieldsPerRecord, result);
                    pending = null;

                    rownumber = SetRowNumberAsId(rownumber, result[rownumber]);
                    continue;
                }

                if (record.Length >= this.expectedFieldsPerRecord)
                {
                    TryMapAndAdd(record, this.expectedFieldsPerRecord, result);
                    rownumber = SetRowNumberAsId(rownumber, result[rownumber]);
                    continue;
                }

                pending = record;
            }

            if (pending != null)
            {
                TryMapAndAdd(pending, this.expectedFieldsPerRecord, result);
                rownumber = SetRowNumberAsId(rownumber, result[rownumber]);
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

        private static void TryMapAndAdd(string[] fields, int expectedFields, List<Person> output)
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

                var p = new Person
                {
                    LastName = fields.Length > 0 ? fields[0] : string.Empty,
                    Name = fields.Length > 1 ? fields[1] : string.Empty,
                    Color = int.TryParse(fields[3], NumberStyles.Integer, CultureInfo.InvariantCulture, out var colorId) ? colorId : 0,
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
            string[] splittedZipCodeAndCity = [string.Empty, string.Empty];
            var match = Regex.Match(zipCodeAndCity, @"^(?<zipCode>\d{5})\s+(?<city>.+)$");

            if (match.Success)
            {
                splittedZipCodeAndCity = new string[2];
                _ = splittedZipCodeAndCity.Append(match.Groups["zipCode"].Value);
                _ = splittedZipCodeAndCity.Append(match.Groups["city"].Value);
            }

            return splittedZipCodeAndCity;
        }

        private static int SetRowNumberAsId(int rownumber, Person person)
        {
            person.Id = rownumber;
            return rownumber++;
        }
    }

}
