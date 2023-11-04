using AppData.data;
using AppData.model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
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
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult> CreateHoaDon(HoaDon hoadon)
        {
            ViewBag.User = new SelectList(_context.Users.ToList().Where(c => c.status == 1).OrderBy(c => c.UserName), "Id", "UserName");
            ViewBag.HinhThucTT = new SelectList(_context.hinhThucThanhToans.ToList().Where(c => c.status == 1).OrderBy(c => c.TenHinhThucThanhToan), "Id", "TenHinhThucThanhToan");
            ViewBag.VoucherDetail = new SelectList(_context.voucherDetail.ToList().Where(c => c.status == 1).OrderBy(c => c.Id), "Id", "Id");
            string url = $"https://localhost:7214/api/HoaDon/create-hoadon?mahd={hoadon.MaHD}&ngaytao={hoadon.NgayTao}&soluong={hoadon.SoLuong}&tongtien={hoadon.TongTien}&tienvanchuyen={hoadon.TienVanChuyen}&ngaygiao={hoadon.NgayGiao}&ngaynhan={hoadon.NgayNhan}&nguoinhan={hoadon.NguoiNhan}&sdt={hoadon.SDT}&quanhuyen={hoadon.QuanHuyen}&tinh={hoadon.Tinh}&diachi={hoadon.DiaChi}&ngaythanhtoan={hoadon.NgayThanhToan}&ghichu={hoadon.GhiChu}&trangthai={hoadon.status}&idnguoidung={hoadon.IdNguoiDunh}&idvoucherdetail={hoadon.IdVoucherDetail}&idhttt={hoadon.IDHTTT}";
            // https://localhost:7214/api/HoaDon/CreateHoaDon?ngaytao=2023%2F11%2F02&soluong=312&tongtien=12321&tienvanchuyen=1231&ngaygiao=2023%2F11%2F02&ngaynhan=2023%2F11%2F02&nguoinhan=fafaf&sdt=21312&quanhuyen=fafsa&tinh=adfaf&diachi=%E1%BA%A7df&ngaythanhtoan=2023%2F11%2F02&ghichu=fafda&idnguoidung=6b5e0c51-2d7a-41c8-a1e4-a15ab3dc9296&idvoucherdetail=c529d4e3-e600-4eaa-91ed-77e4dc6c34d4&idhttt=91185eb8-a1a3-4de3-882a-738ba4f501db
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
        public async Task<ActionResult> Delete(Guid id)
        {
           /* string url = $"https://localhost:7214/api/HoaDon/delete-hoadon?id={id}";
            var obj = JsonConvert.SerializeObject(id);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PutAsync(url, content);

            return RedirectToAction("GetAllHD", "HoaDon");*/
            //var cus = .GetAll().First(c => c.ColorID == id);
            var httpClient = new HttpClient();
            string apiUrl = $"https://localhost:7214/api/HoaDon/delete-hoadon?id={id}";
            var response = await httpClient.DeleteAsync(apiUrl);
            return RedirectToAction("GetAllHD","HoaDon");
        }

        //Tính tiền ship
        public decimal TinhTienShip(Guid IdHd , string Quan_Huyen , string Tinh_Thanhpho)
        {

            return 0;
        }
    }
}
