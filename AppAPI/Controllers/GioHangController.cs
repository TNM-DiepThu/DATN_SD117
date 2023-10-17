using AppData.model;
using AppData.Serviece.Implements;
using AppData.Serviece.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GioHangController : ControllerBase
    {
        private readonly IGioHangService _GH;
        private readonly INguoiDungServiece _nguoidungservice;
        public GioHangController()
        {
            _GH = new GioHangService();
            _nguoidungservice = new NguoiDungService();
        }
        [HttpGet("GetAll")]

        public IEnumerable<GioHang> GetAllAsync()
        {
            return _GH.GetAll();
        }
        [HttpPost("Create")]
        public bool Create(string GhiChu)
        {
            GioHang GH = new GioHang()
            {
                Id = Guid.NewGuid(),
                GhiChu = GhiChu,
            };
            return _GH.Add(GH);
        }
        [HttpDelete("Delete/{Id}")]

        public bool DeleteAsync(Guid Id)
        {
            var result = _GH.Del(Id);
            return result;
        }

        [HttpPut("Update/{id}")]

        public bool UpdateAsync(Guid id, [FromBody] GioHang p)
        {
            var result = _GH.Edit(id, p);
            return result;
        }
    }
}
