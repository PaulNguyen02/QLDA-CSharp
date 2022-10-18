using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.BUS
{
    class PhanCongBUS
    {
        private DAO.PhanCongDAO pcdao;
        public PhanCongBUS() { }
        public void Add(DTO.PhanCong pc)
        {
            pcdao = new DAO.PhanCongDAO();
            pcdao.Add(pc);
        }
        public void Update(DTO.PhanCong pc, String updateid)
        {
            pcdao = new DAO.PhanCongDAO();
            pcdao.Update(pc, updateid);
        }
        public void Delete(String updateid)
        {
            pcdao = new DAO.PhanCongDAO();
            pcdao.Delete(updateid);
        }
        public List<DTO.PhanCong> Query()
        {
            pcdao = new DAO.PhanCongDAO();
            return pcdao.Query();
        }
        public List<DTO.PhanCong> Search(String searchid)
        {
            pcdao = new DAO.PhanCongDAO();
            return pcdao.Search(searchid);
        }        
    }
}
