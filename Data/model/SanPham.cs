using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.model
{
    public class SanPham
    {
        public Guid Id { get; set; }
        public string TenSanPham { get; set; }
        public int status { get; set; }
        public Guid IdThuongHieu { get; set; }
        public ThuongHieu ThuongHieu { get; set; }
        public Guid IdXuatSu { get; set; }
        public XuatSu XuatSu { get; set; }
        public virtual ICollection<SanPhamChiTiet> SanPhamChiTiets { get; set; }
    }
}
