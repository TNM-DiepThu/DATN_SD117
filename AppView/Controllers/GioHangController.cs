using AppData.data;
using AppData.model;
using AppData.Serviece.Implements;
using AppData.Serviece.Interfaces;
using AppData.ViewModal.GioHangChiTietViewModel;
using AppData.ViewModal.SanPhamChiTietVM;
using AppData.ViewModal.Usermodalview;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace AppView.Controllers
{
    public class GioHangController : Controller
    {
        HttpClient _client = new HttpClient();
        private readonly IGioHangCTService ghctservice;
        private readonly MyDbContext _context;


        public GioHangController()
        {
            //_nguoiDungServiece = nguoiDungServiece;
            ghctservice = new GioHangCTService();
            _context = new MyDbContext();

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
        public async Task<ActionResult> GetGioHangChiTiet(GioHang gh)
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            var userlogin = HttpContext.User;
            var email = userlogin.FindFirstValue(ClaimTypes.Email);
            var user = _context.Users.FirstOrDefault(c => c.Email == email);
            string url = $"https://localhost:7214/api/GioHangCT/GetAllFullGioHangChiTiet?IDnguoiDung={user.Id}";
            var repos = await _client.GetAsync(url);
            var data = await repos.Content.ReadAsStringAsync();
            List<GioHangChiTietViewModel> lstghct = JsonConvert.DeserializeObject<List<GioHangChiTietViewModel>>(data);
            return View(lstghct);


        }
        // GET: GioHangController/Create
        public async Task<ActionResult> CreateGioHang(GioHang gh)
        {
            string url = $"https://localhost:7214/api/GioHang/Create?{gh.Id}";


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
        public Guid IDNguoiDung()
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;

            var userlogin = HttpContext.User;
            var email = userlogin.FindFirstValue(ClaimTypes.Email);
            var user = _context.Users.FirstOrDefault(c => c.Email == email);
            return user.Id;
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
        public ActionResult DecreaseButton(Guid id, int soluong)
        {
            Guid Idnguoidung = IDNguoiDung();
            //var idnguoidung = Guid.Parse("911a9476-05be-4a4f-8325-2ea61766e2a0");
            GioHangChiTiet ghct = ghctservice.GetAllGioHangTheoNguoiDungDangNhap(Idnguoidung).FirstOrDefault(c => c.Id == id);
            if (ghct != null)
            {
                if (soluong > 0)
                {
                    soluong--;
                }
                ghct.SoLuong = soluong;
            }
            ghctservice.EditSoluong(id, soluong);
            return RedirectToAction("GetGioHangChiTiet");
        }
        [HttpGet]
        [HttpPost]
        public ActionResult IncreaseButton(Guid id, int soluong)
        {
            Guid Idnguoidung = IDNguoiDung();
            //var idnguoidung = Guid.Parse("911a9476-05be-4a4f-8325-2ea61766e2a0");
            GioHangChiTiet ghct = ghctservice.GetAllGioHangTheoNguoiDungDangNhap(Idnguoidung).FirstOrDefault(c => c.Id == id);
            soluong++;
            if (ghct != null)
            {
                ghct.SoLuong = soluong;
            }
            ghctservice.EditSoluong(id, soluong);
            return RedirectToAction("GetGioHangChiTiet");
        }

        //[HttpGet]
        //[HttpPost]
        //public int LayIDthanhPho(int idthanhpho)
        //{
        //    string json = JsonConvert.SerializeObject(idthanhpho);
        //    HttpContext.Session.SetString("IDThongtinThanhPho", json);
        //    return idthanhpho;
        //}
        [HttpGet]
        [HttpPost]
        public int LayThongTinQuanHuyen(int idquanhuyen)
        {
            string json = JsonConvert.SerializeObject(idquanhuyen);
            HttpContext.Session.SetString("IDThongtinThanhPho", json);
            return idquanhuyen;
        }
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult> ThanhToan(int idthanhpho, int idquanhuyen)
        {
            string token = "be8ee160-008a-11ee-a281-3aa62a37e0a5";
            _client.DefaultRequestHeaders.Add("token", token);
            var urlTinhThanhPho = "https://online-gateway.ghn.vn/shiip/public-api/master-data/province";
            HttpResponseMessage response = await _client.GetAsync(urlTinhThanhPho);
            // lấy ID tỉnh
            if (response.IsSuccessStatusCode)
            {
                var provinces = await response.Content.ReadAsStringAsync();

                JObject jsonObject = JObject.Parse(provinces);

                // Lấy danh sách tỉnh/thành phố từ đối tượng JSON
                JArray provinceArray = (JArray)jsonObject["data"];

                List<ThongTinThanhPho> thongtintp = new List<ThongTinThanhPho>();
                if (provinceArray != null)
                {
                    foreach (JObject province in provinceArray)
                    {
                        ThongTinThanhPho thongTinThanhPho = new ThongTinThanhPho();
                        thongTinThanhPho.Id = Convert.ToInt32(province["ProvinceID"]);
                        thongTinThanhPho.Name = Convert.ToString(province["ProvinceName"]);
                        thongtintp.Add(thongTinThanhPho);
                    }
                }
                else
                {
                    thongtintp = null;
                }
                ViewBag.thongtin = thongtintp;
            }

            //Lấy tất cả quyện huyện theo ID tỉnh
            //string IDtinh_thanhpho = HttpContext.Session.GetString("IDThongtinThanhPho");

            if (idthanhpho != null)
            {
                //ThongTinThanhPho thongtin = JsonConvert.DeserializeObject<ThongTinThanhPho>(IDtinh_thanhpho);

                HttpResponseMessage response1 = await _client.GetAsync($"https://online-gateway.ghn.vn/shiip/public-api/master-data/district?province_id={idthanhpho}");
                if (response1.IsSuccessStatusCode)
                {
                    var jsonData = await response1.Content.ReadAsStringAsync();

                    JObject jsonObject = JObject.Parse(jsonData);

                    // Lấy danh sách tỉnh/thành phố từ đối tượng JSON
                    JArray districtArray = (JArray)jsonObject["data"];

                    List<ThongTinQuanHuyen> thongtinquanhuyen = new List<ThongTinQuanHuyen>();
                    if (districtArray != null)
                    {
                        foreach (JObject district in districtArray)
                        {
                            if (idthanhpho == Convert.ToInt32(district["ProvinceID"]))
                            {
                                ThongTinQuanHuyen QuanHuyen = new ThongTinQuanHuyen();
                                QuanHuyen.Id = Convert.ToInt32(district["DistrictID"]);
                                QuanHuyen.Name = Convert.ToString(district["DistrictName"]);
                                thongtinquanhuyen.Add(QuanHuyen);
                            }
                        }
                    }
                    else
                    {
                        thongtinquanhuyen = null;
                    }
                    ViewBag.thongtinhuyen = thongtinquanhuyen;
                }
            }
            return View();
        }
    }
}
