using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.BUS
{
    class AdvanceSettingBUS
    {
        public DAO.AdvanceSetting avs;
        public AdvanceSettingBUS() { }
        public void DeleteNV() 
        {
            avs = new DAO.AdvanceSetting();
            avs.DeleteNV();
        }
        public void DeleteCV()
        {
            avs = new DAO.AdvanceSetting();
            avs.DeleteCV();
        }
        public void DeleteDA()
        {
            avs = new DAO.AdvanceSetting();
            avs.DeleteDA();
        }
        public void DeleteMH()
        {
            avs = new DAO.AdvanceSetting();
            avs.DeletePC();
        }
        public void DeletePC()
        {
            avs = new DAO.AdvanceSetting();
            avs.DeletePC();
        }
        public void DeleteCTMH()
        {
            avs = new DAO.AdvanceSetting();
            avs.DeleteCTMH();
        }
    }
}
