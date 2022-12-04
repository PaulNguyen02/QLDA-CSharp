using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.BUS
{
    class CheckSigninBUS
    {
        private DAO.CheckSignin cs;
        private DTO.Member res;
        public CheckSigninBUS() { }
        public bool Checked(string id, string pass)
        {
            cs = new DAO.CheckSignin();
            //res = cs.Checked(id);
             if (String.Compare(cs.Checked(id).Pass, pass) == 0)
                return true;                
             else      
                return false;
        }
        public string getEmail(string id)
        {
            cs = new DAO.CheckSignin();
            return cs.Checked(id).Email;
        }
        public string getPass(string id)
        {
            cs = new DAO.CheckSignin();
            return cs.Checked(id).Pass;
        }
    }
}
