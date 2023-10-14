using AppData.model;
using AppData.Serviece.Implements;
using AppData.Serviece.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComboController : ControllerBase
    {
        private readonly IComboService comBoSer;
        public ComboController()
        {
            comBoSer = new ComboService();
        }
        [HttpGet("GetAll")]

        public IEnumerable<Combo> GetAllAsync()
        {
            return comBoSer.GetAll();
        }
        [HttpPost("Create")]
        public bool Create(string Ten, string mota, decimal giatien)
        {
            Combo combo = new Combo()
            {
                Id = Guid.NewGuid(),
                TenCombo = Ten,
                MoTaCombo = mota,
                TienGiamGia = giatien,
                status = 1,
            };
            if (comBoSer.GetAll().Any(c => c.TenCombo == Ten))
            {
                return false;
            }
            
            return comBoSer.Add(combo);
        }
        [HttpDelete("Delete/{Id}")]

        public bool DeleteAsync(Guid Id)
        {
            var result = comBoSer.Del(Id);
            return result;
        }

        [HttpPut("Update/{id}")]

        public bool UpdateAsync(Guid id, [FromBody] Combo p)
        {
            var result = comBoSer.Edit(id, p);
            return result;
        }
    }
}
