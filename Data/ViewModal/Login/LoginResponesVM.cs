using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.ViewModal.Login
{
    public class LoginResponesVM
    {
        public string Token { get; set; }
        public string Error { get; set; }
        public bool Successful { get; set; }
    }
}
