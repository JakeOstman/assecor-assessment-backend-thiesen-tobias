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
        public IEnumerable<Person> Get() => this._service.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Person> Get(int id)
        {
            var person = this._service.GetById(id);
            return person is null ? this.NotFound() : this.Ok(person);
        }
    }

}
