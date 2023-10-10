using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.model
{
    public class BinhLuan
    {
        public Guid Id { get; set; }
        public string DanhGiaSanPham { get; set; }
        public string NoiDung { get ; set; }    
        public DateTime NgayTao { get; set; }
        public string HinhAnh { get; set; }
        public int status { get; set; }
        public Guid IdNguoiDung { get; set; }
        public NguoiDung nguoiDung { get; set; }
        public Guid IdSanPhamChiTiet { get ; set; } 
        public SanPhamChiTiet SanPhamChiTiets { get; set; }
    }
}
