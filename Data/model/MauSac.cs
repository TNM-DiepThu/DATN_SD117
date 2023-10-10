using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.model
{
    public class MauSac
    {
        public Guid Id { get; set; }
        public string TenMauSac { get; set; }

        public int status { get; set; }

        public ICollection<SanPhamChiTiet> phamChiTiet { get; set; }
    }
}
