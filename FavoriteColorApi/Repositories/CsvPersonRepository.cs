using FavoriteColorApi.Models;
using FavoriteColorApi.Services.DataLoader;

namespace FavoriteColorApi.Repositories
{
    public class CsvPersonRepository(CsvDataLoader loader) : IPersonRepository
    {
        private readonly CsvDataLoader _loader = loader;

        public IEnumerable<Person> GetAllPersons() => this._loader.LoadPersons();
    }
}
