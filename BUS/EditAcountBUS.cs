using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.BUS
{
    class EditAcountBUS
    {
        public EditAcountBUS() { }
        public string getID(string user)
        {
            DAO.EditAccountDAO eadao = new DAO.EditAccountDAO();
            return eadao.GetUserID(user).Id;
        }
        public void Update(DTO.Member mb, String updateid)
        {
            DAO.MemberDAO memdao = new DAO.MemberDAO();
            memdao.Update(mb, updateid);
        }
    }
}
