using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.BUS
{
    class MonHocBUS
    {
        private DAO.MonHocDAO mhdao;
        public MonHocBUS() { }
        public void Add(DTO.MonHoc mh)
        {
            mhdao = new DAO.MonHocDAO();
            mhdao.Add(mh);
        }
        public List<DTO.MonHoc> Query()
        {
            mhdao = new DAO.MonHocDAO();
            return mhdao.Query();
        }
        public List<DTO.MonHoc> Search(String searchid)
        {
            mhdao = new DAO.MonHocDAO();
            return mhdao.Search(searchid);
        }
        public void Update(DTO.MonHoc mh, String updateid)
        {
            mhdao = new DAO.MonHocDAO();
            mhdao.Update(mh, updateid);
        }
        public void Delete(String deleteid)
        {
            mhdao = new DAO.MonHocDAO();
            mhdao.Delete(deleteid);
        }
        private List<DTO.MonHoc> MergeASC(List<DTO.MonHoc> left, List<DTO.MonHoc> right)
        {
            List<DTO.MonHoc> result = new List<DTO.MonHoc>();

            while (left.Count > 0 || right.Count > 0)
            {
                if (left.Count > 0 && right.Count > 0)
                {
                    if (left.First().Stc <= right.First().Stc)  //so sanh hai phan tu dau tien  
                    {                                   //de xem phan tu nao nho hon
                        result.Add(left.First());
                        left.Remove(left.First());      //phan con lai cua list, ngoai tru  
                    }                                   //phan tu dau tien
                    else
                    {
                        result.Add(right.First());
                        right.Remove(right.First());
                    }
                }
                else if (left.Count > 0)
                {
                    result.Add(left.First());
                    left.Remove(left.First());
                }
                else if (right.Count > 0)
                {
                    result.Add(right.First());
                    right.Remove(right.First());
                }
            }
            return result;
        }
        private List<DTO.MonHoc> MergeSortASC(List<DTO.MonHoc> unsorted)
        {
            if (unsorted.Count <= 1)
                return unsorted;
            List<DTO.MonHoc> leftlist = new List<DTO.MonHoc>();
            List<DTO.MonHoc> rightlist = new List<DTO.MonHoc>();
            
            int middle = unsorted.Count / 2;
            for (int i = 0; i < middle; i++)  //chia danh sach chua qua sap xep  
            {
                leftlist.Add(unsorted[i]);
            }
            for (int i = middle; i < unsorted.Count; i++)
            {
                rightlist.Add(unsorted[i]);
            }

            leftlist = MergeSortASC(leftlist);
            rightlist = MergeSortASC(rightlist);
            return MergeASC(leftlist, rightlist);
        }
        public List<DTO.MonHoc>SortASC()
        {
            mhdao = new DAO.MonHocDAO();
            return MergeSortASC(mhdao.Query());
        }
        private List<DTO.MonHoc> MergeDESC(List<DTO.MonHoc> left, List<DTO.MonHoc> right)
        {
            List<DTO.MonHoc> result = new List<DTO.MonHoc>();

            while (left.Count > 0 || right.Count > 0)
            {
                if (left.Count > 0 && right.Count > 0)
                {
                    if (left.First().Stc >= right.First().Stc)  //so sanh hai phan tu dau tien  
                    {                                   //de xem phan tu nao nho hon
                        result.Add(left.First());
                        left.Remove(left.First());      //phan con lai cua list, ngoai tru  
                    }                                   //phan tu dau tien
                    else
                    {
                        result.Add(right.First());
                        right.Remove(right.First());
                    }
                }
                else if (left.Count > 0)
                {
                    result.Add(left.First());
                    left.Remove(left.First());
                }
                else if (right.Count > 0)
                {
                    result.Add(right.First());
                    right.Remove(right.First());
                }
            }
            return result;
        }
        private List<DTO.MonHoc> MergeSortDESC(List<DTO.MonHoc> unsorted)
        {
            if (unsorted.Count <= 1)
                return unsorted;
            List<DTO.MonHoc> leftlist = new List<DTO.MonHoc>();
            List<DTO.MonHoc> rightlist = new List<DTO.MonHoc>();

            int middle = unsorted.Count / 2;
            for (int i = 0; i < middle; i++)  //chia danh sach chua qua sap xep  
            {
                leftlist.Add(unsorted[i]);
            }
            for (int i = middle; i < unsorted.Count; i++)
            {
                rightlist.Add(unsorted[i]);
            }

            leftlist = MergeSortDESC(leftlist);
            rightlist = MergeSortDESC(rightlist);
            return MergeDESC(leftlist, rightlist);
        }
        public List<DTO.MonHoc> SortDESC()
        {
            mhdao = new DAO.MonHocDAO();
            return MergeSortDESC(mhdao.Query());
        }
    }
}
