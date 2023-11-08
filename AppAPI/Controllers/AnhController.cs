using AppData.data;
using AppData.model;
using AppData.Serviece.Implements;
using AppData.Serviece.Interfaces;
using AppData.Serviece.ViewModeService;
using AppData.ViewModal.SanPhamChiTietVM;
using Bill.Serviece.Implements;
using Bill.Serviece.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnhController : ControllerBase
    {
        private readonly IAnhServiece _anhsv;
        private readonly IAnhSanPhamService anhSanPhamService;
        private readonly MyDbContext _context;
        private readonly SanPhamChiTietViewModelService _spctviewmodel;
        public AnhController()
        {
            _anhsv = new AnhServiece();
            anhSanPhamService = new AnhSanPhamService();
            _spctviewmodel = new SanPhamChiTietViewModelService();
            _context = new MyDbContext();
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

        [HttpGet("[action]")]
        public List<Guid> Test(Guid idsp)
        {
            List<Guid> DanhsachIdAnh = new List<Guid>();

            foreach (var sp in anhSanPhamService.GetAllAnhChoSanPham())
            {
                if (sp.IdSanPhamChiTiet == idsp)
                {
                    DanhsachIdAnh.Add(sp.Idanh);
                }
            }
            return DanhsachIdAnh;
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
        [HttpPost("[action]")]
        public bool ThemAnhChoSanPham(Guid IDSpct , Guid IDAnh)
        {
            AnhSanPham anh = new AnhSanPham()
            {
               Idanh = IDAnh,
               IdSanPhamChiTiet = IDSpct,
            };
            return anhSanPhamService.AddAnhChoSanPham(anh);
        }

        [HttpGet("[action]")]
        public List<SanPhamChiTietViewModel> GetAllAnhSanPhamCt()
        {
            var products = _spctviewmodel.GetAll().OrderByDescending(c => c.MaSp);
            foreach (var product in products)
            {
                // Lấy các hình ảnh cho sản phẩm
                var productImages = _context.anhSanPhams
                .Where(a => a.IdSanPhamChiTiet == product.Id)
                .Select(a => new Anh { Id = a.Idanh, Connect = a.Anh.Connect, status = a.Anh.status })
                .ToList();
                // Gán danh sách hình ảnh cho sản phẩm
                product.Images = productImages;
            }

            return products.ToList();

        }
    }
}
