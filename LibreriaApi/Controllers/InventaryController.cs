using LibreriaApi.Dtos;
using LibreriaApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaApi.Controllers
{
    [ApiController]
    [Route("api/inventaries")]
    public class InventaryController : ControllerBase
    {
        private readonly IInventaryService _InventaryService;

        public InventaryController(IInventaryService service)
        {
            _InventaryService = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_InventaryService.Get());
        }

        [HttpPost]
        public IActionResult Post([FromBody] SaveInventary dto)
        {
            _InventaryService.Save(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] SaveInventary dto)
        {
            _InventaryService.Update(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _InventaryService.Delete(id);
            return Ok();
        }
    }
}
