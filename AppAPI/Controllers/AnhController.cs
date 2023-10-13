using AppData.model;
using Bill.Serviece.Implements;
using Bill.Serviece.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnhController : ControllerBase
    {
        private readonly IAnhServiece _anhsv;
        public AnhController()
        {
            _anhsv = new AnhServiece();
        }
        [HttpGet("GetAll")]

        public IEnumerable<Anh> GetAllAnh()
        {
            return _anhsv.GetAll();
        }
        [HttpPost("Create")]
        public bool CreateAnh(string name)
        {
            Anh anh = new Anh()
            {
                Id = Guid.NewGuid(),
                Connect = name,
                status = 1,
            };
            return _anhsv.Add(anh);
        }
    }
}
