using AppData.data;
using AppData.model;
using AppData.ViewModal.SanPhamChiTietVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration;
using Newtonsoft.Json;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using AppData.Serviece.ViewModeService;
using ZXing;
using iTextSharp.text.pdf.codec;
using QRCoder;
using ZXing.QrCode;
using Bill.Serviece.Interfaces;
using Bill.Serviece.Implements;
using System.Collections.Generic;

namespace AppView.Controllers
{

    public class QuanTriController : Controller
    {
        // GET: QuanTriController
        private readonly ILogger<QuanTriController> _logger;
        HttpClient _client = new HttpClient();
        private readonly MyDbContext _context;
        private readonly SanPhamChiTietViewModelService _spctViewModel;
        private readonly IAnhServiece anhservice;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public QuanTriController(ILogger<QuanTriController> logger, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            _spctViewModel = new SanPhamChiTietViewModelService();
            anhservice = new AnhServiece();
            _context = new MyDbContext();
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
                var error = await httpResponseMessage.Content.ReadAsStringAsync();
                if(error == "false")
                {
                    TempData["ErrorMessage"] = "Chất liệu bị trùng. Mời bạn nhập loại chất liệu khác";
                    return RedirectToAction("CreateChatLieu", "QuanTri");
                } else
                {
                    return RedirectToAction("GellAllChatLieu", "QuanTri");
                }
                
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
            List<AppData.model.Size> lstsize = JsonConvert.DeserializeObject<List<AppData.model.Size>>(data);

            return View(lstsize);
        }

        [HttpGet]
        [HttpPost]
        public async Task<ActionResult> CreateSize(AppData.model.Size size)
        {
            string url = $"https://localhost:7214/api/Size/Create?size={size.SizeName}";


            var obj = JsonConvert.SerializeObject(size);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PostAsync(url, content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var error = await httpResponseMessage.Content.ReadAsStringAsync();
                if (error == "false")
                {
                    TempData["ErrorMessage"] = "Size bị trùng mã. Mời bạn nhập size khác";
                    return RedirectToAction("CreateSize", "QuanTri");
                }
                else
                {
                    return RedirectToAction("GellAllSize", "QuanTri");
                }
            }
            else
            {
                return View("CreateSize");
            }

        }
        [HttpPut]
        public async Task<ActionResult> DeleteSize(AppData.model.Size size)
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
                var error = await httpResponseMessage.Content.ReadAsStringAsync();
                if (error == "false")
                {
                    TempData["ErrorMessage"] = "Danh mục bị trùng mã. Mời bạn nhập Danh mục khác";
                    return RedirectToAction("CreateDanhMuc", "QuanTri");
                }
                else
                {
                    return RedirectToAction("GellAllDanhMuc", "QuanTri");
                }
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
                string apiResponse = await httpResponseMessage.Content.ReadAsStringAsync();
                if (apiResponse == "false")
                {
                    TempData["ErrorMessage"] = "Màu bị trùng. Vui lòng chọn màu khác.";
                    return View();
                }
                else
                {
                    return RedirectToAction("GellAllMau", "QuanTri");
                }
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
                string apiResponse = await httpResponseMessage.Content.ReadAsStringAsync();
                if (apiResponse == "false")
                {
                    TempData["ErrorMessage"] = "Thương Hiệu bị trùng. Vui lòng chọn thương hiệu khác.";
                    return View();
                }
                else
                {
                    return RedirectToAction(" GellAllThuongHieu", "QuanTri");
                }

            } else
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
                var apiresponse = await httpResponseMessage.Content.ReadAsStringAsync();
                if(apiresponse == "false")
                {
                    TempData["ErrorMessage"] = "Xuất sứ bị trùng. Vui lòng chọn xuất sứ khác.";
                    return RedirectToAction("CreateXuatXu");
                } else
                {
                    return RedirectToAction("GellAllXuatXu", "QuanTri");
                }
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
        public async Task<ActionResult> GellAllSanPham()
        {
            string url = "https://localhost:7214/api/SanPhamChiTiet/GetAllSanPhamViewModel";
            var respon = await _client.GetAsync(url);
            var data = await respon.Content.ReadAsStringAsync();
            List<SanPhamViewModel> lstsize = JsonConvert.DeserializeObject<List<SanPhamViewModel>>(data);

            return View(lstsize);
        }

