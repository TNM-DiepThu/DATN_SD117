using AppData.data;
using AppData.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace AppView.Controllers
{
    public class HoaDonCTController : Controller
    {
        private readonly ILogger<HoaDonCTController> _logger;
        HttpClient _client = new HttpClient();
        private readonly MyDbContext _context;
      
        public HoaDonCTController(ILogger<HoaDonCTController> logger)
        {
            _logger = logger;
            _context = new MyDbContext();
           
        }
        [HttpGet]
        public async Task<ActionResult> GetAllHDCT()
        {
            /* string url = "https://localhost:7214/api/HoaDon/get-hoadon";
             var respon = _client.GetAsync(url).Result;
             var data = respon.Content.ReadAsStringAsync().Result;
             List<HoaDon> lsthoadon = JsonConvert.DeserializeObject<List<HoaDon>>(data);*/
            var response = await _client.GetFromJsonAsync<List<HoaDonChiTiet>>($"https://localhost:7214/api/HoaDonCT/get-hoadonct");
            return View(response);
            //return View(lsthoadon);

        }
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult> CreateHoaDonCT(HoaDonChiTiet hoadonct)
        {
            ViewBag.HoaDon = new SelectList(_context.hoaDons.ToList().Where(c => c.status == 1).OrderBy(c => c.MaHD), "Id", "MaHD");
            ViewBag.ComboChiTiet = new SelectList(_context.comboChiTiets.ToList().OrderBy(c => c.Id), "Id", "Id");
            ViewBag.SanPhamChiTiet = new SelectList(_context.sanPhamChiTiets.ToList().Where(c => c.status == 1).OrderBy(c => c.MaSp), "Id", "MaSp");

            string url = $"https://localhost:7214/api/HoaDonCT/create-hoadonct?soluong={hoadonct.SoLuong}&gia={hoadonct.Gia}&status={hoadonct.status}&idhd={hoadonct.IDHD}&IdCombo={hoadonct.IdCombo}&IdSPCT={hoadonct.IdSPCT}";
            var obj = JsonConvert.SerializeObject(hoadonct);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PostAsync(url, content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllHDCT", "HoaDonCT");
            }
            else
            {
                

                return View();

            }

        }

        [HttpGet]
        [HttpPost]
        public async Task<ActionResult> UpdateHoaDonCT(HoaDonChiTiet hoadonct)
        {
            ViewBag.HoaDon = new SelectList(_context.hoaDons.ToList().Where(c => c.status == 1).OrderBy(c => c.MaHD), "Id", "MaHD");
            ViewBag.ComboChiTiet = new SelectList(_context.comboChiTiets.ToList().OrderBy(c => c.Id), "Id", "Id");
            ViewBag.SanPhamChiTiet = new SelectList(_context.sanPhamChiTiets.ToList().Where(c => c.status == 1).OrderBy(c => c.MaSp), "Id", "MaSp");

            string urldetail = $"https://localhost:7214/api/HoaDonChiTiet/GetByID?id={hoadonct.Id}";
            var respon1 = _client.GetAsync(urldetail).Result;
            var data1 = respon1.Content.ReadAsStringAsync().Result;
            HoaDonChiTiet lstsize = JsonConvert.DeserializeObject<HoaDonChiTiet>(data1);

            string url = $"https://localhost:7214/api/HoaDonCT/update-hoadonct?id={hoadonct.Id}&soluong={hoadonct.SoLuong}&gia={hoadonct.Gia}&status={hoadonct.status}&idhd={hoadonct.IDHD}&IdCombo={hoadonct.IdCombo}&IdSPCT={hoadonct.IdSPCT}";


            var obj = JsonConvert.SerializeObject(hoadonct);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PostAsync(url, content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllHDCT", "HoaDonCT");
            }
            else
            {
               
                return View(lstsize);

            }

        }
        public async Task<ActionResult> Delete(Guid id)
        {
            /* string url = $"https://localhost:7214/api/HoaDon/delete-hoadon?id={id}";
             var obj = JsonConvert.SerializeObject(id);
             StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
             HttpResponseMessage httpResponseMessage = await _client.PutAsync(url, content);

             return RedirectToAction("GetAllHD", "HoaDon");*/
            //var cus = .GetAll().First(c => c.ColorID == id);
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7214/api/HoaDonCT/delete-hoadonct?id={id}";
            var response = await httpClient.DeleteAsync(apiUrl);
            return RedirectToAction("GetAllHDCT", "HoaDonCT");
        }
    }
}
