using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.BUS
{
    class STeamBUS
    {
        private DAO.STeamDAO steamdao;
        public STeamBUS() { }
        public void Add(DTO.Subteam su)
        {
            steamdao = new DAO.STeamDAO();
            steamdao.Add(su);
        }
        public List<DTO.Subteam> Query()
        {
            steamdao = new DAO.STeamDAO();
            return steamdao.Query();
        }
        public List<DTO.Subteam> Search(String searchid)
        {
            steamdao = new DAO.STeamDAO();
            return steamdao.Search(searchid);
        }
        public void Update(DTO.Subteam su, String updateid)
        {
            steamdao = new DAO.STeamDAO();
            steamdao.Update(su, updateid);
        }
        public void Delete(String deleteid)
        {
            steamdao = new DAO.STeamDAO();
            steamdao.Delete(deleteid);
        }
    }
}
