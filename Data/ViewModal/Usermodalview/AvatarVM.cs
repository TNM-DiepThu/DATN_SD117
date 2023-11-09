using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.ViewModal.Usermodalview
{
    public class AvatarVM
    {
        public Guid Id { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
