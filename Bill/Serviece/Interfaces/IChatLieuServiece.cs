using Bill.ViewModal.SanPhamVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill.Serviece.Interfaces
{
    public interface IChatLieuServiece
    {
        public Task<bool> Add(ChatLieuVM p);
        public Task<List<ChatLieuVM>> GetAll();
        public Task<bool> Del(Guid id);
        public Task<bool> Edit(Guid id, ChatLieuVM p);
    }
}
