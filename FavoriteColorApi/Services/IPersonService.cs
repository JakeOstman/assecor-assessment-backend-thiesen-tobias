using FavoriteColorApi.Models;

namespace FavoriteColorApi.Services
{
    public interface IPersonService
    {
        IEnumerable<Person> GetAll();
        Person? GetById(int id);
        void Add(Person person);
        void Update(Person person);
        public IEnumerable<Person> GetPersonsByColor(string colorName);
    }
}
