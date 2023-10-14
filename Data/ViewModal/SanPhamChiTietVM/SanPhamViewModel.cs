using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.ViewModal.SanPhamChiTietVM
{
    public class SanPhamViewModel
    {
        public Guid Id { get; set; }
        public string ThuongHieu { get; set; }
        public string XuatSu { get; set; }
        public string TenSanPham { get; set; }
        public int status { get; set; }

    }
}
