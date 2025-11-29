using FavoriteColorApi.Models;
using FavoriteColorApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FavoriteColorApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly PersonService _service;

        public PersonsController(PersonService service)
        {
            this._service = service;
        }

        [HttpGet]
        public IEnumerable<Person> GetAllPersons() => this._service.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Person> GetPersonById(int id)
        {
            var person = this._service.GetById(id);
            return person is null ? this.NotFound() : this.Ok(person);
        }

        [HttpGet("color/{color}")]
        public ActionResult<Person> GetPersonsByColorId(int color)
        {
            var persons = this._service.GetPersonsByColor(color);
            return persons is null || persons.Count == 0 ? this.NotFound() : this.Ok(persons);
        }
    }

}
