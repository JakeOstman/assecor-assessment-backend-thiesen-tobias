using FavoriteColorApi.Models;

namespace FavoriteColorApi.Repositories
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetAllPersons();
    }
}
