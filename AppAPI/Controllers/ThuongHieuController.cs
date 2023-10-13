using AppData.model;
using Bill.Serviece.Implements;
using Bill.Serviece.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThuongHieuController : ControllerBase
    {
        private readonly IThuongHieuServiece _thuonghieusv;
        public ThuongHieuController()
        {
            _thuonghieusv = new ThuongHieuServiece();
        }
        [HttpGet("GetAll")]

        public IEnumerable<ThuongHieu> GetAllThuongHieu()
        {
            return _thuonghieusv.GetAll();
        }
        [HttpPost("Create")]
        public bool CreateAsync(string name)
        {
            ThuongHieu th = new ThuongHieu()
            {
                Id = Guid.NewGuid(),
                TenThuongHieu = name , 
                Status = 1 ,
            };
            return _thuonghieusv.Add(th);
        }
        [HttpPut("Delete/{Id}")]

        public bool DeleteThuongHieu(Guid Id)
        {
            return _thuonghieusv.Del(Id);
        }
    }
}
