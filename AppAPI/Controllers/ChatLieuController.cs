using AppData.model;
using Bill.Serviece.Implements;
using Bill.Serviece.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Net.WebSockets;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatLieuController : ControllerBase
    {
        private readonly IChatLieuServiece _chatlieusv;
        public ChatLieuController()
        {
            _chatlieusv = new ChatLieuServiece();
        }
        [HttpGet("GetAll")]

        public IEnumerable<ChatLieu> GetAll()
        {
              return _chatlieusv.GetAll();
        }

        [HttpGet("GetByID")]

        public ChatLieu GetAllByID(Guid id)
        {
            return _chatlieusv.GetAllByID(id);
        }
        [HttpPost("Create")]
        public bool Create(string name)
        {
            ChatLieu chat = new ChatLieu()
            {
                Id = Guid.NewGuid(),
                TenChatLieu = name,
                status = 1,
            };
            return _chatlieusv.Add(chat);
        }
        [HttpDelete("Delete/{Id}")]

        public bool Delete(Guid Id)
        {
            return _chatlieusv.Del(Id);
           
        }

        [HttpPut("Update/{id}")]

        public bool UpdateAsync(Guid id , string name , int status)
        {
            ChatLieu chat = _chatlieusv.GetAll().FirstOrDefault(c => c.Id == id);
            if (chat != null)
            {
                chat.TenChatLieu = name;
                chat.status = status;
            }
            return _chatlieusv.Edit(chat);
        }
    }
}
