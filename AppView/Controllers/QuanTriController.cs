﻿using AppData.data;
using AppData.model;
using AppData.ViewModal.SanPhamChiTietVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using AppData.Serviece.ViewModeService;
using Bill.Serviece.Interfaces;
using Bill.Serviece.Implements;
using System.Collections.Generic;
using OfficeOpenXml;
using System.ComponentModel;
using LicenseContext = OfficeOpenXml.LicenseContext;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using AppData.Serviece.Interfaces;
using AppData.Serviece.Implements;
using X.PagedList;
using Microsoft.VisualStudio.Web.CodeGeneration;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ZXing;
using ZXing.QrCode;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Components.RenderTree;
using ZXing.Rendering;
using ZXing.Common;
using System.Collections;
using System.Security.Claims;

namespace AppView.Controllers
{

    public class QuanTriController : Controller
    {
        // GET: QuanTriController
        private readonly ILogger<QuanTriController> _logger;
        HttpClient _client = new HttpClient();
        private readonly MyDbContext _context;
        private readonly ISanPhamServiece _sanphamservice;
        private readonly IGioHangService _giohangservice;
        private readonly ISanPhamChiTietServiece _sanphamchitietservice;
        private readonly IDanhMucServiece _danhmucservice;
        private readonly IMauSacServiece _mausacservice;
        private readonly IAnhSanPhamService _ianhsanpham;
        private readonly ISizeServiece sizeServiece;
        private readonly IThuongHieuServiece _thuonghieuservice;
        private readonly IChatLieuServiece _chatlieuservice;
        private readonly SanPhamChiTietViewModelService _spctViewModel;
        private readonly IAnhServiece anhservice;
        private readonly SanPhamViewModelService sanphanviewmodel;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private static List<SanPhamChiTiet> _tempProducts;
        private static List<string> _tempAnh;
        public static List<SanPhamChiTietViewModel> _temspctvm;
        public readonly List<Anh> anhSanPhams = new List<Anh>();
        private readonly List<string> path = new List<string>();
        public QuanTriController(ILogger<QuanTriController> logger, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            _sanphamchitietservice = new SanPhamChiTietServiece();
            _mausacservice = new MauSacServiece();
            _ianhsanpham = new AnhSanPhamService();
            _giohangservice = new GioHangService();
            sizeServiece = new SizeServiece();
            sanphanviewmodel = new SanPhamViewModelService();
            _chatlieuservice = new ChatLieuServiece();
            _danhmucservice = new DanhMucServiece();
            _thuonghieuservice = new ThuongHieuServiece();
            _sanphamservice = new SanPhamServiece();
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
                if (error == "false")
                {
                    TempData["ErrorMessage"] = "Chất liệu bị trùng. Mời bạn nhập loại chất liệu khác";
                    return RedirectToAction("CreateChatLieu", "QuanTri");
                }
                else
                {
                    return RedirectToAction("GellAllChatLieu");
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
                    return RedirectToAction("GellAllThuongHieu");
                }

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
        public ActionResult GellAllXuatsu()
        {
            string url = "https://localhost:7214/api/XuatSu/GetAll";
            var respon = _client.GetAsync(url).Result;
            var data = respon.Content.ReadAsStringAsync().Result;
            List<XuatSu> lstsize = JsonConvert.DeserializeObject<List<XuatSu>>(data);
            return View(lstsize);
        }

        [HttpGet]
        [HttpPost]
        public ActionResult CreateXuatsu(XuatSu xs)
        {
            string url = $"https://localhost:7214/api/XuatSu/Create?name={xs.TenXuatSu}";

            var obj = JsonConvert.SerializeObject(xs);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = _client.PostAsync(url, content).Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var apiresponse = httpResponseMessage.Content.ReadAsStringAsync().Result;
                if (apiresponse == "false")
                {
                    TempData["ErrorMessage"] = "Xuất sứ bị trùng. Vui lòng chọn xuất sứ khác.";
                    return View();
                }
                else
                {
                    return RedirectToAction("GellAllXuatsu", "QuanTri");
                }
            }
            else
            {
                return View();
            }

        }
        [HttpPut]
        public async Task<ActionResult> DeleteXuatsu(XuatSu xx)
        {
            string url = $"https://localhost:7214/api/XuatSu/Delete/{xx.Id}";
            var obj = JsonConvert.SerializeObject(xx);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PutAsync(url, content);

            return RedirectToAction("GellAllXuatsu", "QuanTri");

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
                if (error == "false")
                {
                    TempData["ErrorMessage"] = "Sản phẩm bị trùng. Vui lòng chọn sản phẩm khác.";
                    return RedirectToAction("CreateSanPham");
                }
                else
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

        [HttpGet]
        public ActionResult GellAllSanPhamCT(int? page, string name, string danhMucFilter, string chatLieuFilter, string ThuongHieuFilter, string trangthaiFilter)
        {
            // phân trang 

            int pageNumber = page ?? 1;
            int pagesize = 5;
            // list danh sach

            string url = "https://localhost:7214/api/SanPhamChiTiet/GetAllSanphamchitietViewModel";
            var respon = _client.GetAsync(url).Result;
            var datalist = respon.Content.ReadAsStringAsync().Result;
            List<SanPhamChiTietViewModel> list = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(datalist);

            foreach (var item in list)
            {
                string infor = "Mã Sản phẩm :" + item.MaSp + " Tên: " + item.TenSP + " Màu sắc :" + item.MauSac + "Chất liệu: " + item.ChatLieu + " Size :" + item.Size;

                var qrCodeWriter = new ZXing.BarcodeWriterPixelData
                {
                    Format = ZXing.BarcodeFormat.QR_CODE,
                    Options = new QrCodeEncodingOptions
                    {
                        Height = 100,
                        Width = 100,
                        Margin = 0
                    }
                };
                var pixelData = qrCodeWriter.Write(infor);

                // creating a bitmap from the raw pixel data; if only black and white colors are used it makes no difference   
                // that the pixel data ist BGRA oriented and the bitmap is initialized with RGB   
                using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
                {
                    using (var ms = new MemoryStream())
                    {
                        var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                        try
                        {
                            // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image   
                            System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                        }
                        finally
                        {
                            bitmap.UnlockBits(bitmapData);
                        }
                        //string fileGuid = Guid.NewGuid().ToString().Substring(0, 4);
                        //bitmap.Save("wwwroot/qrr/" + fileGuid + ".png", System.Drawing.Imaging.ImageFormat.Png);
                        // save to stream as PNG   
                        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        item.QRCode = ms.ToArray();
                    }
                }
            }
            // view anh

            //View thương hiệu lên TABLE
            // lọc theo danh muc
            List<string> tendanhmuc = new List<string>();
            foreach (var item in _context.danhMucs.ToList())
            {
                tendanhmuc.Add(item.TenDanhMuc.ToString());
            }
            var danhmuclist = tendanhmuc;
            ViewBag.DanhMucList = new SelectList(danhmuclist);

            // lọc theo Chất liệu

            List<string> tenchatlieu = new List<string>();
            foreach (var chatlieu in _context.chatLieus.ToList())
            {
                tenchatlieu.Add(chatlieu.TenChatLieu);
            }
            var chatlieulist = tenchatlieu;
            ViewBag.ChatlieuList = new SelectList(chatlieulist);

            //Lọc trạng thái 
            //Lọc theo thương hiệu
            List<string> tenthuonghieu = new List<string>();
            foreach (var thuonghieu in _context.thuongHieus.ToList())
            {
                tenthuonghieu.Add(thuonghieu.TenThuongHieu);
            }
            var thuonghieulist = tenthuonghieu;
            ViewBag.ThuongHieuList = new SelectList(thuonghieulist);
            List<string> sanphammangthuonghieu = new List<string>();
            List<SanPhamChiTietViewModel> sanphamchitiet = new List<SanPhamChiTietViewModel>();
            if (!string.IsNullOrEmpty(ThuongHieuFilter))
            {
                var sanpham = sanphanviewmodel.GetAllSanPham().Where(c => c.ThuongHieu == ThuongHieuFilter).ToList();
                foreach (var sp in sanpham)
                {
                    sanphammangthuonghieu.Add(sp.TenSanPham);
                }
                if (!string.IsNullOrEmpty(chatLieuFilter) && !string.IsNullOrEmpty(danhMucFilter))
                {
                    foreach (var spct in sanphammangthuonghieu)
                    {
                        var spct1 = _spctViewModel.GetAll().FirstOrDefault(c => c.TenSP == spct && c.DanhMuc == danhMucFilter && c.ChatLieu == chatLieuFilter);
                        if (spct1 == null)
                        {
                            //var spctvm = _spctViewModel.GetAll().FirstOrDefault(c => c.ChatLieu == chatLieuFilter && c.DanhMuc == danhMucFilter);
                            //var products = productQuery.ToList();
                            //if (products.Count == 0)
                            //{
                            foreach (var spct2 in sanphammangthuonghieu)
                            {
                                var spct3 = _spctViewModel.GetAll().FirstOrDefault(c => c.TenSP == spct && c.DanhMuc == danhMucFilter);
                                sanphamchitiet.Add(spct3);
                            }
                            foreach (var spct4 in sanphammangthuonghieu)
                            {
                                var spct5 = _spctViewModel.GetAll().FirstOrDefault(c => c.TenSP == spct && c.ChatLieu == chatLieuFilter);
                                sanphamchitiet.Add(spct5);
                            }
                            var botrungsp = sanphamchitiet.Where(c => c.DanhMuc == danhMucFilter && c.ChatLieu == chatLieuFilter).OrderByDescending(c => c.MaSp).Distinct().ToList();

                            var products1 = botrungsp;
                            var model1 = new SanPhamChiTietViewModel
                            {
                                Products = products1.ToPagedList(pageNumber, pagesize)
                            };
                            return View(model1);
                            //}
                            //return View(products);
                        }
                        else
                        {
                            sanphamchitiet.Add(spct1);
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(danhMucFilter))
                {
                    foreach (var spct in sanphammangthuonghieu)
                    {
                        var spct1 = _spctViewModel.GetAll().FirstOrDefault(c => c.TenSP == spct && c.DanhMuc == danhMucFilter);
                        sanphamchitiet.Add(spct1);
                    }
                }
                else if (!string.IsNullOrEmpty(chatLieuFilter))
                {
                    foreach (var spct in sanphammangthuonghieu)
                    {
                        var spct1 = _spctViewModel.GetAll().FirstOrDefault(c => c.TenSP == spct && c.ChatLieu == chatLieuFilter);
                        sanphamchitiet.Add(spct1);
                    }
                }
                else
                {
                    foreach (var spct in sanphammangthuonghieu)
                    {
                        var spct1 = _spctViewModel.GetAll().FirstOrDefault(c => c.TenSP == spct);
                        sanphamchitiet.Add(spct1);
                    }
                }
                //productQuery = productQuery.Where(c => c.ChatLieu == chatLieuFilter).ToList();
                //var products = productQuery.ToList();
                var products = sanphamchitiet.OrderByDescending(c => c.MaSp);
                var model = new SanPhamChiTietViewModel
                {
                    Products = products.ToPagedList(pageNumber, pagesize)
                };
                return View(model);
            } // else tiếp 
            else if (!string.IsNullOrEmpty(chatLieuFilter) && !string.IsNullOrEmpty(danhMucFilter))
            {
                var item = _spctViewModel.GetAll().Where(c => c.ChatLieu == chatLieuFilter && c.DanhMuc == danhMucFilter).OrderByDescending(c => c.MaSp).ToList();
                var products = item;
                var model = new SanPhamChiTietViewModel
                {
                    Products = products.ToPagedList(pageNumber, pagesize)
                };
                return View(model);
            }
            else if (!string.IsNullOrEmpty(danhMucFilter))
            {
                var spct1 = _spctViewModel.GetAll().Where(c => c.DanhMuc == danhMucFilter).OrderByDescending(c => c.MaSp).ToList();
                var products = spct1;
                var model = new SanPhamChiTietViewModel
                {
                    Products = products.ToPagedList(pageNumber, pagesize)
                };
                return View(model);
            }
            else if (!string.IsNullOrEmpty(chatLieuFilter))
            {
                var spct1 = _spctViewModel.GetAll().Where(c => c.ChatLieu == chatLieuFilter).OrderByDescending(c => c.MaSp).ToList();
                var products = spct1;
                var model = new SanPhamChiTietViewModel
                {
                    Products = products.ToPagedList(pageNumber, pagesize)
                };
                return View(model);
            }
            else if (trangthaiFilter != null)
            {
                if (Convert.ToInt32(trangthaiFilter) == 0)
                {
                    sanphamchitiet = _spctViewModel.GetAll().Where(c => c.status == 0).OrderByDescending(c => c.MaSp).ToList();
                }
                else if (Convert.ToInt32(trangthaiFilter) == 1)
                {
                    sanphamchitiet = _spctViewModel.GetAll().Where(c => c.status == 1).OrderByDescending(c => c.MaSp).ToList();
                }
                var products = sanphamchitiet;
                var model = new SanPhamChiTietViewModel
                {
                    Products = products.ToPagedList(pageNumber, pagesize)
                };
                return View(model);
            }
            else if (name == null || name == "")
            {
                var products = list;
                var model = new SanPhamChiTietViewModel
                {
                    Products = products.ToPagedList(pageNumber, pagesize)
                };
                return View(model);
            }
            else
            {

                string urltimliem = $"https://localhost:7214/api/SanPhamChiTiet/GetByNameSPCTVM?name={name}";
                var respon1 = _client.GetAsync(urltimliem).Result;
                var data = respon1.Content.ReadAsStringAsync().Result;
                List<SanPhamChiTietViewModel> listbyname = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(data);
                var products = listbyname.OrderByDescending(c => c.MaSp);
                var model = new SanPhamChiTietViewModel
                {
                    Products = products.ToPagedList(pageNumber, pagesize)
                };
                return View(model);
                // return View(listbyname);
            }
        }

        [HttpPost]
        public void IdSize([FromBody] string Size)
        {
            if (Size == null || Size == "")
            {
                return;
            }
            else
            {
                string idsize = sizeServiece.GetAll().FirstOrDefault(c => c.SizeName == Size).Id.ToString();
                var jsonsize = JsonConvert.SerializeObject(idsize);
                HttpContext.Session.SetString("Size", jsonsize);
            }
        }

        [HttpPost]
        public void IDmausac([FromBody] string color)
        {
            if (color == null || color == "")
            {
                return;
            }
            else
            {
                string idmau = _mausacservice.GetAll().FirstOrDefault(c => c.TenMauSac == color).Id.ToString();
                var jsoncolor = JsonConvert.SerializeObject(idmau);
                HttpContext.Session.SetString("Color", jsoncolor);
            }

        }
        [HttpGet]
        public ActionResult GellByIDSanPhamCT(Guid id, int quantity, Guid idsize, Guid idcolor)
        {

            //idsize = Guid.Parse( HttpContext.Session.GetString("Size")) ;
            //idcolor = Guid.Parse( HttpContext.Session.GetString("Color"));
            var urllstmausac = $"https://localhost:7214/api/SanPhamChiTiet/GetAllMauSacTheoSanPham?IdSPCT={id}";
            var responmausac = _client.GetAsync(urllstmausac).Result;
            var datamausac = responmausac.Content.ReadAsStringAsync().Result;
            List<MauSac> listmausac = JsonConvert.DeserializeObject<List<MauSac>>(datamausac);
            ViewBag.lstmausac = listmausac;


            var urllstsize = $"https://localhost:7214/api/SanPhamChiTiet/GetAllSizeTheoSanPham?IdSPCT={id}";
            var responsize = _client.GetAsync(urllstsize).Result;
            var datasize = responsize.Content.ReadAsStringAsync().Result;
            List<AppData.model.Size> listsize = JsonConvert.DeserializeObject<List<AppData.model.Size>>(datasize);
            ViewBag.listSize = listsize;

            string url = $"https://localhost:7214/api/SanPhamChiTiet/GetByIDSPCTVM?id={id}";
            var respon = _client.GetAsync(url).Result;
            var data = respon.Content.ReadAsStringAsync().Result;
            SanPhamChiTietViewModel lstspctvm = JsonConvert.DeserializeObject<SanPhamChiTietViewModel>(data);

            var tensp = _spctViewModel.GetAll().FirstOrDefault(c => c.Id == id).TenSP;

            List<SanPhamChiTietViewModel> spctmv = _spctViewModel.GetAll().Where(c => c.TenSP == tensp).ToList();

            decimal giamin = 0;
            decimal giamax = 0;
            if (spctmv.Count > 1)
            {
                giamin = spctmv.Min(c => c.GiaBan);
                giamax = spctmv.Max(c => c.GiaBan);
                if (giamax == giamin)
                {
                    ViewBag.giamax = giamax;
                    ViewBag.giamin = 0;
                }
                else
                {
                    ViewBag.giamax = giamax;
                    ViewBag.giamin = giamin;
                }
            }
            else if (spctmv.Count == 1)
            {
                giamax = spctmv.Max(c => c.GiaBan);
                ViewBag.giamin = giamin;
                ViewBag.giamax = giamax;
            }
            var IDsanpham = _sanphamservice.GetAll().FirstOrDefault(c => c.TenSanPham == lstspctvm.TenSP).Id;
            ViewBag.IDSP = IDsanpham;

            int soluong = quantity;
            ViewBag.quatity = soluong;

            return View(lstspctvm);
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
        public async Task<ActionResult> CreateSanPhamCT()
        {
            ViewBag.DanhMuc = new SelectList(_context.danhMucs.ToList().Where(c => c.status == 1).OrderBy(c => c.TenDanhMuc), "Id", "TenDanhMuc");
            ViewBag.SanPham = new SelectList(_context.sanPhams.ToList().Where(c => c.status == 1).OrderBy(c => c.TenSanPham), "Id", "TenSanPham");
            //ViewBag.IDSP = new SelectList(_context.sanPhams.ToList().Where(c => c.status == 1).OrderBy(c => c.TenSanPham), "Id", "TenSanPham");

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

        [HttpPost]
        public List<Anh> LuuAnh([FromBody] List<string> ImagesPath)
        {
            var length = ImagesPath.Count;
            for (int i = 0; i < length; i++)
            {
                var item = anhservice.GetAll().FirstOrDefault(c => c.Connect == ImagesPath[i]);
                anhSanPhams.Add(item);
            }
            var anhSanPhamsJson = JsonConvert.SerializeObject(anhSanPhams);
            HttpContext.Session.SetString("anhSanPhams", anhSanPhamsJson);
            return anhSanPhams;
        }
        [HttpPost]
        public async Task<ActionResult> CreateSanPhamCT(SanPhamChiTiet spct)
        {
            string url2 = $"https://localhost:7214/api/SanPhamChiTiet/CreateSanPhamChiTiet?iddm={spct.IdDanhMuc}&idcl={spct.IdChatLieu}&idms={spct.IdMauSac}&idsize={spct.IdSize}&idsp={spct.IDSP}&soluong={spct.SoLuong}&gia={spct.GiaBan}&mota={spct.MoTa}";
            //string url2 = ""; &idanh={spct.IdAnh}

            var obj = JsonConvert.SerializeObject(spct);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = _client.PostAsync(url2, content).Result;

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var error = await httpResponseMessage.Content.ReadAsStringAsync();
                if (error == "Sản phẩm bị trùng mã")
                {
                    TempData["ErrorMessage"] = "Sản phẩm bị trùng mã. Mời bạn nhập mã khác";
                    return RedirectToAction("CreateSanPhamCT");

                }
                else if (error == "Sản phẩm đã tồn tại")
                {
                    TempData["ErrorMessage"] = "Sản phẩm bạn nhập " + _sanphamservice.GetAll().Where(c => c.Id == spct.IDSP).Select(c => c.TenSanPham) + " đã có trong danh sách sản phẩm.";
                    return RedirectToAction("CreateSanPhamCT");
                }
                else
                {
                    //var masp = _sanphamchitietservice.GetAll()[_sanphamchitietservice.GetAll().Count() - 1].MaSp;

                    string anhSanPhamsJson = HttpContext.Session.GetString("anhSanPhams");
                    if (anhSanPhamsJson == null)
                    {
                        return RedirectToAction("GellAllSanPhamCT");
                    }
                    else
                    {
                        List<Anh> anhSanPham1s = JsonConvert.DeserializeObject<List<Anh>>(anhSanPhamsJson);
                        AnhSanPham anhSanPham = new AnhSanPham();
                        foreach (var idanh in anhSanPham1s)
                        {
                            anhSanPham.IdSanPhamChiTiet = _sanphamchitietservice.GetAll().OrderByDescending(c => c.MaSp).First().Id;
                            anhSanPham.Idanh = idanh.Id;
                            _ianhsanpham.AddAnhChoSanPham(anhSanPham);
                        }

                        return RedirectToAction("GellAllSanPhamCT");
                    }
                }
            }
            else
            {
                return RedirectToAction("CreateSanPhamCT");
            }
        }
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult> UpdateSanPhamCT(SanPhamChiTiet spct)
        {
            ViewBag.DanhMuc = new SelectList(_context.danhMucs.ToList().Where(c => c.status == 1).OrderBy(c => c.TenDanhMuc), "Id", "TenDanhMuc");
            ViewBag.SanPham = new SelectList(_context.sanPhams.ToList().Where(c => c.status == 1).OrderBy(c => c.TenSanPham), "Id", "TenSanPham");
            ViewBag.ChatLieu = new SelectList(_context.chatLieus.ToList().Where(c => c.status == 1).OrderBy(c => c.TenChatLieu), "Id", "TenChatLieu");
            ViewBag.MauSac = new SelectList(_context.mauSacs.ToList().Where(c => c.status == 1).OrderBy(c => c.TenMauSac), "Id", "TenMauSac");
            ViewBag.Size = new SelectList(_context.sizes.ToList().Where(c => c.status == 1).OrderBy(c => c.SizeName), "Id", "SizeName");

            string urlanhsp = $"https://localhost:7214/api/SanPhamChiTiet/GetByIDSPCTVM?id={spct.Id}";
            var responanhsp = _client.GetAsync(urlanhsp).Result;
            var dataanhsp = responanhsp.Content.ReadAsStringAsync().Result;
            SanPhamChiTietViewModel lstspctvm = JsonConvert.DeserializeObject<SanPhamChiTietViewModel>(dataanhsp);

            ViewBag.anhsp = lstspctvm.Images;

            string urldetail = $"https://localhost:7214/api/SanPhamChiTiet/GetByID?Id={spct.Id}";
            var respon1 = _client.GetAsync(urldetail).Result;
            var data1 = respon1.Content.ReadAsStringAsync().Result;
            SanPhamChiTiet lstsize = JsonConvert.DeserializeObject<SanPhamChiTiet>(data1);

            string urlanh = "https://localhost:7214/api/Anh/GetAll";
            var respon = _client.GetAsync(urlanh).Result;
            var data = respon.Content.ReadAsStringAsync().Result;
            List<Anh> album = JsonConvert.DeserializeObject<List<Anh>>(data);
            ImageUploadModel img = new ImageUploadModel();
            ViewBag.upload = img.ImageFile;
            ViewBag.listanh = album;

            string urltest = $"https://localhost:7214/api/SanPhamChiTiet/UpdateSanPhamChiTiet?id={spct.Id}&iddm={spct.IdDanhMuc}&idcl={spct.IdChatLieu}&idms={spct.IdMauSac}&idsize={spct.IdSize}&idsp={spct.IDSP}&masp={spct.MaSp}&soluong={spct.SoLuong}&gia={spct.GiaBan}&mota={spct.MoTa}&trangthai={spct.status}";
            //string urltest = ""; &idanh={spct.IdAnh}
            var obj = JsonConvert.SerializeObject(spct);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PostAsync(urltest, content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string anhSanPhamsJson = HttpContext.Session.GetString("anhSanPhams");
                List<Anh> anhSanPham1s = JsonConvert.DeserializeObject<List<Anh>>(anhSanPhamsJson);
                AnhSanPham anhSanPham = new AnhSanPham();
                foreach (var idanh in anhSanPham1s)
                {
                    anhSanPham.IdSanPhamChiTiet = spct.Id;
                    anhSanPham.Idanh = idanh.Id;
                    _ianhsanpham.AddAnhChoSanPham(anhSanPham);
                }
                return RedirectToAction("GellAllSanPhamCT", "QuanTri");
            }
            else
            {
                return View(lstsize);
            }
        }
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult> DeleteSanPhamCT(SanPhamChiTiet spct)
        {
            string url = $"https://localhost:7214/api/SanPhamChiTiet/DeleteSanPhamChiTiet?Id={spct.Id}";
            var obj = JsonConvert.SerializeObject(spct);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PutAsync(url, content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var Error = await httpResponseMessage.Content.ReadAsStringAsync();
                if (Error == "true")
                {

                    TempData["SuccessFull"] = "Sản phẩm đã được thay đổi là hết hàng.";
                    return RedirectToAction("GellAllSanPhamCT", "QuanTri");
                }
                else
                {
                    TempData["ErrorMessage"] = "Xóa thất bại.";
                    return RedirectToAction("GellAllSanPhamCT", "QuanTri");
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Xóa thất bại.";
                return RedirectToAction("GellAllSanPhamCT", "QuanTri");
            }
        }
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> UploadImages(List<IFormFile> imageFiles)
        {
            if (imageFiles != null && imageFiles.Count > 0)
            {
                var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

                foreach (var imageFile in imageFiles)
                {
                    if (imageFile.Length > 0)
                    {
                        // Kiểm tra định dạng tệp
                        if (imageFile.ContentType != "image/jpeg" && imageFile.ContentType != "image/png")
                        {
                            ModelState.AddModelError("ImageFiles", "Chỉ cho phép tải lên tệp .jpg và .png");
                            return View();
                        }

                        var uniqueFileName = imageFile.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }

                        // Lưu đường dẫn vào thuộc tính Path của đối tượng Anh
                        var anh = new Anh
                        {
                            Id = Guid.NewGuid(),
                            Connect = filePath,
                            status = 1 // Có thể đặt trạng thái khác tùy ý
                        };

                        anhservice.Add(anh);
                    }
                }
                return RedirectToAction("GellAllAnh");
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
        public IActionResult Themsanphamvaogiohang(Guid id)
        {
            string url = $"https://localhost:7214/api/SanPhamChiTiet/ThemSPvaoGioHang?iduser=911a9476-05be-4a4f-8325-2ea61766e2a0&IDspct={id}";
            return View(url);
        }
        public IActionResult Themsanphamctvaogiohang(SanPhamChiTietViewModel spctvm)
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;

            var userlogin = HttpContext.User;
            var email = userlogin.FindFirstValue(ClaimTypes.Email);
            if (email != null)
            {
                var user = _context.Users.FirstOrDefault(c => c.Email == email);
                var idgh = _giohangservice.GetAll().FirstOrDefault(c => c.IdNguoiDung == user.Id);
                if (idgh == null)
                {
                    GioHang gioHang = new GioHang();
                    string urlapigiohang = $"https://localhost:7214/api/GioHang/Create?idnguoidung={user.Id}";
                    var obj = JsonConvert.SerializeObject(gioHang);
                    StringContent stringContent = new StringContent(obj, Encoding.UTF8, "application/json");
                    HttpResponseMessage httpResponseMessage = _client.PostAsync(urlapigiohang, stringContent).Result;
                }

                var idgh1 = idgh.Id;
                var spct = _spctViewModel.GetAll().FirstOrDefault(C => C.TenSP == spctvm.TenSP && C.MauSac == spctvm.MauSac && C.Size == spctvm.Size);
                if (spct == null)
                {
                    TempData["ErrorMessage"] = "Sản phẩm này đã hết.";
                    return RedirectToAction("GellByIDSanPhamCT", new { id = spctvm.Id });
                }
                else
                {
                    var IDMau = _mausacservice.GetAll().FirstOrDefault(c => c.TenMauSac == spctvm.MauSac).Id;
                    var IDSize = sizeServiece.GetAll().FirstOrDefault(c => c.SizeName == spctvm.Size).Id;
                    var IDSP = _sanphamservice.GetAll().FirstOrDefault(c => c.TenSanPham == spctvm.TenSP).Id;
                    var soluong = spctvm.SoLuong;

                    string url = $"https://localhost:7214/api/SanPhamChiTiet/ThemSPCTVaoGioHang?idnguoidung={user.Id}&idSP={IDSP}&IdMau={IDMau}&IdSize={IDSize}&soluong={soluong}";
                    GioHangChiTiet ghct = new GioHangChiTiet()
                    {
                        IdGioHang = idgh1,
                        IdSanPhamChiTiet = spct.Id,
                        DonGia = spct.GiaBan,
                        IdComboChiTiet = null,
                        SoLuong = soluong
                    };
                    var obj = JsonConvert.SerializeObject(ghct);
                    StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
                    HttpResponseMessage httpResponseMessage = _client.PostAsync(url, content).Result;
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        var result = httpResponseMessage.Content.ReadAsStringAsync().Result;
                        if (result == "Thêm Thành công.")
                        {
                            return RedirectToAction("GetGioHangChiTiet", "GioHang");
                        }
                        else if (result == "Sản phẩm đã hết hàng. Mời bạn chọn sản phẩm khác.")
                        {
                            TempData["ErrorMessage"] = "Sản phẩm này đang hết hàng. Mời bạn chọn sản phẩm khác.";
                            return RedirectToAction("GellByIDSanPhamCT", new { id = spct.Id });
                        }
                        else if (result == "Bạn chưa chọn màu. Mời bạn chọn màu.")
                        {
                            TempData["ErrorMessage"] = "Bạn chưa chọn màu. Mời bạn chọn màu.";
                            return RedirectToAction("GellByIDSanPhamCT", new { id = spct.Id });
                        }
                        else if (result == "Bạn chưa chọn size. Mời bạn chọn size.")
                        {
                            TempData["ErrorMessage"] = "Bạn chưa chọn size. Mời bạn chọn size.";
                            return RedirectToAction("GellByIDSanPhamCT", new { id = spct.Id });
                        }
                        else
                        {
                            return RedirectToAction("GetGioHangChiTiet", "GioHang");
                        }
                    }
                    else
                    {
                        return RedirectToAction("GellByIDSanPhamCT", new { id = spct.Id });
                    }
                }
            }
            else
            {
                return RedirectToAction("LoginJWT", "Login");
            }

        }
        [HttpPost]
        public async Task<IActionResult> UploadExcelFile(IFormFile excelFile)
        {
            if (excelFile != null && excelFile.Length > 0)
            {
                using (var stream = excelFile.OpenReadStream())
                {
                    ExcelPackage.LicenseContext = LicenseContext.Commercial;

                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0]; // Chọn worksheet đầu tiên (index 0)

                        // Lấy số dòng và cột
                        int rowCount = worksheet.Dimension.Rows;
                        int colCount = worksheet.Dimension.Columns;

                        // Tạo danh sách để lưu trữ dữ liệu
                        List<SanPhamChiTietViewModel> products = new List<SanPhamChiTietViewModel>();
                        List<SanPhamChiTiet> lstspct = new List<SanPhamChiTiet>();
                        List<string> lstanhsp = new List<string>();
                        string[] pathimages;
                        // Bắt đầu từ dòng thứ 2 (để bỏ qua tiêu đề)
                        for (int row = 2; row <= rowCount; row++)
                        {
                            // view hiển thị
                            SanPhamChiTietViewModel product = new SanPhamChiTietViewModel
                            {
                                TenSP = worksheet.Cells[row, 1].Value.ToString(),
                                DanhMuc = worksheet.Cells[row, 2].Value.ToString(),
                                ChatLieu = worksheet.Cells[row, 3].Value.ToString(),
                                MauSac = worksheet.Cells[row, 4].Value.ToString(),
                                Size = worksheet.Cells[row, 5].Value.ToString(),

                                SoLuong = Convert.ToInt32(worksheet.Cells[row, 6].Value),
                                GiaBan = Convert.ToDecimal(worksheet.Cells[row, 7].Value),
                                MoTa = worksheet.Cells[row, 8].Value.ToString()

                            };
                            products.Add(product);
                            try
                            {
                                SanPhamChiTiet spct = new SanPhamChiTiet()
                                {
                                    IDSP = _sanphamservice.GetAll().FirstOrDefault(c => c.TenSanPham == worksheet.Cells[row, 1].Value.ToString()).Id,
                                    IdDanhMuc = _danhmucservice.GetAll().FirstOrDefault(c => c.TenDanhMuc == worksheet.Cells[row, 2].Value.ToString()).Id,
                                    IdChatLieu = _chatlieuservice.GetAll().FirstOrDefault(c => c.TenChatLieu == worksheet.Cells[row, 3].Value.ToString()).Id,
                                    IdMauSac = _mausacservice.GetAll().FirstOrDefault(c => c.TenMauSac == worksheet.Cells[row, 4].Value.ToString()).Id,
                                    IdSize = sizeServiece.GetAll().FirstOrDefault(c => c.SizeName == worksheet.Cells[row, 5].Value.ToString()).Id,
                                    SoLuong = Convert.ToInt32(worksheet.Cells[row, 6].Value),
                                    GiaBan = Convert.ToDecimal(worksheet.Cells[row, 7].Value),
                                    MoTa = worksheet.Cells[row, 8].Value.ToString(),
                                    status = 1,
                                };
                                lstspct.Add(spct);
                            }
                            catch
                            {
                                TempData["ErrorMessage"] = "Dữ liệu đầu vào không có trong cơ sở dữ liệu.";
                                return RedirectToAction("CreateSanPhamCT", "QuanTri");
                            }

                        }

                        string productListJson = JsonConvert.SerializeObject(products);
                        //string anhlistJson = JsonConvert.SerializeObject(lstanhsp);
                        //_tempAnh = lstanhsp;
                        _tempProducts = lstspct;
                        _temspctvm = products;
                        TempData["Products"] = productListJson;
                        //TempData["Anhs"] = anhlistJson;


                        // Ở đây, bạn có danh sách "products" chứa dữ liệu từ tệp Excel
                        // Bạn có thể lưu dữ liệu này vào cơ sở dữ liệu hoặc hiển thị trên một trang khác.

                        return RedirectToAction("DisplayData", products);
                    }
                }
            }
            return RedirectToAction("GellAllSanPhamCT");
        }

        [HttpGet]
        [HttpDelete]
        public IActionResult XoaAnhSanPham(Guid idsanpham, Guid idanh)
        {
            string url = $"https://localhost:7214/api/Anh/RemoveAnh?idsp={idsanpham}&idanh={idanh}";
            HttpResponseMessage httpResponseMessage = _client.DeleteAsync(url).Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                TempData["success"] = "Xóa ảnh thành công.";
                return RedirectToAction("UpdateSanPhamCT", new { id = idsanpham });
            }
            else
            {
                TempData["Error"] = "Xóa ảnh thất bại.";
                return RedirectToAction("UpdateSanPhamCT", new { id = idsanpham });
            }

        }


        public IActionResult DisplayData()
        {
            // Lấy danh sách sản phẩm từ TempData
            string productListJson = TempData["Products"] as string;
            //string anhlistjson = TempData["Anhs"] as string;
            //List<string[]> manglon = new List<string[]>();
            //List<string[]> mangnho = new List<string[]>();

            //List<string> mangChuoi = JsonConvert.DeserializeObject<List<string>>(anhlistjson);


            var products = JsonConvert.DeserializeObject<List<SanPhamChiTietViewModel>>(productListJson);
            //for (int i = 0; i < mangChuoi.Count; i++)
            //{
            //    if (mangChuoi[i].Contains(","))
            //    {
            //        string[] Chuoitach = mangChuoi[i].Split(',');
            //        mangnho.Add(Chuoitach);
            //        //manglon.Add(mangnho[]);
            //    }
            //    else
            //    {
            //        //manglon.Add(mangChuoi[i]);
            //    }
            //}
            //var anhs = JsonConvert.DeserializeObject<List<Anh>>(anhlistjson);
            //ViewBag.hienanh = anhs;
            return View(products);
        }
        [HttpPost]
        public IActionResult AddDataToDatabase()
        {
            try
            {
                if (_tempProducts != null && _tempProducts.Count() > 0)
                {
                    for (int i = 0; i < _tempProducts.Count(); i++)
                    {
                        string url2 = $"https://localhost:7214/api/SanPhamChiTiet/CreateSanPhamChiTiet?iddm={_tempProducts[i].IdDanhMuc}&idcl={_tempProducts[i].IdChatLieu}&idms={_tempProducts[i].IdMauSac}&idsize={_tempProducts[i].IdSize}&&idsp={_tempProducts[i].IDSP}&masp={"SP" + Convert.ToString(_sanphamchitietservice.GetAll().Count() + 1)}&soluong={_tempProducts[i].SoLuong}&gia={_tempProducts[i].GiaBan}&mota={_tempProducts[i].MoTa}";
                        //string url2 = "";
                        var obj = JsonConvert.SerializeObject(_tempProducts[i]);
                        StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
                        HttpResponseMessage httpResponseMessage = _client.PostAsync(url2, content).Result;
                        if (httpResponseMessage.IsSuccessStatusCode)
                        {
                            var error = httpResponseMessage.Content.ReadAsStringAsync().Result;
                            if (error == "Sản phẩm bị trùng mã")
                            {
                                TempData["ErrorMessage"] = "Sản phẩm bị trùng mã. Mời bạn nhập mã khác";
                            }
                            else if (error == "Sản phẩm đã tồn tại")
                            {
                                TempData["ErrorMessage"] = "Sản phẩm bạn nhập " + _sanphamservice.GetAll().Where(c => c.Id == _tempProducts[i].IDSP).Select(c => c.TenSanPham) + " đã có trong danh sách sản phẩm.";
                            }
                            else
                            {
                                //string anhSanPhamsJson = HttpContext.Session.GetString("anhSanPhams");
                                //List<Anh> anhSanPham1s = JsonConvert.DeserializeObject<List<Anh>>(anhSanPhamsJson);
                                //AnhSanPham anhSanPham = new AnhSanPham();
                                //foreach (var idanh in anhSanPham1s)
                                //{
                                //    anhSanPham.IdSanPhamChiTiet = _sanphamchitietservice.GetAll().OrderByDescending(c => c.MaSp).First().Id;
                                //    anhSanPham.Idanh = idanh.Id;
                                //    _ianhsanpham.AddAnhChoSanPham(anhSanPham);
                                //}
                            }
                        }
                    }

                    return RedirectToAction("GellAllSanPhamCT", "QuanTri");
                }
                else
                {
                    return RedirectToAction("DisplayData", "QuanTri");
                }
            }
            catch (Exception ex) { }
            {
                TempData["ErrorMessage"] = "Bạn xem lại dữ liệu nhập vào không trùng khớp. Mời bạn xem lại.";
                return RedirectToAction("DisplayData", "QuanTri");
            }
        }
    }
}


