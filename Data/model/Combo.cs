using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.model
{
    public class Combo
    {
        public Guid Id { get; set; }
        public string TenCombo { get; set; }
        public string MoTaCombo { get; set; }
        public decimal PhanTramGiam { get; set; }
        public int status { get; set; }
        public virtual ICollection<ComboChiTiet> ComboChiTiet { get; set; }
    }
}
