using AppData.data;
using AppData.model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace AppView.Controllers
{
    public class HoaDonController : Controller
    {
        private readonly ILogger<HoaDonController> _logger;
        HttpClient _client = new HttpClient();
        private readonly MyDbContext _context;
        
        
        public HoaDonController(ILogger<HoaDonController> logger)
        {
            _logger = logger;
            _context = new MyDbContext();
            _context = new MyDbContext();
           
        }
        [HttpGet]
        public async Task<ActionResult> GetAllHD()
        {
           /* string url = "https://localhost:7214/api/HoaDon/get-hoadon";
            var respon = _client.GetAsync(url).Result;
            var data = respon.Content.ReadAsStringAsync().Result;
            List<HoaDon> lsthoadon = JsonConvert.DeserializeObject<List<HoaDon>>(data);*/
            var response = await _client.GetFromJsonAsync<List<HoaDon>>($"https://localhost:7214/api/HoaDon/get-hoadon");
            return View(response);
            //return View(lsthoadon);
           
        }
        public Guid IDNguoiDung()
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;

            var userlogin = HttpContext.User;
            var email = userlogin.FindFirstValue(ClaimTypes.Email);
            var user = _context.Users.FirstOrDefault(c => c.Email == email);
            return user.Id;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllHDById()
        {
            Guid IDnguoidung = IDNguoiDung();
            string url = $"https://localhost:7214/api/HoaDon/GetAllHoaDonByIDnguoiDung?id={IDnguoidung}";
            var respone = await _client.GetAsync(url);
            var data = await respone.Content.ReadAsStringAsync();
            List<HoaDon> lsthd = JsonConvert.DeserializeObject<List<HoaDon>>(data);
            return View(lsthd);
        }
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult> CreateHoaDon(HoaDon hoadon)
        {
            ViewBag.User = new SelectList(_context.Users.ToList().Where(c => c.status == 1).OrderBy(c => c.UserName), "Id", "UserName");
            ViewBag.HinhThucTT = new SelectList(_context.hinhThucThanhToans.ToList().Where(c => c.status == 1).OrderBy(c => c.TenHinhThucThanhToan), "Id", "TenHinhThucThanhToan");
            ViewBag.VoucherDetail = new SelectList(_context.voucherDetail.ToList().Where(c => c.status == 1).OrderBy(c => c.Id), "Id", "Id");
            string url = $"https://localhost:7214/api/HoaDon/CreateHoaDon?ngaygiao={hoadon.NgayGiao}&ngaynhan={hoadon.NgayNhan}&nguoinhan={hoadon.NguoiNhan}&sdt={hoadon.SDT}&quanhuyen={hoadon.QuanHuyen}&tinh={hoadon.Tinh}&diachi={hoadon.DiaChi}&ngaythanhtoan={hoadon.NgayThanhToan}&ghichu={hoadon.GhiChu}&idnguoidung={hoadon.IdNguoiDunh}&idhttt={hoadon.IDHTTT}";
            var obj = JsonConvert.SerializeObject(hoadon);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PostAsync(url, content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllHD", "HoaDon");
            }
            else
            {    
                return View();

            }
        }

        [HttpGet]
        [HttpPost]
        public async Task<ActionResult> UpdateHoaDon(HoaDon hoadon)
        {
            ViewBag.User = new SelectList(_context.Users.ToList().Where(c => c.status == 1).OrderBy(c => c.UserName), "Id", "UserName");
            ViewBag.HinhThucTT = new SelectList(_context.hinhThucThanhToans.ToList().Where(c => c.status == 1).OrderBy(c => c.TenHinhThucThanhToan), "Id", "TenHinhThucThanhToan");
            ViewBag.VoucherDetail = new SelectList(_context.voucherDetail.ToList().Where(c => c.status == 1).OrderBy(c => c.Id), "Id", "Id");
            string urldetail = $"https://localhost:7214/api/HoaDon/GetByID?id={hoadon.Id}";
            var respon1 = _client.GetAsync(urldetail).Result;
            var data1 = respon1.Content.ReadAsStringAsync().Result;
            HoaDon lstsize = JsonConvert.DeserializeObject<HoaDon>(data1);

            string url = $"https://localhost:7214/api/HoaDon/update-hoadon?id={hoadon.Id}&mahd={hoadon.MaHD}&ngaytao={hoadon.NgayTao}&soluong={hoadon.SoLuong}&tongtien={hoadon.TongTien}&tienvanchuyen={hoadon.TienVanChuyen}&ngaygiao={hoadon.NgayGiao}&ngaynhan={hoadon.NgayNhan}&nguoinhan={hoadon.NguoiNhan}&sdt={hoadon.SDT}&quanhuyen={hoadon.QuanHuyen}&tinh={hoadon.Tinh}&diachi={hoadon.DiaChi}&ngaythanhtoan={hoadon.NgayThanhToan}&ghichu={hoadon.GhiChu}&trangthai={hoadon.status}&idnguoidung={hoadon.IdNguoiDunh}&idvoucherdetail={hoadon.IdVoucherDetail}&idhttt={hoadon.IDHTTT}";
          
            var obj = JsonConvert.SerializeObject(hoadon);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PostAsync(url, content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllHD", "HoaDon");
            }
            else
            {
                return View(lstsize);
            }
        }
        [HttpPut("[action]")]
        public async Task<ActionResult> Delete(Guid id)
        {
            return View();
        }

        //Tính tiền ship
        public decimal TinhTienShip(Guid IdHd , string Quan_Huyen , string Tinh_Thanhpho)
        {

            return 0;
        }

        [HttpPost]
        public IActionResult UpdateDaXacNhan(HoaDon hd)
        {
            string url = $"https://localhost:7214/api/HoaDon/UpdateDaXacNhan?idhb={hd.Id}";
            var respons = _client.GetAsync(url).Result;
            var obj = JsonConvert.SerializeObject(hd);
            StringContent stringContent = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = _client.PostAsync(url, stringContent).Result;
            if(httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllHD","HoaDon");
            }
            else
            {
                return RedirectToAction("GetAllHD", "HoaDon");
            }
        }
        [HttpPost]
        public IActionResult UpdateChoLayHang(HoaDon hd)
        {
            string url = $"https://localhost:7214/api/HoaDon/UpdateChoLayHang?idhb={hd.Id}";
            var respons = _client.GetAsync(url).Result;
            var obj = JsonConvert.SerializeObject(hd);
            StringContent stringContent = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = _client.PostAsync(url, stringContent).Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllHD", "HoaDon");
            }
            else
            {
                return RedirectToAction("GetAllHD", "HoaDon");
            }
        }
        [HttpPost]
        public IActionResult UpdateDaLayHang(HoaDon hd)
        {
            string url = $"https://localhost:7214/api/HoaDon/UpdateDaLayhang?idhb={hd.Id}";
            var respons = _client.GetAsync(url).Result;
            var obj = JsonConvert.SerializeObject(hd);
            StringContent stringContent = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = _client.PostAsync(url, stringContent).Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllHD", "HoaDon");
            }
            else
            {
                return RedirectToAction("GetAllHD", "HoaDon");
            }
        }
        [HttpPost]
        public IActionResult UpdateHuy(HoaDon hd)
        {
            string url = $"https://localhost:7214/api/HoaDon/UpdateHuyHang?idhb={hd.Id}";
            var respons = _client.GetAsync(url).Result;
            var obj = JsonConvert.SerializeObject(hd);
            StringContent stringContent = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = _client.PostAsync(url, stringContent).Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllHD", "HoaDon");
            }
            else
            {
                return RedirectToAction("GetAllHD", "HoaDon");
            }
        }
        [HttpPost]
        public IActionResult UpdateDaNhanHang(HoaDon hd)
        {
            string url = $"https://localhost:7214/api/HoaDon/UpdateDaNhanHang?idhb={hd.Id}";
            var respons = _client.GetAsync(url).Result;
            var obj = JsonConvert.SerializeObject(hd);
            StringContent stringContent = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = _client.PostAsync(url, stringContent).Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllHD", "HoaDon");
            }
            else
            {
                return RedirectToAction("GetAllHD", "HoaDon");
            }
        }
        [HttpPost]
        public IActionResult UpdateDaThanhToan(HoaDon hd)
        {
            string url = $"https://localhost:7214/api/HoaDon/UpdateDaThanhToan?idhb={hd.Id}";
            var respons = _client.GetAsync(url).Result;
            var obj = JsonConvert.SerializeObject(hd);
            StringContent stringContent = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = _client.PostAsync(url, stringContent).Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllHD", "HoaDon");
            }
            else
            {
                return RedirectToAction("GetAllHD", "HoaDon");
            }
        }
    }
}
