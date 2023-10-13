using AppData.model;
using Bill.Serviece.Implements;
using Bill.Serviece.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MauSacController : ControllerBase
    {
        private readonly IMauSacServiece _mausacsv;
        public MauSacController()
        {
            _mausacsv = new MauSacServiece();
        }

        [HttpGet("GetAll")]
        public IEnumerable<MauSac> GetAllMauSac()
        {
            return _mausacsv.GetAll();
        }

        [HttpPost("Create")]
        public bool CreaterAsync(string name)
        {
            MauSac mau = new MauSac()
            {
                Id = Guid.NewGuid(),
                TenMauSac = name,
                status = 1,
            };
            return _mausacsv.Add(mau);
        }

        [HttpPut("Deltete/{id}")]
        public bool DeleteAsync(Guid Id)
        {
            return  _mausacsv.Del(Id);
            
        }
    }
}
