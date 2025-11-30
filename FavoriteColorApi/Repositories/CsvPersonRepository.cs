using FavoriteColorApi.Models;
using FavoriteColorApi.Services.DataLoader;

namespace FavoriteColorApi.Repositories
{
    public class CsvPersonRepository(CsvDataLoader loader) : IPersonRepository
    {
        private readonly CsvDataLoader _loader = loader;

        public List<Person> LoadPersons() => this._loader.LoadPersons();
    }
}
