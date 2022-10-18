using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1.DAO
{
    class CTMHDAO
    {
        private List<DTO.CTMH> ListCTMH = new List<DTO.CTMH>();
        private SqlConnection conn = Connection.GetDBConnection();
        private DTO.CTMH chitietmh;
        public CTMHDAO() { }
        public void Add(DTO.CTMH ctmh)
        {
            try
            {
                conn.Open();
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.InsertCommand = new SqlCommand("Insert CTMH values ('" + ctmh.Idsv + "','" + ctmh.Mamh + "')", conn);
                adap.InsertCommand.ExecuteNonQuery();
                Console.WriteLine("Insert Successfully !");
            }
            catch (SqlException sqlex)
            {
                Console.WriteLine("Error: " + sqlex.Message);
                MessageBox.Show("Bạn cần phải điền vào bảng thành viên, môn học trước", "OOPS Lỗi !");
            }
            finally
            {
                conn.Close();
            }
        }
        public void Update(DTO.CTMH ctmh, String updateid)
        {
            try
            {
                conn.Open();
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.InsertCommand = new SqlCommand("UPDATE CTMH SET MSSV='" + ctmh.Idsv + "',MAMH='" + ctmh.Mamh+ "' WHERE MSSV='" + updateid + "'", conn);
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
        public List<DTO.CTMH> Query()
        {
            try
            {
                conn.Open();
                SqlCommand cmd;
                SqlDataReader reader;
                cmd = new SqlCommand("Select * From CTMH", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    chitietmh= new DTO.CTMH()
                    {
                        Idsv= Convert.ToString(reader.GetValue(0)),
                        Mamh= Convert.ToString(reader.GetValue(1))
                    };
                    ListCTMH.Add(chitietmh);
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
            return ListCTMH;
        }
        public List<DTO.CTMH> Search(String searchid)
        {
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand("Select * From CTMH where MAMH = @mamh", conn);
                command.Parameters.Add(new SqlParameter("@mamh", searchid));
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        chitietmh = new DTO.CTMH()
                        {
                            Idsv = Convert.ToString(reader.GetValue(0)),
                            Mamh = Convert.ToString(reader.GetValue(1))
                        };
                        ListCTMH.Add(chitietmh);
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
            return ListCTMH;
        }
        public void Delete(String deleteid)
        {
            try
            {
                conn.Open();
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.InsertCommand = new SqlCommand("DELETE FROM CTMH WHERE MSSV='" + deleteid + "'", conn);
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
    }
}
