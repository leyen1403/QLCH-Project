using Microsoft.AspNetCore.Mvc;
using QLCH.Application.Interfaces.IService;
using Microsoft.Extensions.Logging;
using QLCH.Application.DTOs;

namespace QLCH.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChucVuController : ControllerBase
    {
        private readonly IChucVuService _chucVuService;
        private readonly ILogger<ChucVuController> _logger;

        public ChucVuController(IChucVuService chucVuService, ILogger<ChucVuController> logger)
        {
            _chucVuService = chucVuService;
            _logger = logger;
        }

        /// <summary>Lấy tất cả chức vụ</summary>
        [HttpGet]
        public ActionResult<List<ChucVuDto>> GetAll()
        {
            try
            {
                var data = _chucVuService.GetAll();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách chức vụ.");
                return StatusCode(500, "Đã xảy ra lỗi.");
            }
        }

        /// <summary>Lấy chức vụ theo ID</summary>
        [HttpGet("{id}")]
        public ActionResult<ChucVuDto> GetById(int id)
        {
            try
            {
                var data = _chucVuService.GetById(id);
                if (data == null)
                    return NotFound($"Không tìm thấy chức vụ với ID = {id}");
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi lấy chức vụ ID = {id}");
                return StatusCode(500, "Đã xảy ra lỗi.");
            }
        }

        /// <summary>Thêm chức vụ mới</summary>
        [HttpPost]
        public ActionResult Create([FromBody] ChucVuDto dto)
        {
            try
            {
                _chucVuService.Create(dto);
                return Ok(new { message = "Thêm thành công." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi thêm chức vụ.");
                return StatusCode(500, "Đã xảy ra lỗi khi thêm.");
            }
        }

        /// <summary>Cập nhật chức vụ</summary>
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] ChucVuDto dto)
        {
            try
            {
                if (dto.MaChucVu != null && dto.MaChucVu != id)
                    return BadRequest("ID không khớp.");

                dto.MaChucVu = id;
                _chucVuService.Update(dto);
                return Ok(new { message = "Cập nhật thành công." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi cập nhật chức vụ ID = {id}");
                return StatusCode(500, "Đã xảy ra lỗi khi cập nhật.");
            }
        }

        /// <summary>Xóa chức vụ</summary>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _chucVuService.Delete(id);
                return Ok(new { message = "Xóa thành công." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi xóa chức vụ ID = {id}");
                return StatusCode(500, "Đã xảy ra lỗi khi xóa.");
            }
        }
    }
}
