using AppData.data;
using AppData.model;
using Bill.Serviece.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill.Serviece.Implements
{
    public class ChatLieuServiece : IChatLieuServiece
    {
        private readonly MyDbContext _context;
        public ChatLieuServiece()
        {
            _context = new MyDbContext();
        }

        public bool Add(ChatLieu p)
        {
            try
            {
                ChatLieu chatLieu = new ChatLieu()
                {
                    Id = Guid.NewGuid(),
                    TenChatLieu = p.TenChatLieu,
                    status = 1,
                };
                _context.chatLieus.Add(chatLieu);
                _context.SaveChanges();
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }

        public bool Del(Guid id)
        {
            try
            {
                ChatLieu chatlieu = _context.chatLieus.FirstOrDefault(x => x.Id == id);
                if (chatlieu != null)
                {
                    chatlieu.status = 0;
                }
                _context.chatLieus.Update(chatlieu);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(ChatLieu p)
        {
            try
            {
                ChatLieu chatlieu = _context.chatLieus.FirstOrDefault(x => x.Id == p.Id);
                if (chatlieu != null)
                {
                    chatlieu.TenChatLieu = p.TenChatLieu;
                    chatlieu.status = p.status;
                }
                _context.chatLieus.Update(chatlieu);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<ChatLieu> GetAll()
        {
            return _context.chatLieus.ToList();
        }
        public ChatLieu GetAllByID(Guid id)
        {
            return _context.chatLieus.FirstOrDefault(c => c.Id == id);
        }
    }
}
