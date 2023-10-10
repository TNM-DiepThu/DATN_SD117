using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.model
{
    public class XuatSu
    {
        public Guid Id { get; set; }
        public string TenXuatSu { get; set; }
        public int Status { get; set; }
        public virtual ICollection<SanPham> SanPham { get; set;}
    }
}
