using AppData.model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill.Serviece.Interfaces
{
    public interface IChatLieuServiece
    {
        public bool Add(ChatLieu p);
        public List<ChatLieu> GetAll();
        public bool Del(Guid id);
        public bool Edit(ChatLieu p);
        public ChatLieu GetAllByID(Guid id);
    }
}
