using AppData.data;
using AppData.model;
using AppData.Serviece.Implements;
using AppData.Serviece.Interfaces;
using AppData.Session;
using AppData.ViewModal.GioHangChiTietViewModel;
using AppData.ViewModal.SanPhamChiTietVM;
using AppData.ViewModal.Usermodalview;
using AppData.ViewModal.VoucherVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Policy;
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
        public Guid IDNguoiDung()
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;

            var userlogin = HttpContext.User;
            var email = userlogin.FindFirstValue(ClaimTypes.Email);
            var user = _context.Users.FirstOrDefault(c => c.Email == email);
            return user.Id;
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
            var idnguoidung = IDNguoiDung();
            string url = $"https://localhost:7214/api/GioHangCT/GetAllFullGioHangChiTiet?IDnguoiDung={idnguoidung}";
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
        // Thanh toán

        // Lấy hết thành phố tỉnh
        [HttpPost]
        public List<ThongTinThanhPho> GetAllTinh()
        {
            string token = "be8ee160-008a-11ee-a281-3aa62a37e0a5";
            _client.DefaultRequestHeaders.Add("token", token);
            List<ThongTinThanhPho> thongtintp = new List<ThongTinThanhPho>();
            var urlTinhThanhPho = "https://online-gateway.ghn.vn/shiip/public-api/master-data/province";
            HttpResponseMessage response = _client.GetAsync(urlTinhThanhPho).Result;
            // lấy ID tỉnh
            if (response.IsSuccessStatusCode)
            {
                var provinces = response.Content.ReadAsStringAsync().Result; ;

                JObject jsonObject = JObject.Parse(provinces);

                // Lấy danh sách tỉnh/thành phố từ đối tượng JSON
                JArray provinceArray = (JArray)jsonObject["data"];


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
                ViewBag.thongtin = thongtintp;
            }
            HttpContext.Session.Set("ThanhPho", thongtintp);
            return thongtintp;
        }


        //[HttpGet]
        ////Lấy hết quận huyện
        //public List<ThongTinQuanHuyen> GetQuanHuyen(int? idtp)
        //{

        //    return thongtinquanhuyen;
        //}
        // Lấy Id thành phố tỉnh 
        [HttpPost]
        public IActionResult NhanIDTinh(int IdThanhPho)
        {
            List<ThongTinQuanHuyen> thongtinquanhuyen = new List<ThongTinQuanHuyen>();
            string token = "be8ee160-008a-11ee-a281-3aa62a37e0a5";
            _client.DefaultRequestHeaders.Add("token", token);
            string url = $"https://online-gateway.ghn.vn/shiip/public-api/master-data/district?province_id={IdThanhPho}";
            HttpResponseMessage response1 = _client.GetAsync(url).Result;
            if (response1.IsSuccessStatusCode)
            {
                var districts = response1.Content.ReadAsStringAsync().Result;

                JObject jsonObject1 = JObject.Parse(districts);

                // Lấy danh sách tỉnh/thành phố từ đối tượng JSON
                JArray districtArray = (JArray)jsonObject1["data"];


                if (districtArray != null)
                {
                    foreach (JObject district in districtArray)
                    {
                        ThongTinQuanHuyen QuanHuyen = new ThongTinQuanHuyen();
                        QuanHuyen.ID = Convert.ToInt32(district["DistrictID"]);
                        QuanHuyen.Name = Convert.ToString(district["DistrictName"]);
                        thongtinquanhuyen.Add(QuanHuyen);
                    }
                }
            }

            HttpContext.Session.Set("Thongtinquanhuyen", thongtinquanhuyen);

            HttpContext.Session.Remove("IdThanhPho");
            HttpContext.Session.SetInt32("IdThanhPho", IdThanhPho);
            return Json(thongtinquanhuyen);
        }
        [HttpGet]
        public IActionResult LayQuanHuyen()
        {
            var thongTinQuanHuyen = HttpContext.Session.GetString("Thongtinquanhuyen");

            // Trả về dữ liệu dưới dạng JSON
            return Json(new { thongTinQuanHuyen });
        }
        // lấy ID quận huyện
        //[HttpPost]
        //public void NhanIDHuyen(int IdQuanHuyen)
        //{
        //    HttpContext.Session.Remove("IdQuanHuyen");
        //    HttpContext.Session.SetInt32("IdQuanHuyen", IdQuanHuyen);

        //}
        [HttpPost]
        public IActionResult NhanIDHuyen(int IdQuanHuyen)
        {
            List<XaPhuong> lstxaphuong = new List<XaPhuong>();
            string token = "be8ee160-008a-11ee-a281-3aa62a37e0a5";
            _client.DefaultRequestHeaders.Add("token", token);
            string url = $"https://online-gateway.ghn.vn/shiip/public-api/master-data/ward?district_id={IdQuanHuyen}";
            HttpResponseMessage response1 = _client.GetAsync(url).Result;
            if (response1.IsSuccessStatusCode)
            {
                var districts = response1.Content.ReadAsStringAsync().Result;

                JObject jsonObject1 = JObject.Parse(districts);

                // Lấy danh sách tỉnh/thành phố từ đối tượng JSON
                JArray WardCodeArray = (JArray)jsonObject1["data"];


                if (WardCodeArray != null)
                {
                    foreach (JObject Ward in WardCodeArray)
                    {
                        XaPhuong xaphuong = new XaPhuong();
                        xaphuong.WardCode = Convert.ToString(Ward["WardCode"]);
                        xaphuong.WardName = Convert.ToString(Ward["WardName"]);
                        lstxaphuong.Add(xaphuong);
                    }
                }
            }

            HttpContext.Session.Set("XaPhuong", lstxaphuong);

            HttpContext.Session.Remove("IDQuanHuyen");
            HttpContext.Session.SetInt32("IDQuanHuyen", IdQuanHuyen);
            return Json(lstxaphuong);
        }
        [HttpGet]
        public IActionResult GetAllXaPhuong()
        {
            var ThongTinXaPhuong = HttpContext.Session.GetString("XaPhuong");

            // Trả về dữ liệu dưới dạng JSON
            return Json(new { ThongTinXaPhuong });
        }
        // thanh toán
        [HttpGet]
        public ActionResult ThanhToan()
        {
            List<ThongTinThanhPho> laytinh = GetAllTinh();
            var idtp = HttpContext.Session.GetInt32("IdThanhPho");
            var Idquanhuyen = HttpContext.Session.GetInt32("IdQuanHuyen");

            var idnguoidung = IDNguoiDung();
            string urlvoucherDetailByID = $"https://localhost:7214/api/VoucherDetail/GetlistVoucherViewModelByIdNguoiDung?idnguoidung={idnguoidung}";
            var reposVocuher = _client.GetAsync(urlvoucherDetailByID).Result;
            var data1 = reposVocuher.Content.ReadAsStringAsync().Result;
            List<VoucherDetailHoanThien> lstvoucher = JsonConvert.DeserializeObject<List<VoucherDetailHoanThien>>(data1);
            ViewBag.LstVoucher = lstvoucher;

            string url = $"https://localhost:7214/api/GioHangCT/GetAllFullGioHangChiTiet?IDnguoiDung={idnguoidung}";
            var repos = _client.GetAsync(url).Result;
            var data = repos.Content.ReadAsStringAsync().Result;
            List<GioHangChiTietViewModel> lstghct = JsonConvert.DeserializeObject<List<GioHangChiTietViewModel>>(data);
            ViewBag.GioHang = lstghct;
            return View();
        }
        [HttpPost]
        public IActionResult ThanhToan(HoaDon hd)
        {
            VoucherDetailHoanThien vouchersd = null;
            Guid Idnguoidung = IDNguoiDung();
            var obj = JsonConvert.SerializeObject(hd);
            var tienship = HttpContext.Session.GetInt32("TienShip");

            byte[] serializedData = HttpContext.Session.Get("Voucher");

            if (serializedData != null)
            {
                string json = Encoding.UTF8.GetString(serializedData);
                vouchersd = JsonConvert.DeserializeObject<VoucherDetailHoanThien>(json);
            }
            if (vouchersd == null)
            {
                string urlnotVoucher = $"https://localhost:7214/api/HoaDon/CreateHoaDon?nguoinhan={hd.NguoiNhan}&sdt={hd.SDT}&quanhuyen={hd.QuanHuyen}&tinh={hd.Tinh}&diachi={hd.DiaChi}&ngaythanhtoan={hd.NgayThanhToan}&ghichu={hd.GhiChu}&idnguoidung={Idnguoidung}&idhttt={hd.IDHTTT}&tienship={tienship}";

                StringContent content = new StringContent(urlnotVoucher, Encoding.UTF8, "application/json");
                HttpResponseMessage message = _client.PostAsync(obj, content).Result;
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAllHDById", "HoaDon");
                }
            }
            else
            {
                ViewBag.GiaTriVoucher = vouchersd != null ? Convert.ToInt32(vouchersd.GiaTriVoucher) : 0;
                string urlhaveVoucher = $"https://localhost:7214/api/HoaDon/CreateHoaDon?nguoinhan={hd.NguoiNhan}&sdt={hd.SDT}&quanhuyen={hd.QuanHuyen}&tinh={hd.Tinh}&diachi={hd.DiaChi}&ngaythanhtoan={hd.NgayThanhToan}&ghichu={hd.GhiChu}&idnguoidung={Idnguoidung}&idvoucherdetail={vouchersd.IDVoucherDetail}&idhttt={hd.IDHTTT}&tienship={tienship}";
                StringContent content1 = new StringContent(urlhaveVoucher, Encoding.UTF8, "application/json");
                HttpResponseMessage message1 = _client.PostAsync(obj, content1).Result;
                if (message1.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAllHDById", "HoaDon");
                }
            }
            return RedirectToAction("ThanhToan", "GioHang");

        }
        //Tính tiền ship
        [HttpPost]
        [HttpGet]
        public IActionResult TinhTienShip(int WardCode)
        {
            var tienship = 0;
            var tongtien = 0;
            var IdQuanHuyen = HttpContext.Session.GetInt32("IDQuanHuyen");
            List<DichVuChuyenPhat> lstdichvu = new List<DichVuChuyenPhat>();
            // ChonDichVụ
            string token = "be8ee160-008a-11ee-a281-3aa62a37e0a5";
            _client.DefaultRequestHeaders.Add("token", token);
            string urldichvu = $"https://online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/available-services?shop_id=3689187&from_district=3440&to_district={IdQuanHuyen}";
            HttpResponseMessage response1 = _client.GetAsync(urldichvu).Result;
            if (response1.IsSuccessStatusCode)
            {
                var Services = response1.Content.ReadAsStringAsync().Result;

                JObject jsonObject = JObject.Parse(Services);

                // Lấy danh sách tỉnh/thành phố từ đối tượng JSON
                JArray ServicesArray = (JArray)jsonObject["data"];
                if (ServicesArray != null)
                {
                    foreach (JObject service in ServicesArray)
                    {
                        DichVuChuyenPhat dichvucchuyenphat = new DichVuChuyenPhat();
                        dichvucchuyenphat.service_id = Convert.ToInt32(service["service_id"]);
                        dichvucchuyenphat.Name = Convert.ToString(service["short_name"]);
                        lstdichvu.Add(dichvucchuyenphat);
                    }
                }
            }

            int idservice = lstdichvu.FirstOrDefault(c => c.Name == "Chuyển phát thương mại điện tử").service_id;

            string urltinhphi = $"https://online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/fee?service_id={idservice}&insurance_value={tongtien}&from_district_id=3440&to_district_id={IdQuanHuyen}&to_ward_code={WardCode}&height=20&length=20&weight=2000&width=20";
            HttpResponseMessage httpResponseMessage = _client.GetAsync(urltinhphi).Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var ship = httpResponseMessage.Content.ReadAsStringAsync().Result;

                JObject ships = JObject.Parse(ship);


                JToken dataToken = ships["data"];
                if (dataToken is JArray)
                {
                    JArray districtArray = (JArray)dataToken;
                    // Tiếp tục xử lý với districtArray
                }
                else if (dataToken is JObject)
                {
                    JObject districtObject = (JObject)dataToken;
                    // Tiếp tục xử lý nếu cần thiết
                    tienship = Convert.ToInt32(districtObject["total"]);
                }
            }
            HttpContext.Session.Remove("TienShip");
            HttpContext.Session.SetInt32("TienShip", tienship);
            return Json(tienship);
        }
        [HttpGet]
        public IActionResult HienThiTienShip()
        {
            var tienship = HttpContext.Session.GetInt32("TienShip");
            return Json(new { tienship });
        }
        [HttpPost]
        public IActionResult NhanMaVoucher([FromBody] string Ma)
        {
            try
            {
                var urlVoucherDetail = $"https://localhost:7214/api/VoucherDetail/GetListVoucherViewModelByName?MaVoucher={Ma}";
                var repos = _client.GetAsync(urlVoucherDetail).Result;
                var data = repos.Content.ReadAsStringAsync().Result;
                VoucherDetailHoanThien voucherhoanthien = JsonConvert.DeserializeObject<VoucherDetailHoanThien>(data);
                if (voucherhoanthien != null)
                {
                    HttpContext.Session.Set("Voucher", voucherhoanthien);
                    return Json(voucherhoanthien.GiaTriVoucher);
                }
                else
                {
                    return Json(0);
                }
            }
            catch
            {
                return Json(0);
            }

        }
        [HttpGet]
        public IActionResult NhanMaVoucher()
        {
            VoucherDetailHoanThien vouchersd = null;
            int giatrivoucher = 0;
            byte[] serializedData = HttpContext.Session.Get("Voucher");
            if (serializedData != null)
            {
                string json = Encoding.UTF8.GetString(serializedData);
                vouchersd = JsonConvert.DeserializeObject<VoucherDetailHoanThien>(json);
                giatrivoucher = Convert.ToInt32(vouchersd.GiaTriVoucher);
            }
            return Json(new { giatrivoucher });
        }
    }
}

