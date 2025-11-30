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
            this._context = context;
        }

        public IEnumerable<Person> GetAllPersons() => this._context.Person.ToList();

        public Person? GetById(int id) => this._context.Person.FirstOrDefault(p => p.Id == id);

        public void Add(Person person)
        {
            this._context.Person.Add(person);
            this._context.SaveChanges();
        }

        public void Update(Person person)
        {
            this._context.Person.Update(person);
            this._context.SaveChanges();
        }

        public IEnumerable<Person> GetPersonsByColor(string color)
            => this._context.Person.Where(p => p.Color == color).ToList();
    }
}
