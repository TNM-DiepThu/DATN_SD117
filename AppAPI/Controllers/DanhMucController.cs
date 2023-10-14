using AppData.model;
using Bill.Serviece.Implements;
using Bill.Serviece.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DanhMucController : ControllerBase
    {
        private readonly IDanhMucServiece _danhmucsv;
        public DanhMucController()
        {
            _danhmucsv = new DanhMucServiece();
        }
        [HttpGet("GetAll")]

        public  IEnumerable<DanhMuc> GetAllDanhMuc()
        {
            return _danhmucsv.GetAll();
        }
        [HttpPost("Create")]
        public bool CreateDanhMuc(string name)
        {
            DanhMuc danh = new DanhMuc()
            {
                Id = Guid.NewGuid(),
                TenDanhMuc = name,
                status = 1 ,
            };
            if(_danhmucsv.GetAll().Any(c => c.TenDanhMuc == name))
            {
                return false;
            }
            return _danhmucsv.Add(danh);
        }

        [HttpPut("[action]")]
        public bool UpdateDanhMuc(Guid id , string name , int status)
        {
           DanhMuc danh = _danhmucsv.GetAll().FirstOrDefault(x => x.Id == id);
            if (danh != null)
            {
                danh.TenDanhMuc = name;
                danh.status = status;
            }
            return _danhmucsv.Edit(id, danh);
        }
        [HttpPut("[action]")]
        public bool Delete(Guid id)
        {
            return _danhmucsv.Del(id);
        }
    }
}
