using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.BUS
{
    class CTMHBUS
    {
        private DAO.CTMHDAO ctmhdao;
        public CTMHBUS() { }
        public void Add(DTO.CTMH ctmh)
        {
            ctmhdao = new DAO.CTMHDAO();
            ctmhdao.Add(ctmh);
        }
        public List<DTO.CTMH> Query()
        {
            ctmhdao = new DAO.CTMHDAO();
            return ctmhdao.Query();
        }
        public List<DTO.CTMH> Search(String searchid)
        {
            ctmhdao = new DAO.CTMHDAO();
            return ctmhdao.Search(searchid);
        }
        public void Update(DTO.CTMH ctmh, String updateid)
        {
            ctmhdao = new DAO.CTMHDAO();
            ctmhdao.Update(ctmh, updateid);
        }
        public void Delete(String deleteid)
        {
            ctmhdao = new DAO.CTMHDAO();
            ctmhdao.Delete(deleteid);
        }
    }
}
