using FavoriteColorApi.Models;
using FavoriteColorApi.Repositories;

namespace FavoriteColorApi.Services
{
    public class PersonService(IPersonRepository personRepo) : IPersonService
    {
        private readonly List<Person> _persons = personRepo.LoadPersons();

        public IEnumerable<Person> GetAll() => this._persons;

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

        public IEnumerable<Person> GetPersonsByColor(string colorName)
        {
            return this._persons.Where(p => p.Color == colorName).ToList();
        }
    }

}