        [HttpGet]
        public async Task<ActionResult> CreateSanPham()
        {
            ViewBag.thuonghieu = new SelectList(_context.thuongHieus.ToList().Where(c => c.Status == 1).OrderBy(c => c.TenThuongHieu), "Id", "TenThuongHieu");
            ViewBag.Xuatsu = new SelectList(_context.xuatSus.ToList().Where(c => c.Status == 1).OrderBy(c => c.TenXuatSu), "Id", "TenXuatSu");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateSanPham(SanPham sp)
        {
            string url = $"https://localhost:7214/api/Sanpham/Create?tensp={sp.TenSanPham}&idth={sp.IdThuongHieu}&idxx={sp.IdXuatSu}";
            var obj = JsonConvert.SerializeObject(sp);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PostAsync(url, content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var error = await httpResponseMessage.Content.ReadAsStringAsync();
                if(error == "false")
                {
                    TempData["ErrorMessage"] = "Sản phẩm bị trùng. Vui lòng chọn sản phẩm khác.";
                    return RedirectToAction("CreateSanPham");
                } else
                {
                    return RedirectToAction("GellAllSanPham", "QuanTri");
                }
            }
            else
            {
                return RedirectToAction("CreateSanPham");
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
        public ActionResult GellAllSanPhamCT(string name)
        {
            if (name == null || name == "")
            {
                string url = "https://localhost:7214/api/SanPhamChiTiet/GetAllSanphamchitietViewModel";
                var respon = _client.GetAsync(url).Result;
                var datalist = respon.Content.ReadAsStringAsync().Result;
                List<SanPhamChiTietViewModel> list = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(datalist);
                //foreach (var product in lstsize)
                //{
                //    product.QRCode = GenerateQRCode(product.Id);
                //}
                return View(list);
            }
            else
            {
             
                string urltimliem = $"https://localhost:7214/api/SanPhamChiTiet/GetByNameSPCTVM?name={name}";
                var respon1 = _client.GetAsync(urltimliem).Result;
                var data = respon1.Content.ReadAsStringAsync().Result;
                List<SanPhamChiTietViewModel> listbyname = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(data);
                return View(listbyname);
            }
        }

        // tạo QrCode cho SanphamCTViewModel 
        //public IActionResult GenerateBarcode(Guid Id)
        //{
        //    // Tìm sản phẩm trong cơ sở dữ liệu bằng productId
        //    SanPhamChiTietViewModel product = _spctViewModel.GetById(Id);

        //    if (product != null)
        //    {
        //        // Tạo barcode cho sản phẩm
        //        BarcodeWriter<Bitmap> barcodeWriter = new BarcodeWriter<Bitmap>();
        //        barcodeWriter.Format = BarcodeFormat.QR_CODE; // Chọn định dạng barcode
        //        var barcodeBitmap = barcodeWriter.Write(product);

        //        // Lưu hình ảnh barcode vào thư mục hoặc cơ sở dữ liệu

        //        // Trả về hình ảnh barcode dưới dạng FileResult
        //        return File(ImageToByte2D(barcodeBitmap), "image/png");
        //    }

        //    // Xử lý trường hợp sản phẩm không tồn tại
        //    return NotFound();
        //}
        //public static byte[] ImageToByte2D(Bitmap img)
        //{
        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        img.Save(stream, ImageFormat.Png);
        //        return stream.ToArray();
        //    }
        //}

        //public string GenerateQRCode(Guid id)
        //{
        //    // Truy xuất thông tin đối tượng dựa trên ID
        //    SanPhamChiTietViewModel spct = _spctViewModel.GetById(id);
        //    var infor = $"Mã Sản phẩm: {spct.MaSp} Tên: {spct.TenSP} Màu sắc : {spct.MauSac} Chất liệu: {spct.ChatLieu} Size : {spct.Size}";
        //    if (spct != null)
        //    {
        //        // Tạo mã QR từ thông tin đối tượng
        //        BarcodeWriter<Bitmap> qrCodeWriter = new BarcodeWriter<Bitmap>();
        //        qrCodeWriter.Format = BarcodeFormat.QR_CODE;
        //        qrCodeWriter.Options = new QrCodeEncodingOptions
        //        {
        //            DisableECI = true,
        //            CharacterSet = "UTF-8",
        //            Width = 200,
        //            Height = 200,
        //        };

        //        var qrCodeBitmap = qrCodeWriter.Write(infor);

        //        // Chuyển đổi mã QR thành một dạng hiển thị trên giao diện
        //        using (var bitmap = new Bitmap(qrCodeBitmap))
        //        {
        //            var byteArray = BitmapToByteArray(bitmap);
        //            var base64String = Convert.ToBase64String(byteArray);

        //            // Trả về mã QR dưới dạng hình ảnh hoặc sử dụng nó trong giao diện
        //            return$"<img src='data:image/png;base64,{base64String}' />";
        //        }
        //    }

        //    return "Không tìm thấy đối tượng.";
        //}
        //private byte[] BitmapToByteArray(Bitmap bitmap)
        //{
        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        bitmap.Save(stream, ImageFormat.Png);
        //        return stream.ToArray();
        //    }
        //}
        // Kết thúc 

        [HttpGet]
        public ActionResult GellByIDSanPhamCT(Guid id)
        {
            string url = $"https://localhost:7214/api/SanPhamChiTiet/GetByIDSPCTVM?id={id}";
            var respon = _client.GetAsync(url).Result;
            var data = respon.Content.ReadAsStringAsync().Result;
            SanPhamChiTietViewModel lstsize = JsonConvert.DeserializeObject<SanPhamChiTietViewModel>(data);

            return View(lstsize);
        }
        [HttpGet]
        public ActionResult GellByNameSanPhamCT(string name)
        {
            string url = $"https://localhost:7214/api/SanPhamChiTiet/GetByNameSPCTVM?name={name}";
            var respon = _client.GetAsync(url).Result;
            var data = respon.Content.ReadAsStringAsync().Result;
            SanPhamChiTietViewModel lstsize = JsonConvert.DeserializeObject<SanPhamChiTietViewModel>(data);

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
                var error = await httpResponseMessage.Content.ReadAsStringAsync();
                if(error == "Sản phẩm bị trùng mã")
                {
                    TempData["ErrorMessage"] = "Sản phẩm bị trùng mã. Mời bạn nhập mã khác";
                    return RedirectToAction("CreateSanPhamCT", "QuanTri");

                } else if (error == "Sản phẩm đã tồn tại")
                {
                    TempData["ErrorMessage"] = "Sản phẩm bạn nhập đã có trong danh sách sản phẩm.";
                    return RedirectToAction("CreateSanPhamCT", "QuanTri");
                } else
                {
                    return RedirectToAction("GellAllSanPhamCT", "QuanTri");
                }
            }
            else
            {
                ViewBag.DanhMuc = new SelectList(_context.danhMucs.ToList().Where(c => c.status == 1).OrderBy(c => c.TenDanhMuc), "Id", "TenDanhMuc");
                ViewBag.SanPham = new SelectList(_context.sanPhams.ToList().Where(c => c.status == 1).OrderBy(c => c.TenSanPham), "Id", "TenSanPham");
                ViewBag.ChatLieu = new SelectList(_context.chatLieus.ToList().Where(c => c.status == 1).OrderBy(c => c.TenChatLieu), "Id", "TenChatLieu");
                ViewBag.MauSac = new SelectList(_context.mauSacs.ToList().Where(c => c.status == 1).OrderBy(c => c.TenMauSac), "Id", "TenMauSac");
                ViewBag.Size = new SelectList(_context.sizes.ToList().Where(c => c.status == 1).OrderBy(c => c.SizeName), "Id", "SizeName");
                //ViewBag.Anh = new SelectList(_context.anhs.ToList(), "Id", "Connect");
                string urlanh = "https://localhost:7214/api/Anh/GetAll";
                var respon = _client.GetAsync(urlanh).Result;
                var data = respon.Content.ReadAsStringAsync().Result;
                List<Anh> album = JsonConvert.DeserializeObject<List<Anh>>(data);
                ImageUploadModel img = new ImageUploadModel();
                ViewBag.upload = img.ImageFile;
                ViewBag.listanh = album;
                return View();
            }
        }
        [HttpGet]
        public ActionResult UpdateSanPhamCT(Guid id)
        {
            string url = $"https://localhost:7214/api/SanPhamChiTiet/GetByIDSPCTVM?id={id}";
            var respon = _client.GetAsync(url).Result;
            var data = respon.Content.ReadAsStringAsync().Result;
            SanPhamChiTietViewModel lstsize = JsonConvert.DeserializeObject<SanPhamChiTietViewModel>(data);
            return View(lstsize);
        }
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
        [HttpGet]
        public IActionResult UploadImage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(ImageUploadModel model)
        {
            if (model.ImageFile != null)
            {
                // Xử lý tệp ảnh ở đây
                if (model.ImageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                    var uniqueFileName = model.ImageFile.FileName;

                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(fileStream);
                    }

                    // Lưu đường dẫn vào thuộc tính Path của đối tượng Anh
                    var anh = new Anh
                    {
                        Id = Guid.NewGuid(),
                        Connect = filePath,
                        status = 1 // Có thể đặt trạng thái khác tùy ý
                    };


                    anhservice.Add(anh);

                    return RedirectToAction("UploadImage");
                }
            }

            return View();
        }
        public IActionResult DisplayImage(string imagePath)
        {
            // Kiểm tra xem đường dẫn có tồn tại không
            if (System.IO.File.Exists(imagePath))
            {
                byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
                return File(imageBytes, "image/jpeg"); // Thay đổi kiểu hình ảnh tùy theo định dạng thực tế
            }
            else
            {
                // Trả về một hình ảnh mặc định hoặc thông báo lỗi
                return NotFound();
            }
        }

    }
}
