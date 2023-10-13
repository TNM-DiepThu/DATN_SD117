using AppData.model;
using Bill.Serviece.Implements;
using Bill.Serviece.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly ISizeServiece _sizesv;
        public SizeController()
        {
            _sizesv = new SizeServiece();
        }
        [HttpGet("GetAll")]

        public  IEnumerable<Size> GetAllSize()
        {
            return _sizesv.GetAll();
        }
        [HttpPost("Create")]
        public bool  CreateAsync(string size)
        {
            Size size1 = new Size()
            {
                Id = Guid.NewGuid(),
                SizeName = size,
                status = 1,
            };
            return _sizesv.Add(size1);
        }
        [HttpPut("Delete/{Id}")]

        public bool DeleteSize(Guid Id)
        {
            return _sizesv.Del(Id);
        }

        [HttpPut("Update/{id}")]

        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] Size p)
        {
            var result =  _sizesv.Edit(id, p);
            return Ok(result);
        }
    }
}
