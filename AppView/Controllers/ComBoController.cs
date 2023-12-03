using AppData.data;
using AppData.model;
using AppData.Serviece.Implements;
using AppData.Serviece.Interfaces;
using AppData.Serviece.ViewModeService;
using AppData.ViewModal.SanPhamChiTietVM;
using AppData.ViewModal.Usermodalview;
using AppData.ViewModal.VoucherVM;
using Bill.Serviece.Implements;
using Bill.Serviece.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace AppView.Controllers
{
    public class ComBoController : Controller
    {

        HttpClient _client;
        private readonly MyDbContext _context;
        private readonly IGioHangService _giohangService;
        private readonly SanPhamChiTietViewModelService _spctviewmodel;

        public ComBoController()
        {
            _client = new HttpClient();
            _spctviewmodel = new SanPhamChiTietViewModelService();
            _giohangService = new GioHangService();
            _context = new MyDbContext();

        }
        [HttpGet]
        public IActionResult GetlistComBO()
        {
            string url = "https://localhost:7214/api/Combo/GetAll";
            var respon = _client.GetAsync(url).Result;
            var data = respon.Content.ReadAsStringAsync().Result;
            List<Combo> lstCombo = JsonConvert.DeserializeObject<List<Combo>>(data);
            return View(lstCombo);
        }

        //// GET: ComBoController/Details/5
        //public ActionResult Details(int id)
        //{
        //    //https://localhost:7214/api/Combo/GetAll
        //    return View();
        //}
        [HttpGet]
        [HttpPost]

        public async Task<ActionResult> CreateComBO(Combo combo)
        {
            string url = $"https://localhost:7214/api/Combo/Create?Ten={combo.TenCombo}&mota={combo.MoTaCombo}&phantramgiam={combo.PhanTramGiam}";

            var obj = JsonConvert.SerializeObject(combo);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PostAsync(url, content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetlistComBO");
            }
            else
            {
                return View();
            }

        }

        // GET: ComBoController/Delete/5
        [HttpGet]
        [HttpPut]
        public ActionResult DeleteCB(Combo cb)
        {
            string url = $"https://localhost:7214/api/Combo/DeleteComBo?Id={cb.Id}";
            var obj = JsonConvert.SerializeObject(cb);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = _client.PutAsync(url, content).Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessFull"] = "Thành công";
                return RedirectToAction("GetlistComBO");
            }
            else
            {
                TempData["ErrorMessage"] = "Lỗi xóa";
                return RedirectToAction("GetlistComBO");
            }
        }

        [HttpGet]
        [HttpPost]
        public IActionResult UpdateCB(Combo combo)
        {
            string ApiDetail = $"https://localhost:7214/api/Combo/GetbyID?id={combo.Id}";
            var repones = _client.GetAsync(ApiDetail).Result;
            var data = repones.Content.ReadAsStringAsync().Result;
            Combo comboDetail = JsonConvert.DeserializeObject<Combo>(data);

            string Apiupdate = $"https://localhost:7214/api/Combo/Update/{combo.Id}?Ten={combo.TenCombo}&mota={combo.MoTaCombo}&phantram={combo.PhanTramGiam}";

            var obj = JsonConvert.SerializeObject(combo);
            StringContent stringContent = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = _client.PostAsync(Apiupdate, stringContent).Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetlistComBO");
            }
            else
            {
                return View(comboDetail);
            }
        }


        // Combo chi tiết


        [HttpGet]
        public IActionResult GetlistComBOCT()
        {
            string url = "https://localhost:7214/api/ComBoChiTiet/GetallFullComboCt";
            var respon = _client.GetAsync(url).Result;
            var data = respon.Content.ReadAsStringAsync().Result;
            List<ComBoChiTietViewModel> lstCombo = JsonConvert.DeserializeObject<List<ComBoChiTietViewModel>>(data);
            return View(lstCombo);
        }

        [HttpGet]
        public IActionResult DetailComboCT(Guid id)
        {
            string apiurl = $"https://localhost:7214/api/ComBoChiTiet/GetallFullComboCtByID?id={id}";
            var httpClient = new HttpClient();
            var respone = httpClient.GetAsync(apiurl).Result;
            var data = respone.Content.ReadAsStringAsync().Result;
            ComBoChiTietViewModel sp = JsonConvert.DeserializeObject<ComBoChiTietViewModel>(data);
            return View(sp);

        }

        [HttpGet]
        [HttpPost]
        public IActionResult UpdateComBoCT(ComboChiTiet cbct)
        {
            ViewBag.Combo = new SelectList(_context.combos.ToList().Where(c => c.status == 1).OrderBy(c => c.TenCombo), "Id", "TenCombo");
            ViewBag.spct = new SelectList(_spctviewmodel.GetAll().Where(c => c.status == 1).OrderBy(c => c.TenSP), "Id", "TenSP");
            string apiurlDetail = $"https://localhost:7214/api/ComBoChiTiet/GetByID?ID={cbct.Id}";
            var httpClient = new HttpClient();
            var respone = httpClient.GetAsync(apiurlDetail).Result;
            var data = respone.Content.ReadAsStringAsync().Result;
            ComboChiTiet sp = JsonConvert.DeserializeObject<ComboChiTiet>(data);

            string urlupdate = $"https://localhost:7214/api/ComBoChiTiet/UpdateCombo?id={cbct.Id}&soluongsanpham={cbct.SoLuongSanPham}&tencombo={cbct.TenComboct}&IDcombo={cbct.IdCombo}&IDCTSP={cbct.IdSPCT}&soluongcombo={cbct.SoLuongCombo}&trangthai={cbct.TrangThai}";

            var obj = JsonConvert.SerializeObject(cbct);
            StringContent stringContent = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = _client.PostAsync(urlupdate, stringContent).Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetlistComBOCT");
            }
            else
            {
                return View(sp);
            }
        }

        [HttpGet]
        [HttpPost]
        public ActionResult CreateComBoChiTiet(ComboChiTiet cbct)
        {
            ViewBag.Combo = new SelectList(_context.combos.ToList().Where(c => c.status == 1).OrderBy(c => c.TenCombo), "Id", "TenCombo");
            ViewBag.SPCT = new SelectList(_spctviewmodel.GetAll().ToList().Where(c => c.status == 1).OrderBy(c => c.TenSP), "Id", "TenSP");

            string url = $"https://localhost:7214/api/ComBoChiTiet/Create?soluongsanpham={cbct.SoLuongSanPham}&tencombo={cbct.TenComboct}&IDcombo={cbct.IdCombo}&IDCTSP={cbct.IdSPCT}&soluongcombo={cbct.SoLuongCombo}";
            var obj = JsonConvert.SerializeObject(cbct);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = _client.PostAsync(url, content).Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetlistComBOCT");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        [HttpPost]
        public ActionResult ThemComBoVaoGioHang(ComboChiTiet cbct)
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;

            var userlogin = HttpContext.User;
            var email = userlogin.FindFirstValue(ClaimTypes.Email);
            var user = _context.Users.FirstOrDefault(c => c.Email == email);
            var giohang = _giohangService.GetAll().FirstOrDefault(c => c.IdNguoiDung == user.Id);
            string url = $"https://localhost:7214/api/ComBoChiTiet/ThemComBoVaoGioHang?idnguoidung={user.Id}&IDComboCt={cbct.Id}&SoLuong=1";
            GioHangChiTiet gioHangChiTiet = new GioHangChiTiet()
            {
                Id = Guid.NewGuid(),
                IdComboChiTiet = null,
                IdGioHang = giohang.Id,
                IdSanPhamChiTiet = null,
                SoLuong = 1,
            };

            var obj = JsonConvert.SerializeObject(gioHangChiTiet);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = _client.PostAsync(url, content).Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetGioHangChiTiet", "GioHang");
            }
            else
            {
                return RedirectToAction("GetlistComBOCT", "ComBo");
            }
        }
    }
}
