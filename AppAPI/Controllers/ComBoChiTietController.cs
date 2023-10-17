using AppData.model;
using AppData.Serviece.Implements;
using AppData.Serviece.Interfaces;
using AppData.Serviece.ViewModeService;
using AppData.ViewModal.SanPhamChiTietVM;
using Bill.Serviece.Implements;
using Bill.Serviece.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComBoChiTietController : ControllerBase
    {
        private readonly IComboService Cb;
        private readonly ComBoChiTietViewModelService _combochitietViewModel;
        private readonly IComboChiTietService CbChiTiet;
        private readonly ISanPhamChiTietServiece sanPhamChiTietServiece;
        public ComBoChiTietController()
        {
            Cb = new ComboService();
            CbChiTiet = new ComBoChiTietService();
            _combochitietViewModel = new ComBoChiTietViewModelService();
            sanPhamChiTietServiece = new SanPhamChiTietServiece();


        }
        [HttpGet("GetAll")]

        public IEnumerable<ComboChiTiet> GetAllAsync()
        {
            return CbChiTiet.GetAll();
        }
        [HttpPost("Create")]
        public bool Create(int soluongsanpham, decimal giatien, Guid IDcombo, Guid IDCTSP)
        {
            ComboChiTiet comboct = new ComboChiTiet()
            {
                Id = Guid.NewGuid(),
                SoLuongSanPham = soluongsanpham,
                GiaBan = giatien,
                IdCombo = Cb.GetAll().FirstOrDefault(c => c.Id == IDcombo).Id,
            };
            return CbChiTiet.Add(comboct);
        }
        [HttpDelete("Delete/{Id}")]

        public bool DeleteAsync(Guid Id)
        {
            var result = CbChiTiet.Del(Id);
            return result;
        }

        [HttpPut("Update/{id}")]

        public bool UpdateAsync(Guid id, [FromBody] ComboChiTiet p)
        {
            var result = CbChiTiet.Edit(id, p);
            return result;
        }
        [HttpGet("[action]")]
        public List<ComBoChiTietViewModel> GetallFullComboCt()
        {
            return _combochitietViewModel.GetAllComBoChiTiet();
        }
        [HttpGet("[action]")]
        public List<ComBoChiTietViewModel> GetallFullComboCtByName(string name)
        {
            return _combochitietViewModel.GetAllComBoChiTietByName(name);
        }
    }
}
