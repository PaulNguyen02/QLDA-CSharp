using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.BUS
{
    class CongViecBUS
    {
        private DAO.CongViecDAO cvdao;
        public CongViecBUS() { }
        public void Add(DTO.CongViec cv)
        {
            cvdao = new DAO.CongViecDAO();
            cvdao.Add(cv);
        }
        public List<DTO.CongViec> Query()
        {
            cvdao = new DAO.CongViecDAO();
            return cvdao.Query();
        }
        public List<DTO.CongViec> Search(String searchid)
        {
            cvdao = new DAO.CongViecDAO();
            return cvdao.Search(searchid);
        }
        public List<DTO.CongViec> ASCBD()
        {
            cvdao = new DAO.CongViecDAO();
            return cvdao.SortASCBD();
        }
        public List<DTO.CongViec>DESCBD()
        {
            cvdao = new DAO.CongViecDAO();
            return cvdao.SortDESCBD();
        }
        public List<DTO.CongViec>ASCKT()
        {
            cvdao = new DAO.CongViecDAO();
            return cvdao.SortASCKT();
        }
        public List<DTO.CongViec>DESCKT()
        {
            cvdao = new DAO.CongViecDAO();
            return cvdao.SortDESCKT();
        }
        public void Update(DTO.CongViec cv, String updateid)
        {
            cvdao = new DAO.CongViecDAO();
            cvdao.Update(cv, updateid);
        }
        public void Delete(String deleteid)
        {
            cvdao = new DAO.CongViecDAO();
            cvdao.Delete(deleteid);
        }
        public int Count(String search)
        {
            cvdao = new DAO.CongViecDAO();
            return cvdao.Count(search);
        }
        public int CountFinish(String search)
        {
            cvdao = new DAO.CongViecDAO();
            return cvdao.CountFinished(search);
        }
        public List<DTO.CongViec> Search1(String search)
        {
            cvdao = new DAO.CongViecDAO();
            return cvdao.Search1(search);
        }
    }
}
