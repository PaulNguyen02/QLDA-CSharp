using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace WindowsFormsApp1.DAO
{
    class MonHocDAO
    {
        private List<DTO.MonHoc> ListMH = new List<DTO.MonHoc>();
        private SqlConnection conn = Connection.GetDBConnection();
        private DTO.MonHoc monhoc;
        public MonHocDAO() { }
        public void Add(DTO.MonHoc mh)
        {
            try
            {
                conn.Open();
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.InsertCommand = new SqlCommand("Insert MonHoc values ('" + mh.Mamh + "',N'" + mh.Tenmh + "'," + mh.Stc + ","+mh.Tiendo+")", conn);
                adap.InsertCommand.ExecuteNonQuery();
                Console.WriteLine("Insert Successfully !");
            }
            catch (SqlException sqlex)
            {
                Console.WriteLine("Error: " + sqlex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        public void Update(DTO.MonHoc mh, String updateid)
        {
            try
            {
                conn.Open();
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.InsertCommand = new SqlCommand("UPDATE MonHoc SET MAMH='" + mh.Mamh + "',tenmh=N'" + mh.Tenmh + "',STC=" + mh.Stc + " WHERE MAMH='" + updateid + "'", conn);
                adap.InsertCommand.ExecuteNonQuery();
                Console.WriteLine("Update Successfully !");
            }
            catch (SqlException sqlex)
            {
                Console.WriteLine("Error: " + sqlex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        public List<DTO.MonHoc> Query()
        {
            try
            {
                conn.Open();
                SqlCommand cmd;
                SqlDataReader reader;
                cmd = new SqlCommand("Select * From MonHoc", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    monhoc = new DTO.MonHoc()
                    {
                        Mamh = Convert.ToString(reader.GetValue(0)),
                        Tenmh = Convert.ToString(reader.GetValue(1)),
                        Stc = Convert.ToInt32(reader.GetValue(2))
                    };
                    ListMH.Add(monhoc);
                }
            }
            catch (SqlException sqlex)
            {
                Console.WriteLine("Error: " + sqlex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ListMH;
        }
        public List<DTO.MonHoc> Search(String searchid)
        {
            try
            {
                conn.Open();                
                SqlCommand command = new SqlCommand("select * from MonHoc where MAMH = @mamh or TENMH=@mamh", conn);
                command.Parameters.Add(new SqlParameter("@mamh", searchid));
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        monhoc = new DTO.MonHoc()
                        {
                            Mamh = Convert.ToString(reader.GetValue(0)),
                            Tenmh = Convert.ToString(reader.GetValue(1)),
                            Stc = Convert.ToInt32(reader.GetValue(2))
                        };
                        ListMH.Add(monhoc);
                    }
                }
            }
            catch (SqlException sqlex)
            {
                Console.WriteLine("Error: " + sqlex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ListMH;
        }
        public void Delete(String deleteid)
        {
            try
            {
                conn.Open();
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.InsertCommand = new SqlCommand("DELETE FROM MONHOC WHERE MAMH='" + deleteid + "'", conn);
                adap.InsertCommand.ExecuteNonQuery();
                Console.WriteLine("Delete Successfully !");
            }
            catch (SqlException sqlex)
            {
                Console.WriteLine("Error: " + sqlex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        public List<DTO.MonHoc> Sort()
        {
            try
            {
                conn.Open();
                SqlCommand cmd;
                SqlDataReader reader;
                cmd = new SqlCommand("Select * From MonHoc", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    monhoc = new DTO.MonHoc()
                    {
                        Mamh = Convert.ToString(reader.GetValue(0)),
                        Tenmh = Convert.ToString(reader.GetValue(1)),
                        Stc = Convert.ToInt32(reader.GetValue(2))
                    };
                    ListMH.Add(monhoc);
                }
            }
            catch (SqlException sqlex)
            {
                Console.WriteLine("Error: " + sqlex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ListMH;
        }
    }
}
