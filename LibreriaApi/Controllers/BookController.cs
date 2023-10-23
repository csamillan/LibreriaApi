using LibreriaApi.Dtos;
using LibreriaApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaApi.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService service)
        {
            _bookService = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookService.Get());
        }

        [HttpPost]
        public IActionResult Post([FromBody] SaveBook dto)
        {
            _bookService.Save(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] SaveBook dto)
        {
            _bookService.Update(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _bookService.Delete(id);
            return Ok();
        }
    }
}
