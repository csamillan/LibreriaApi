using LibreriaApi.Dtos;
using LibreriaApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaApi.Controllers
{
    [ApiController]
    [Route("api/editorials")]
    public class EditorialController : ControllerBase
    {
        private readonly IEditorialService _editorialService;

        public EditorialController(IEditorialService service)
        {
            _editorialService = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_editorialService.Get());
        }

        [HttpPost]
        public IActionResult Post([FromBody] SaveEditorial dto)
        {
            _editorialService.Save(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] SaveEditorial dto)
        {
            _editorialService.Update(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _editorialService.Delete(id);
            return Ok();
        }
    }
}
