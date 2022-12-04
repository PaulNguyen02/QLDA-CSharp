using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.BUS
{
    class MessageBUS
    {
        private DAO.MessageDAO msdao;
        public bool Search(string email)
        {
            msdao = new DAO.MessageDAO();
            //res = cs.Checked(id);
            if (String.Compare(msdao.Search(email).Email, email) == 0)
                return true;
            else
                return false;
        }
    }
}
