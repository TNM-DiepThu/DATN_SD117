using AppData.model;
using AppData.Serviece.Implements;
using AppData.Serviece.Interfaces;
using AppData.ViewModal.GioHangChiTietViewModel;
using AppData.ViewModal.Usermodalview;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace AppView.Controllers
{
    public class GioHangController : Controller
    {
        HttpClient _client = new HttpClient();
        private readonly IGioHangCTService ghctservice;
        //private readonly INguoiDungServiece _nguoiDungServiece;

        public GioHangController(/*INguoiDungServiece nguoiDungServiece*/)
        {
            //_nguoiDungServiece = nguoiDungServiece;
            ghctservice = new GioHangCTService();
        }

        // GET: GioHangController
        public ActionResult Index()
        {
            return View();

        }

        // GET: GioHangController/Details/5
        public async Task<ActionResult> GetallGH()
        {
            var response = await _client.GetFromJsonAsync<List<GioHang>>($"https://localhost:7214/api/GioHang/GetAll");
            return View(response);
        }

        [HttpGet]
        public async Task<ActionResult> GetGioHangChiTiet()
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;

            var userlogin = HttpContext.User;
            var email = userlogin.FindFirstValue(ClaimTypes.Email);
            //NguoiDungVM user = await _nguoiDungServiece.GetAllAsync().;
            string url = $"https://localhost:7214/api/GioHangCT/GetAllFullGioHangChiTiet?IDnguoiDung=911a9476-05be-4a4f-8325-2ea61766e2a0";
            var repos = await _client.GetAsync(url);
            var data = await repos.Content.ReadAsStringAsync();
            List<GioHangChiTietViewModel> lstghct = JsonConvert.DeserializeObject<List<GioHangChiTietViewModel>>(data);
            return View(lstghct);
        }
        // GET: GioHangController/Create
        public async Task<ActionResult> CreateGioHang(GioHang gh)
        {
            string url = $"https://localhost:7214/api/GioHang/Create?{gh.GhiChu}";


            var obj = JsonConvert.SerializeObject(gh);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PostAsync(url, content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetallGH");
            }
            else
            {
                return View("CreateGioHang");
            }
        }

        // POST: GioHangController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GioHangController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GioHangController/Edit/5
        [HttpGet]
        public IActionResult UpdateGH(Guid Id)
        {
            string apiurl = $"https://localhost:7214/api/GioHang/Update/{Id}";

            var respone = _client.GetAsync(apiurl).Result;
            var data = respone.Content.ReadAsStringAsync().Result;
            var sp = JsonConvert.DeserializeObject<GioHang>(data);





            return View(sp);

        }
        [HttpPost]
        public IActionResult UpdateGH(Guid id, GioHang gh)
        {
            //string url = $"https://localhost:7214/api/Combo/Update/{cb.Id}";
            //var obj = JsonConvert.SerializeObject(cb);
            //StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            //HttpResponseMessage httpResponseMessage = await _client.PutAsync(url, content);

            //return RedirectToAction("GetlistComBO");
            string apiurl = $"https://localhost:7214/api/GioHang/Update/{id}?Ten={gh.GhiChu}";

            var obj = JsonConvert.SerializeObject(gh);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            var respone = _client.PutAsJsonAsync(apiurl, obj).Result;
            if (respone.IsSuccessStatusCode)
            {
                return RedirectToAction("GetlistComBO");
            }
            else
            {
                return RedirectToAction("GetallGH");
            }


        }

        // GET: GioHangController/Delete/5
        public async Task<ActionResult> DeleteAsync(GioHang gio)
        {
            string url = $"https://localhost:7214/api/GioHang/Delete/{gio.Id}";
            var obj = JsonConvert.SerializeObject(gio);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PutAsync(url, content);

            return RedirectToAction("GetallGH", "QuanTri");
        }

        // POST: GioHangController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> XoaSanPhamKhoiGioHang(Guid id)
        {
            string url = $"https://localhost:7214/api/GioHangCT/XoaSanPham?id={id}";
            var reposs = await _client.GetAsync(url);
            HttpResponseMessage Xoa = await _client.DeleteAsync(url);
            if (Xoa.IsSuccessStatusCode)
            {
                // Xóa thành công
                TempData["success"] = "Xóa thành công.";
                return RedirectToAction("GetGioHangChiTiet");
            }
            else
            {
                // Xử lý lỗi, ví dụ: hiển thị thông báo lỗi
                TempData["Error"] = "Xóa không thành công.";
                return RedirectToAction("GetGioHangChiTiet");
            }
        }
        [HttpGet]
        [HttpPost]
        public ActionResult DecreaseButton(Guid id , int soluong)
        {
            var idnguoidung = Guid.Parse("911a9476-05be-4a4f-8325-2ea61766e2a0");
            GioHangChiTiet ghct = ghctservice.GetAllGioHangTheoNguoiDungDangNhap(idnguoidung).FirstOrDefault(c => c.Id == id); 
            if (ghct != null)
            {
                if(soluong > 0)
                {
                    soluong--;
                }
                ghct.SoLuong = soluong;
            }
            ghctservice.EditSoluong(id , soluong);
            return RedirectToAction("GetGioHangChiTiet");
        }
        [HttpGet]
        [HttpPost]
        public ActionResult IncreaseButton(Guid id, int soluong)
        {
            var idnguoidung = Guid.Parse("911a9476-05be-4a4f-8325-2ea61766e2a0");
            GioHangChiTiet ghct = ghctservice.GetAllGioHangTheoNguoiDungDangNhap(idnguoidung).FirstOrDefault(c => c.Id == id);
            soluong++;
            if (ghct != null)
            {
                ghct.SoLuong = soluong;
            }
            ghctservice.EditSoluong(id, soluong);
            return RedirectToAction("GetGioHangChiTiet");
        }
    }
}
