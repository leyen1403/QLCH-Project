using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLCH.Application.Interfaces.IService;

namespace QLCH.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChucVuController : ControllerBase
    {
        private readonly IChucVuService _chucVuService;
        public ChucVuController(IChucVuService chucVuService)
        {
            _chucVuService = chucVuService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _chucVuService.GetAll();
            return Ok(result);
        }
    }
}
