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
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            _spctViewModel = new SanPhamChiTietViewModelService();
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
        public async Task<ActionResult> DeleteCBAsync(Combo cb)
        {
            string url = $"https://localhost:7214/api/Combo/DeleteComBo?Id={cb.Id}";
            var obj = JsonConvert.SerializeObject(cb);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.DeleteAsync(url);

            return RedirectToAction("GetlistComBO");

        }
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
            var sp = JsonConvert.DeserializeObject<Combo>(data);
            return View(sp);

        [HttpGet]
        [HttpPost]
        public ActionResult CreateComBoChiTiet(ComboChiTiet cbct)
        {
            string apiurl = $"https://localhost:7214/api/Combo/Update/{id}?Ten={cb.TenCombo}&mota={cb.MoTaCombo}&giatien={cb.PhanTramGiam}";

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
        public IActionResult GetlistComBOCT()
        {
            string url = "https://localhost:7214/api/ComBoChiTiet/GetAll";
            var respon = _client.GetAsync(url).Result;
            var data = respon.Content.ReadAsStringAsync().Result;
            List<ComboChiTiet> lstCombo = JsonConvert.DeserializeObject<List<ComboChiTiet>>(data);
            return View(lstCombo);
        }
        public IActionResult Detail(Guid id)
        {
            string apiurl = $"https://localhost:7214/api/Combo/{id}";
            var httpClient = new HttpClient();
            var respone = httpClient.GetAsync(apiurl).Result;
            var data = respone.Content.ReadAsStringAsync().Result;
            var sp = JsonConvert.DeserializeObject<Combo>(data);
            return View(sp);

        }
    }
}
