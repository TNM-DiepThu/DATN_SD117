using AppData.data;
using AppData.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace AppView.Controllers
{
    public class LichsuHDController : Controller
    {
        private readonly ILogger<LichsuHDController> _logger;
        HttpClient _client = new HttpClient();
        private readonly MyDbContext _context;


        public LichsuHDController(ILogger<LichsuHDController> logger)
        {
            _logger = logger;
            _context = new MyDbContext();

        }
        [HttpGet]
        public async Task<ActionResult> GetAllLsHD()
        {
            /* string url = "https://localhost:7214/api/HoaDon/get-hoadon";
             var respon = _client.GetAsync(url).Result;
             var data = respon.Content.ReadAsStringAsync().Result;
             List<HoaDon> lsthoadon = JsonConvert.DeserializeObject<List<HoaDon>>(data);*/
            var response = await _client.GetFromJsonAsync<List<LichSuHoaDon>>($"https://localhost:7214/api/LichsuHoaDon/get-lichsuhoadon");
            return View(response);
            //return View(lsthoadon);

        }
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult> CreateLsHD(LichSuHoaDon lshoadon)
        {
            ViewBag.User = new SelectList(_context.Users.ToList().Where(c => c.status == 1).OrderBy(c => c.UserName), "Id", "UserName");
            ViewBag.HoaDon = new SelectList(_context.hoaDons.ToList().Where(c => c.status == 1).OrderBy(c => c.MaHD), "Id", "MaHD");
            string url = $"https://localhost:7214/api/LichsuHoaDon/create-lichsuhoadon?ngaytao={lshoadon.NgayTao}&nguoitao={lshoadon.NguoiTao}&ghichu={lshoadon.GhiChu}&idnguoidung={lshoadon.IdNguoiDung}&idvoucherdetail={lshoadon.IdHoaDon}";
            var obj = JsonConvert.SerializeObject(lshoadon);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PostAsync(url, content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllLsHD", "LichsuHD");
            }
            else
            {
               
                return View();

            }

        }

        [HttpGet]
        [HttpPost]
        public async Task<ActionResult> UpdateLsHD(LichSuHoaDon lshoadon)
        {
            ViewBag.User = new SelectList(_context.Users.ToList().Where(c => c.status == 1).OrderBy(c => c.UserName), "Id", "UserName");
            ViewBag.HoaDon = new SelectList(_context.hoaDons.ToList().Where(c => c.status == 1).OrderBy(c => c.MaHD), "Id", "MaHD");
            string urldetail = $"https://localhost:7214/api/LichSuHoaDon/GetByID?id={lshoadon.Id}";
            var respon1 = _client.GetAsync(urldetail).Result;
            var data1 = respon1.Content.ReadAsStringAsync().Result;
            LichSuHoaDon lstsize = JsonConvert.DeserializeObject<LichSuHoaDon>(data1);
            string url = $"https://localhost:7214/api/LichsuHoaDon/update-lichsuhoadon?ngaytao={lshoadon.NgayTao}&nguoitao={lshoadon.NguoiTao}&ghichu={lshoadon.GhiChu}&idnguoidung={lshoadon.IdNguoiDung}&idvoucherdetail={lshoadon.IdHoaDon}";


            var obj = JsonConvert.SerializeObject(lshoadon);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PostAsync(url, content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllLsHD", "LichsuHD");
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
            string apiUrl = $"https://localhost:7214/api/LichsuHoaDon/delete-lichsuhoadon?id={id}";
            var response = await httpClient.DeleteAsync(apiUrl);
            return RedirectToAction("GetAllLsHD", "LichsuHD");
        }
    }
}
