using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.model
{
    public class Quyen : IdentityRole<Guid>
    {
        public int status {  get; set; }
    }
}
