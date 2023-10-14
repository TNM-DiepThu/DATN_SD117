using AppData.ViewModal.SanPhamChiTietVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.model
{
    public class SanPhamChiTiet
    {
        public Guid Id { get; set; }
          
        public Guid IdDanhMuc { get; set; }
        public Guid IDSP { get; set; }
        public Guid IdChatLieu { get; set; }
        public Guid IdMauSac { get; set; }
        public Guid IdSize { get; set; }
        public Guid IdAnh { get; set; }

        public string MaSp { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaBan { get; set; }
        public string? MoTa { get; set; }

        public int status { get; set; }

        public SanPham SanPham { get; set; }

        public Anh Anh { get; set; }
        
        public MauSac MauSac { get; set; }
   
        public Size Size { get; set; }

        public ChatLieu ChatLieu { get; set; }
        //public ImageUploadModel  imageUploadModel { get; set; }
        public DanhMuc DanhMuc { get; set;}
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }
        public virtual ICollection <HoaDonChiTiet> hoaDonChiTiets { get; set; }
        public virtual ICollection<ComboChiTiet> comboChiTiets { get; set; }
        public virtual ICollection<GioHangChiTiet> gioHangChiTiets { get; set; }


    }
}
