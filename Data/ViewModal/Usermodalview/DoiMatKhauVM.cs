using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.ViewModal.Usermodalview
{
    public class DoiMatKhauVM
    {
        public Guid? Id { get; set; }
        public string oldpass { get; set; }
        public string pass { get; set; }
        public string? repass { get; set; }
    }
}
