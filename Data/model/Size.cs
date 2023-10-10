using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.model
{
    public class Size
    {
        public Guid Id { get; set; }
        public string SizeName { get; set; }
        public int status { get; set; }

        public virtual ICollection<SanPhamChiTiet> sanPhamChiTiets { get; set;}
    }
}
