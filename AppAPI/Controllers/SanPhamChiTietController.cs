using AppData.model;
using AppData.Serviece.Implements;
using AppData.Serviece.Interfaces;
using AppData.Serviece.ViewModeService;
using AppData.ViewModal.SanPhamChiTietVM;
using Bill.Serviece.Implements;
using Bill.Serviece.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Drawing;
using System.Linq;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamChiTietController : ControllerBase
    {
        private readonly ISanPhamChiTietServiece _sanphamCTsv;
        private readonly IAnhServiece _anhServiece;
        private readonly ComBoChiTietViewModelService _comchitietviewmodelsevice;
        private readonly IComboChiTietService _comboctsevice;
        private readonly IGioHangCTService _giohangctservice;
        private readonly IGioHangService _giohangservice;
        private readonly IDanhMucServiece _anhMuc;
        private readonly SanPhamChiTietViewModelService _spctViewModel;
        private readonly SanPhamViewModelService _spViewModel;
        private readonly IChatLieuServiece _chatLieu;
        private readonly IMauSacServiece _auSacServiece;
        private readonly ISizeServiece sizeServiece;
        private readonly ISanPhamServiece sanPhamServiece;
        public SanPhamChiTietController( )
        {
            _anhMuc = new DanhMucServiece();
            _anhServiece = new AnhServiece();
            _giohangctservice = new GioHangCTService();
            _giohangservice = new GioHangService();
            _comchitietviewmodelsevice = new ComBoChiTietViewModelService();
            _chatLieu = new ChatLieuServiece();
            _spctViewModel = new SanPhamChiTietViewModelService();
            _comboctsevice = new ComBoChiTietService();
            _spViewModel = new SanPhamViewModelService();
            _auSacServiece = new MauSacServiece();
            sizeServiece = new SizeServiece();
            sanPhamServiece = new SanPhamServiece();
            _sanphamCTsv = new SanPhamChiTietServiece();
        }
        [HttpGet("GetAll")]
        public IEnumerable<SanPhamChiTiet> GetAllAsync()
        {
            return _sanphamCTsv.GetAll();
        }
        [HttpGet("[action]")]
        public IEnumerable<SanPhamChiTietViewModel> GetAllSanphamchitietViewModel()
        {
            return _spctViewModel.GetAll();
        }
        [HttpGet("[action]")]
        public IEnumerable<SanPhamViewModel> GetAllSanPhamViewModel()
        {
            return _spViewModel.GetAllSanPham();
        }
        [HttpGet("[action]")]
        public SanPhamChiTietViewModel GetByIDSPCTVM(Guid id)
        {
            return _spctViewModel.GetById(id);
        }
        [HttpGet("[action]")]
        public IEnumerable<SanPhamChiTietViewModel> GetByNameSPCTVM(string name)
        {
            return _spctViewModel.GetByName(name);
        }
        [HttpGet("[action]")]
        public SanPhamViewModel GetByIDSPVM(Guid id)
        {
            return _spViewModel.GetById(id);
        }
        [HttpGet("GetByID")]
        public SanPhamChiTiet GetByID(Guid Id)
        {
            return _sanphamCTsv.GetByID(Id);
        }

        [HttpGet("[action]")]
        public List<MauSac> GetAllMauSacTheoSanPham(Guid IdSPCT)
        {
            List<MauSac> lstmau = new List<MauSac>();

            var SanPhamChiTietVM = _spctViewModel.GetAll().FirstOrDefault(c => c.Id == IdSPCT);
            var TenSP = SanPhamChiTietVM.TenSP;

            var mau = _spctViewModel.GetAll().Where(c => c.TenSP == TenSP).Select(c => c.MauSac).Distinct().ToList();
            for (var i = 0; i < mau.Count(); i++)
            {
                MauSac sizelist = _auSacServiece.GetAll().FirstOrDefault(c => c.TenMauSac == mau[i]);
                lstmau.Add(sizelist);
            }
            return lstmau.ToList();
        }
        [HttpGet("[action]")]
        public List<AppData.model.Size> GetAllSizeTheoSanPham(Guid IdSPCT)
        {
            List<AppData.model.Size> lstsize  = new List<AppData.model.Size>();

            var SanPhamChiTietVM = _spctViewModel.GetAll().FirstOrDefault(c => c.Id == IdSPCT);
            var TenSP = SanPhamChiTietVM.TenSP;

            var size = _spctViewModel.GetAll().Where(c => c.TenSP == TenSP).Select(c => c.Size).Distinct().ToList();
            for(var i = 0;  i < size.Count() ; i++)
            {
                AppData.model.Size sizelist = sizeServiece.GetAll().FirstOrDefault(c => c.SizeName == size[i]);
                lstsize.Add(sizelist);
            }
            return lstsize.ToList();
        }

        [HttpPost("CreateSanPhamChiTiet")]
        public string CreateSanPhamChiTiet(Guid iddm, Guid idcl, Guid idms, Guid idsize, Guid idanh, Guid idsp, string masp, int soluong, decimal gia, string? mota)
        {
            SanPhamChiTiet spct = new SanPhamChiTiet()
            {
                Id = Guid.NewGuid(),
                IdAnh = _anhServiece.GetAll().FirstOrDefault(c => c.Id == idanh).Id,
                IdChatLieu = _chatLieu.GetAll().FirstOrDefault(c => c.Id == idcl).Id,
                IdDanhMuc = _anhMuc.GetAll().FirstOrDefault(c => c.Id == iddm).Id,
                IdMauSac = _auSacServiece.GetAll().FirstOrDefault(c => c.Id == idms).Id,
                IdSize = sizeServiece.GetAll().FirstOrDefault(c => c.Id == idsize).Id,
                IDSP = sanPhamServiece.GetAll().FirstOrDefault(c => c.Id == idsp).Id,
                MaSp = masp,
                SoLuong = soluong,
                GiaBan = gia,
                MoTa = mota,
                status = 1,
            };
            if (_sanphamCTsv.GetAll().Any(c => c.MaSp == masp))
            {
                return "Sản phẩm bị trùng mã";
            }
            if (_sanphamCTsv.GetAll().Any(c => c.IdAnh == idanh && c.IdChatLieu == idcl && c.IdDanhMuc == iddm && c.IdMauSac == idms && c.IDSP == idsp && c.IdSize == idsize))
            {
                return "Sản phẩm đã tồn tại";
            }
            return _sanphamCTsv.Add(spct);
        }

        [HttpPost("[action]")]
        public bool UpdateSanPhamChiTiet(Guid id, Guid iddm, Guid idcl, Guid idms, Guid idsize, Guid idanh, Guid idsp, string masp, int soluong, decimal gia, string? mota, int trangthai)
        {
            SanPhamChiTiet spct = _sanphamCTsv.GetAll().FirstOrDefault(c => c.Id == id);
            if (spct != null)
            {
                spct.IdAnh = _anhServiece.GetAll().FirstOrDefault(c => c.Id == idanh).Id;
                spct.IdChatLieu = _chatLieu.GetAll().FirstOrDefault(c => c.Id == idcl).Id;
                spct.IdDanhMuc = _anhMuc.GetAll().FirstOrDefault(c => c.Id == iddm).Id;
                spct.IdMauSac = _auSacServiece.GetAll().FirstOrDefault(c => c.Id == idms).Id;
                spct.IdSize = sizeServiece.GetAll().FirstOrDefault(c => c.Id == idsize).Id;
                spct.IDSP = sanPhamServiece.GetAll().FirstOrDefault(c => c.Id == idsp).Id;
                spct.MaSp = masp;
                spct.SoLuong = soluong;
                spct.GiaBan = gia;
                spct.MoTa = mota;
                spct.status = trangthai;
            }

            return _sanphamCTsv.Edit(id, spct);
        }
        [HttpPut("[action]")]

        public bool DeleteSanPhamChiTiet(Guid Id)
        {
            SanPhamChiTiet spct = _sanphamCTsv.GetAll().FirstOrDefault(c => c.Id == Id);
            if (spct != null)
            {
                spct.status = 0;
            }
            return _sanphamCTsv.Edit(Id, spct);
        }

        [HttpPost("[action]")]
        public bool ThemSPvaoGioHang(Guid iduser, Guid? IDcombo, Guid? IDspct)
        {
            if (IDcombo == null && IDspct == null)
            {
                return false;
            }
            else if (IDcombo == null && IDspct != null)
            {
                if (_giohangctservice.GetAllGioHangTheoNguoiDungDangNhap(iduser).Any(c => c.IdSanPhamChiTiet == IDspct) == false)
                {
                    GioHangChiTiet ghct = new GioHangChiTiet()
                    {
                        Id = Guid.NewGuid(),
                        IdGioHang = _giohangservice.GetAll().FirstOrDefault(c => c.IdNguoiDung == iduser).Id,
                        IdComboChiTiet = null,
                        IdSanPhamChiTiet = _sanphamCTsv.GetAll().FirstOrDefault(c => c.Id == IDspct).Id,
                        SoLuong = 1,
                        DonGia = _sanphamCTsv.GetAll().FirstOrDefault(c => c.Id == IDspct).GiaBan
                    };
                    return _giohangctservice.Add(ghct);
                }
                else
                {
                    GioHangChiTiet ghct1 = _giohangctservice.GetAllGioHangTheoNguoiDungDangNhap(iduser).FirstOrDefault(c => c.IdSanPhamChiTiet == IDspct);
                    ghct1.SoLuong += 1;
                    ghct1.DonGia = _sanphamCTsv.GetAll().FirstOrDefault(c => c.Id == IDspct).GiaBan;
                    return _giohangctservice.Edit(ghct1.Id, ghct1);
                }
            }
            else if (IDcombo != null && IDspct == null)
            {
                if (_giohangctservice.GetAllGioHangTheoNguoiDungDangNhap(iduser).Any(c => c.IdComboChiTiet == IDcombo) == false)
                {
                    GioHangChiTiet ghct = new GioHangChiTiet()
                    {
                        Id = Guid.NewGuid(),
                        IdGioHang = _giohangservice.GetAll().FirstOrDefault(c => c.IdNguoiDung == iduser).Id,
                        IdComboChiTiet = _comchitietviewmodelsevice.GetAllComBoChiTiet().FirstOrDefault(c => c.Id == IDcombo).Id,
                        IdSanPhamChiTiet = null,
                        SoLuong = 1,
                        DonGia = _comchitietviewmodelsevice.GetAllComBoChiTiet().FirstOrDefault(c => c.Id == IDcombo).ThanhTienComBo,
                    };
                    return _giohangctservice.Add(ghct);
                }
                else
                {
                    GioHangChiTiet ghct1 = _giohangctservice.GetAllGioHangTheoNguoiDungDangNhap(iduser).FirstOrDefault(c => c.IdComboChiTiet == IDcombo);
                    ghct1.SoLuong += 1;
                    ghct1.DonGia = _comchitietviewmodelsevice.GetAllComBoChiTiet().FirstOrDefault(c => c.Id == IDcombo).ThanhTienComBo;
                    return _giohangctservice.Edit(ghct1.Id, ghct1);
                }
            }
            return false;

        }

        [HttpPost("[action]")]
        public string ThemSPCTVaoGioHang(Guid idnguoidung, Guid idSP, Guid IdMau, Guid IdSize, int soluong)
        {
            if (IdMau == null)
            {
                return "Bạn chưa chọn màu. Mời bạn chọn màu.";
            }
            else if (IdSize == null)
            {
                return "Bạn chưa chọn size. Mời bạn chọn size.";
            }
            else
            {
                if (soluong <= 0) return "Số lượng của bạn phải lớn hơn 0.";

                var spct = _sanphamCTsv.GetAll().FirstOrDefault(c => c.IdMauSac == IdMau && c.IdSize == IdSize && c.IDSP == idSP);
                if (spct == null) return "Không có sản phẩm.";
                if (spct.status == 0 || spct.SoLuong == 0) return "Sản phẩm đã hết hàng. Mời bạn chọn sản phẩm khác.";
              
                if (soluong > 0 && soluong < spct.SoLuong) {
                    GioHangChiTiet ghct = _giohangctservice.GetAllGioHangTheoNguoiDungDangNhap(idnguoidung).FirstOrDefault(c => c.IdSanPhamChiTiet == spct.Id);
                    if (_giohangservice.GetAll().Any(c => c.IdNguoiDung == idnguoidung))
                    {
                        if (ghct == null)
                        {
                            GioHangChiTiet newghct = new GioHangChiTiet()
                            {
                                Id = Guid.NewGuid(),
                                IdComboChiTiet = null,
                                IdGioHang = _giohangservice.GetAll().FirstOrDefault(c => c.IdNguoiDung == idnguoidung).Id,
                                IdSanPhamChiTiet = spct.Id,
                                SoLuong = soluong,
                                DonGia = spct.GiaBan
                            };
                            _giohangctservice.Add(newghct);
                            return "Thêm Thành công.";
                        }
                        else
                        {
                            ghct.SoLuong += 1;
                            _giohangctservice.Edit(ghct.Id, ghct);
                        }
                    } else
                    {
                        _giohangservice.Add(idnguoidung);
                        if (ghct == null)
                        {
                            GioHangChiTiet newghct = new GioHangChiTiet()
                            {
                                Id = Guid.NewGuid(),
                                IdComboChiTiet = null,
                                IdGioHang = _giohangservice.GetAll().FirstOrDefault(c => c.IdNguoiDung == idnguoidung).Id,
                                IdSanPhamChiTiet = spct.Id,
                                SoLuong = soluong,
                                DonGia = spct.GiaBan
                            };
                            _giohangctservice.Add(ghct);
                            return "Thêm Thành công.";
                        }
                        else
                        {
                            ghct.SoLuong += 1;
                            _giohangctservice.Edit(ghct.Id, ghct);
                        }
                    }
                    
                  
                } else
                {
                    return "Số lượng vượt quá số lượng tổng sản phẩm. Mời bạn nhập lại số lượng.";
                }

                return "";
            }
        }
    }
}
