using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.BUS
{
    class DoAnBUS
    {
        private DAO.DoAnDAO dadao;
        public DoAnBUS() { }
        public void Add(DTO.DoAn da)
        {
            dadao = new DAO.DoAnDAO();
            dadao.Add(da);
        }
        public List<DTO.DoAn> Query()
        {
            dadao = new DAO.DoAnDAO();
            return dadao.Query();
        }
        public List<DTO.DoAn> Search(String searchid)
        {
            dadao = new DAO.DoAnDAO();
            return dadao.Search(searchid);
        }
        public void Update(DTO.DoAn da, String updateid)
        {
            dadao = new DAO.DoAnDAO();
            dadao.Update(da, updateid);
        }
        public void Delete(String deleteid)
        {
            dadao = new DAO.DoAnDAO();
            dadao.Delete(deleteid);
        }
        public List<DTO.DoAn> ASCBD()
        {
            dadao = new DAO.DoAnDAO();
            return dadao.SortASCBD();
        }
        public List<DTO.DoAn> DESCBD()
        {
            dadao = new DAO.DoAnDAO();
            return dadao.SortDESCBD();
        }
        public List<DTO.DoAn> ASCKT()
        {
            dadao = new DAO.DoAnDAO();
            return dadao.SortASCKT();
        }
        public List<DTO.DoAn> DESCKT()
        {
            dadao = new DAO.DoAnDAO();
            return dadao.SortDESCKT();
        }
    }
}
