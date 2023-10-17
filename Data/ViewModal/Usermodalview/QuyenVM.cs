using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.ViewModal.Usermodalview
{
    public class QuyenVM
    {      
            public Guid Id { get; set; }
            [Required]
            public string? Name { get; set; }
            [Required]
            public string? normalizedName { get; set; }

            public string? concurrencyStamp { get; set; }
            public int status { get; set; }     
    }
}
