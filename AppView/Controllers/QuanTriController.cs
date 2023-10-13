using AppData.model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AppView.Controllers
{

    public class QuanTriController : Controller
    {
        // GET: QuanTriController
        private readonly ILogger<QuanTriController> _logger;
        HttpClient _client = new HttpClient();
        public QuanTriController(ILogger<QuanTriController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public ActionResult GellAllChatLieu()
        {
            string url = "https://localhost:7214/api/ChatLieu/GetAll";
            var respon = _client.GetAsync(url).Result;
            var data = respon.Content.ReadAsStringAsync().Result;
            List<ChatLieu> lstchatlieu = JsonConvert.DeserializeObject<List<ChatLieu>>(data);

            return View(lstchatlieu);
        }

        [HttpGet]
        [HttpPost]
        public async Task<ActionResult> CreateChatLieu(ChatLieu chatlieu)
        {
            string url = $"https://localhost:7214/api/ChatLieu/Create?name={chatlieu.TenChatLieu}";


            var obj = JsonConvert.SerializeObject(chatlieu);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PostAsync(url, content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GellAllChatLieu", "QuanTri");
            }
            else
            {
                return View("CreateChatLieu");
            }

        }
        [HttpPut]
        public async Task<ActionResult> DeleteChatLieu(ChatLieu chatlieu)
        {
            string url = $"https://localhost:7214/api/ChatLieu/Delete/{chatlieu.Id}";
            var obj = JsonConvert.SerializeObject(chatlieu);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PutAsync(url, content);

            return RedirectToAction("GellAllChatLieu", "QuanTri");

        }

        // Size 

        [HttpGet]
        public ActionResult GellAllSize()
        {
            string url = "https://localhost:7214/api/Size/GetAll";
            var respon = _client.GetAsync(url).Result;
            var data = respon.Content.ReadAsStringAsync().Result;
            List<Size> lstsize = JsonConvert.DeserializeObject<List<Size>>(data);

            return View(lstsize);
        }

        [HttpGet]
        [HttpPost]
        public async Task<ActionResult> CreateSize(Size size)
        {
            string url = $"https://localhost:7214/api/Size/Create?size={size.SizeName}";


            var obj = JsonConvert.SerializeObject(size);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PostAsync(url, content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GellAllSize", "QuanTri");
            }
            else
            {
                return View("CreateSize");
            }

        }
        [HttpPut]
        public async Task<ActionResult> DeleteSize(Size size)
        {
            string url = $"https://localhost:7214/api/Size/Delete/{size.Id}";
            var obj = JsonConvert.SerializeObject(size);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PutAsync(url, content);

            return RedirectToAction("GellAllSize", "QuanTri");

        }


        //anhh

        [HttpGet]
        public ActionResult GellAllAnh()
        {
            string url = "https://localhost:7214/api/Anh/GetAll";
            var respon = _client.GetAsync(url).Result;
            var data = respon.Content.ReadAsStringAsync().Result;
            List<Anh> lstsize = JsonConvert.DeserializeObject<List<Anh>>(data);

            return View(lstsize);
        }

        [HttpGet]
        [HttpPost]
        public async Task<ActionResult> AddAnh(Anh anh)
        {
            string url = $"https://localhost:7214/api/Anh/Create?name={anh.Connect}";


            var obj = JsonConvert.SerializeObject(anh);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PostAsync(url, content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GellAllAnh", "QuanTri");
            }
            else
            {
                return View("AddAnh");
            }

        }

        // DanhMuc

        [HttpGet]
        public ActionResult GellAllDanhMuc()
        {
            string url = "https://localhost:7214/api/DanhMuc/GetAll";
            var respon = _client.GetAsync(url).Result;
            var data = respon.Content.ReadAsStringAsync().Result;
            List<DanhMuc> lstsize = JsonConvert.DeserializeObject<List<DanhMuc>>(data);

            return View(lstsize);
        }

        [HttpGet]
        [HttpPost]
        public async Task<ActionResult> CreateDanhMuc(DanhMuc danhmuc)
        {
            string url = $"https://localhost:7214/api/DanhMuc/Create?name={danhmuc.TenDanhMuc}";


            var obj = JsonConvert.SerializeObject(danhmuc);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PostAsync(url, content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GellAllDanhMuc", "QuanTri");
            }
            else
            {
                return View();
            }

        }
        [HttpPut]
        public async Task<ActionResult> DeleteDanhMuc(DanhMuc danhmuc)
        {
            string url = $"https://localhost:7214/api/DanhMuc/Delete?id={danhmuc.Id}";
            var obj = JsonConvert.SerializeObject(danhmuc);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PutAsync(url, content);

            return RedirectToAction("GellAllSize", "QuanTri");

        }

        // mau 

        [HttpGet]
        public ActionResult GellAllMau()
        {
            string url = "https://localhost:7214/api/MauSac/GetAll";
            var respon = _client.GetAsync(url).Result;
            var data = respon.Content.ReadAsStringAsync().Result;
            List<MauSac> lstsize = JsonConvert.DeserializeObject<List<MauSac>>(data);

            return View(lstsize);
        }

        [HttpGet]
        [HttpPost]
        public async Task<ActionResult> CreateMau(MauSac mau)
        {
            string url = $"https://localhost:7214/api/MauSac/Create?name={mau.TenMauSac}";


            var obj = JsonConvert.SerializeObject(mau);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PostAsync(url, content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GellAllMau", "QuanTri");
            }
            else
            {
                return View();
            }

        }
        [HttpPut]
        public async Task<ActionResult> DeleteMau(MauSac mau)
        {
            string url = $"https://localhost:7214/api/DanhMuc/Delete?id={mau.Id}";
            var obj = JsonConvert.SerializeObject(mau);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PutAsync(url, content);

            return RedirectToAction("GellAllSize", "QuanTri");

        }

        // thuong hieu


        [HttpGet]
        public ActionResult GellAllThuongHieu()
        {
            string url = "https://localhost:7214/api/ThuongHieu/GetAll";
            var respon = _client.GetAsync(url).Result;
            var data = respon.Content.ReadAsStringAsync().Result;
            List<ThuongHieu> lstsize = JsonConvert.DeserializeObject<List<ThuongHieu>>(data);

            return View(lstsize);
        }

        [HttpGet]
        [HttpPost]
        public async Task<ActionResult> CreateThuongHieu(ThuongHieu th)
        {
            string url = $"https://localhost:7214/api/ThuongHieu/Create?name={th.TenThuongHieu}";


            var obj = JsonConvert.SerializeObject(th);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PostAsync(url, content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GellAllThuongHieu", "QuanTri");
            }
            else
            {
                return View();
            }

        }
        [HttpPut]
        public async Task<ActionResult> DeleteThuongHieu(ThuongHieu th)
        {
            string url = $"https://localhost:7214/api/ThuongHieu/Delete/{th.Id}";
            var obj = JsonConvert.SerializeObject(th);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PutAsync(url, content);

            return RedirectToAction("GellAllThuongHieu", "QuanTri");

        }

        // Xuat xu 

        [HttpGet]
        public ActionResult GellAllXuatXu()
        {
            string url = "https://localhost:7214/api/XuatSu/GetAll";
            var respon = _client.GetAsync(url).Result;
            var data = respon.Content.ReadAsStringAsync().Result;
            List<XuatSu> lstsize = JsonConvert.DeserializeObject<List<XuatSu>>(data);

            return View(lstsize);
        }

        [HttpGet]
        [HttpPost]
        public async Task<ActionResult> CreateXuatXu(XuatSu xx)
        {
            string url = $"https://localhost:7214/api/XuatSu/Create?name={xx.TenXuatSu}";


            var obj = JsonConvert.SerializeObject(xx);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PostAsync(url, content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GellAllXuatXu", "QuanTri");
            }
            else
            {
                return View();
            }

        }
        [HttpPut]
        public async Task<ActionResult> DeleteXuatXu(XuatSu xx)
        {
            string url = $"https://localhost:7214/api/XuatSu/Delete/{xx.Id}";
            var obj = JsonConvert.SerializeObject(xx);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PutAsync(url, content);

            return RedirectToAction("GellAllXuatXu", "QuanTri");

        }

        // SanPham 

        [HttpGet]
        public ActionResult GellAllSanPham()
        {
            string url = "https://localhost:7214/api/XuatSu/GetAll";
            var respon = _client.GetAsync(url).Result;
            var data = respon.Content.ReadAsStringAsync().Result;
            List<SanPham> lstsize = JsonConvert.DeserializeObject<List<SanPham>>(data);

            return View(lstsize);
        }

        [HttpGet]
        [HttpPost]
        public async Task<ActionResult> CreateSanPham(SanPham sp)
        {
            string url = $"https://localhost:7214/api/Sanpham/Create?tensp={sp.TenSanPham}&idth={sp.IdThuongHieu}&idxx={sp.IdXuatSu}";


            var obj = JsonConvert.SerializeObject(sp);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PostAsync(url, content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GellAllSanPham", "QuanTri");
            }
            else
            {
                return View();
            }
        }
        [HttpPut]
        public async Task<ActionResult> DeleteSanPham(SanPham sp)
        {
            string url = $"https://localhost:7214/api/Sanpham/Delete/{sp.Id}";
            var obj = JsonConvert.SerializeObject(sp);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PutAsync(url, content);

            return RedirectToAction("GellAllSanPham", "QuanTri");

        }

        // san pham chi tiet

        [HttpGet]
        public ActionResult GellAllSanPhamCT()
        {
            string url = "https://localhost:7214/api/SanPhamChiTiet/GetAll";
            var respon = _client.GetAsync(url).Result;
            var data = respon.Content.ReadAsStringAsync().Result;
            List<SanPhamChiTiet> lstsize = JsonConvert.DeserializeObject<List<SanPhamChiTiet>>(data);

            return View(lstsize);
        }
        [HttpGet]
        public ActionResult GellByIDSanPhamCT(Guid id)
        {
            string url = $"https://localhost:7214/api/SanPhamChiTiet/GetByID?Id={id}";
            var respon = _client.GetAsync(url).Result;
            var data = respon.Content.ReadAsStringAsync().Result;
            SanPhamChiTiet lstsize = JsonConvert.DeserializeObject<SanPhamChiTiet>(data);

            return View(lstsize);
        }

        [HttpGet]
        [HttpPost]
        public async Task<ActionResult> CreateSanPhamCT(SanPhamChiTiet spct)
        {
            string url = $"https://localhost:7214/api/SanPhamChiTiet/Create?iddm={spct.IdDanhMuc}&idcl={spct.ChatLieu}&idms={spct.IdMauSac}&idsize={spct.IdSize}&idanh={spct.IdAnh}&idsp={spct.IDSP}&masp={spct.MaSp}&soluong={spct.SoLuong}&gia={spct.GiaBan}&mota={spct.MoTa}";


            var obj = JsonConvert.SerializeObject(spct);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PostAsync(url, content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GellAllSanPhamCT", "QuanTri");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult> UpdateSanPhamCT(SanPhamChiTiet spct)
        {
            string url = $"   https://localhost:7214/api/SanPhamChiTiet/Update?id={spct.Id}&iddm={spct.IdDanhMuc}&idcl={spct.ChatLieu}&idms={spct.IdMauSac}&idsize={spct.IdSize}&idanh={spct.IdAnh}&idsp={spct.IDSP}&masp={spct.MaSp}&soluong={spct.SoLuong}&gia={spct.GiaBan}&mota={spct.MoTa}&trangthai={spct.status}";


            var obj = JsonConvert.SerializeObject(spct);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PutAsync(url, content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GellAllSanPhamCT", "QuanTri");
            }
            else
            {
                return View();
            }
        }
     
        [HttpPut]
        public async Task<ActionResult> DeleteSanPhamCT(SanPhamChiTiet sp)
        {
            string url = $"https://localhost:7214/api/SanPhamChiTiet/Delete/{sp.Id}";
            var obj = JsonConvert.SerializeObject(sp);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PutAsync(url, content);

            return RedirectToAction("GellAllSanPhamCT", "QuanTri");

        }

    }
}
