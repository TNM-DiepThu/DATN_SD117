using AppData.model;
using AppData.Serviece.Implements;
using AppData.Serviece.Interfaces;
using Bill.Serviece.Implements;
using Bill.Serviece.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnhController : ControllerBase
    {
        private readonly IAnhServiece _anhsv;
        private readonly IAnhSanPhamService anhSanPhamService;
        public AnhController()
        {
            _anhsv = new AnhServiece();
            anhSanPhamService = new AnhSanPhamService();
        }
        [HttpGet("GetAll")]

        public IEnumerable<Anh> GetAllAnh()
        {
            return _anhsv.GetAll();
        }

        [HttpGet("[action]")]
        public IEnumerable<AnhSanPham> GetAllAnhsp()
        {
            return anhSanPhamService.GetAllAnhChoSanPham();
        }
        [HttpGet("[action]")]
        public IEnumerable<AnhSanPham> GetAllAnhByIDsp(Guid idsp)
        {
            return anhSanPhamService.GetAllAnhChoSanPhamBySP(idsp);
        }
        [HttpPost("Create")]
        public bool CreateAnh(string name)
        {
            Anh anh = new Anh()
            {
                Id = Guid.NewGuid(),
                Connect = name,
                status = 1,
            };
            return _anhsv.Add(anh);
        }
    }
}
