using FavoriteColorApi.Models;

namespace FavoriteColorApi.Repositories
{
    public interface IPersonRepository
    {
        List<Person> LoadPersons();
    }
}
