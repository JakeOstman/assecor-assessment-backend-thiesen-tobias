using CsvHelper;
using FavoriteColorApi.Models;
using FavoriteColorApi.Services.DataLoader;

namespace FavoriteColorApi.Services
{
    public class PersonService
    {
        private readonly List<Person> _persons;

        public PersonService(CsvDataLoader loader) // TODO: CsvDataReader nutzen?
        {
            this._persons = loader.LoadPersons();
        }

        public List<Person> GetAll() => this._persons;

        public Person? GetById(int id) => this._persons.FirstOrDefault(p => p.Id == id);

        public void Add(Person person) => this._persons.Add(person);

        public void Update(Person person)
        {
            var index = this._persons.FindIndex(p => p.Id == person.Id);
            if (index != -1)
            {
                this._persons[index] = person;
            }
        }
    }

}
