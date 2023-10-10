using AppData.data;
using AppData.model;
using Bill.Serviece.Interfaces;
using Bill.ViewModal.SanPhamVM;
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
        public async Task<bool> Add(ChatLieuVM p)
        {
            try
            {
                var x = new ChatLieu()
                {
                    Id = Guid.NewGuid(),
                    TenChatLieu = p.TenChatLieu,
                    status = p.status
                };
                _context.Add(x);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> Del(Guid id)
        {
            try
            {
                var list = await _context.chatLieus.ToListAsync();
                var obj = list.FirstOrDefault(c => c.Id == id);
                _context.chatLieus.Remove(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> Edit(Guid id, ChatLieuVM p)
        {
            try
            {
                var listobj = await _context.chatLieus.ToListAsync();
                var obj = listobj.FirstOrDefault(c => c.Id == id);
                obj.TenChatLieu = p.TenChatLieu;
                obj.status = p.status;

                _context.chatLieus.Update(obj);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<ChatLieuVM>> GetAll()
        {
            var list = await _context.chatLieus.ToListAsync();
            var listvm = new List<ChatLieuVM>();
            foreach (var item in list)
            {
                var x = new ChatLieuVM();
                x.Id = item.Id;
                x.TenChatLieu = item.TenChatLieu;
                x.status = item.status;
                listvm.Add(x);
            }
            return listvm.ToList();
        }
    }
}
