using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.model
{
    public class GioHangChiTiet
    {
        public Guid Id { get; set; }    
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public Guid? IdSanPhamChiTiet { get; set; }
        public Guid IdGioHang { get; set; }
        public Guid? IdComboChiTiet { get; set; }
        public SanPhamChiTiet? SanPhamChiTiet { get; set; }
        public GioHang GioHang { get; set; }
        public ComboChiTiet? ComboChiTiet { get; set; }
    }
}
