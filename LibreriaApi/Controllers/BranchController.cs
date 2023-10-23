using LibreriaApi.Dtos;
using LibreriaApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaApi.Controllers
{
    [ApiController]
    [Route("api/branchs")]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _BranchService;

        public BranchController(IBranchService service)
        {
            _BranchService = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_BranchService.Get());
        }

        [HttpPost]
        public IActionResult Post([FromBody] SaveBranch dto)
        {
            _BranchService.Save(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] SaveBranch dto)
        {
            _BranchService.Update(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _BranchService.Delete(id);
            return Ok();
        }
    }
}
