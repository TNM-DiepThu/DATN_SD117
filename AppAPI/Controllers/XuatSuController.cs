using AppData.model;
using Bill.Serviece.Implements;
using Bill.Serviece.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XuatSuController : ControllerBase
    {
        private readonly IXuatSuServiece _xuatsusv;
        public XuatSuController()
        {
            _xuatsusv = new XuatSuServiece();
        }
        [HttpGet("GetAll")]

        public IEnumerable<XuatSu> GetAllAsync()
        {
            return _xuatsusv.GetAll();
        }
        [HttpPost("Create")]
        public bool CreateAsync(string name)
        {
           XuatSu xx = new XuatSu()
           {
               Id = Guid.NewGuid(),
               TenXuatSu = name,
               Status = 1 ,
           };
            return _xuatsusv.Add(xx);
        }
        [HttpPut("Delete/{Id}")]

        public bool DeleteAsync(Guid Id)
        {
            return _xuatsusv.Del(Id);
        }

    }
}
