using System.Drawing;
using FavoriteColorApi.Models;
using FavoriteColorApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FavoriteColorApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _service;

        public PersonController(IPersonService service)
        {
            this._service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Person>> GetAllPersons()
        {
            var persons = this._service.GetAll();
            return persons is null || !persons.Any() ? this.NotFound() : this.Ok(persons);
        }

        [HttpGet("{id}")]
        public ActionResult<Person> GetPersonById(int id)
        {
            var person = this._service.GetById(id);
            return person is null ? this.NotFound() : this.Ok(person);
        }

        [HttpGet("color/{color}")]
        public ActionResult<IEnumerable<Person>> GetPersonsByColor(string color)
        {
            var persons = this._service.GetPersonsByColor(color);
            return persons is null || !persons.Any() ? this.NotFound() : this.Ok(persons);
        }
    }

}
