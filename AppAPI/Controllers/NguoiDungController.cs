using AppData.data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguoiDungController : ControllerBase
    {
        // GET: api/<NguoiDungController
        
        
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<NguoiDungController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<NguoiDungController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<NguoiDungController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NguoiDungController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
