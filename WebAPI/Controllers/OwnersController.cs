using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private IOwnerService _ownerService;

        public OwnersController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }
        //http://localhost:4000/api/owners/getall
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _ownerService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);

        }

        [HttpPost("add")]
        public IActionResult Add(Owner owner)
        {
            var result = _ownerService.Add(owner);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
    }
}