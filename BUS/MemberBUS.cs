using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace WindowsFormsApp1.BUS
{
    class MemberBUS
    {
        private DAO.MemberDAO memdao;
        public MemberBUS() { }
        public void Add(DTO.Member mb)
        {
            memdao = new DAO.MemberDAO();
            memdao.Add(mb);
        }       
        public List<DTO.Member> Query()
        {
            memdao = new DAO.MemberDAO();
            return memdao.Query();
        }
        public List<DTO.Member> Search(String searchid)
        {
            memdao = new DAO.MemberDAO();
            return memdao.Search(searchid);
        }
        public void Update(DTO.Member mb, String updateid)
        {
            DAO.MemberDAO memdao = new DAO.MemberDAO();
            memdao.Update(mb, updateid);
        }
        public void Delete(String deleteid)
        {
            DAO.MemberDAO memdao = new DAO.MemberDAO();
            memdao.Delete(deleteid);
        }
    }
}
