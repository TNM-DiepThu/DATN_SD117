using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.ViewModal.SanPhamChiTietVM
{
    public class SanPhamChiTietViewModel
    {
        public Guid Id { get; set; }
        public string DanhMuc { get; set; }
        public string TenSP { get; set; }
        public string ChatLieu { get; set; }
        public string MauSac { get; set; }
        public string Size { get; set; }
        public string Anh { get; set; }

        public string MaSp { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaBan { get; set; }
        public string? MoTa { get; set; }

        public int status { get; set; }
    }
}
