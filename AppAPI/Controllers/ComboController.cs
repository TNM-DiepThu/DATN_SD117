using AppData.data;
using AppData.model;
using AppData.Serviece.Implements;
using AppData.Serviece.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComboController : ControllerBase
    {
        private readonly IComboService comBoSer;
        private MyDbContext _dbContext = new MyDbContext();

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

        public bool UpdateAsync(Guid id, string Ten, string mota, decimal giatien)
        {
            //Combo cb = comBoSer.GetAll().FirstOrDefault(c => c.Id == id);
            //if (cb != null)
            //{
            //    cb.TenCombo = Ten;
            //    cb.MoTaCombo = mota;
            //    cb.TienGiamGia = giatien;

            //}
            //return comBoSer.Edit(cb);
            var cb = _dbContext.combos.FirstOrDefault(p => p.Id == id);
            cb.TenCombo = Ten;
            cb.MoTaCombo = mota;
            cb.TienGiamGia = giatien;
            _dbContext.combos.Update(cb);
            _dbContext.SaveChanges(); return true;
        }
        [HttpGet("[action]")]
        public Combo GetbyID(Guid id)
        {
            return _dbContext.combos.FirstOrDefault(p => p.Id == id);
        }
    }
}
