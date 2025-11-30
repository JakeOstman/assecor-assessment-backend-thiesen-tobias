using FavoriteColorApi.Data;
using FavoriteColorApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FavoriteColorApi.Repositories
{
    public class SqlPersonRepository : IPersonRepository
    {
        private readonly PersonDbContext _context;

        public SqlPersonRepository(PersonDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Person> GetAllPersons() => _context.Person.ToList();

        public Person? GetById(int id) => _context.Person.FirstOrDefault(p => p.Id == id);

        public void Add(Person person)
        {
            _context.Person.Add(person);
            _context.SaveChanges();
        }

        public void Update(Person person)
        {
            _context.Person.Update(person);
            _context.SaveChanges();
        }

        public IEnumerable<Person> GetPersonsByColor(string color)
            => _context.Person.Where(p => p.Color == color).ToList();
    }
}
