using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.BUS
{
    class TrackerBUS
    {
        private DAO.TrackerDAO tkdao;
        public TrackerBUS() { }
        public List<DTO.Tracker> Query()
        {
            tkdao = new DAO.TrackerDAO();
            return tkdao.Query();
        }
        public int Count()
        {
            tkdao = new DAO.TrackerDAO();
            return tkdao.Count();
        }
    }
}
