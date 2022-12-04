using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1.DAO
{
    class PhanCongDAO
    {
        private SqlConnection conn = Connection.GetDBConnection();
        private List<DTO.PhanCong> List = new List<DTO.PhanCong>();
        private DTO.PhanCong pc;
        public PhanCongDAO() { }
        public void Add(DTO.PhanCong pc)
        {
            try
            {
                conn.Open();
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.InsertCommand = new SqlCommand("Insert PHANCONG values ('"+pc.Idsteam+"','" + pc.Mssv + "',N'" + pc.Macv + "')", conn);
                adap.InsertCommand.ExecuteNonQuery();
                Console.WriteLine("Insert Successfully !");
            }
            catch (SqlException sqlex)
            {
                Console.WriteLine("Error: " + sqlex.Message);
                MessageBox.Show("Bạn cần phải điền vào bảng thành viên, công việc, subteam trước", "OOPS Lỗi !");
            }
            finally
            {
                conn.Close();
            }
        }
        public void Update(DTO.PhanCong pc, String updateid)
        {
            try
            {
                conn.Open();
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.InsertCommand = new SqlCommand("UPDATE PhanCong SET IDSTEAM='"+pc.Idsteam+"', MSSV='" + pc.Mssv + "', MACV=N'" + pc.Macv + "' WHERE MSSV='" + updateid + "'", conn);
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
        public void Delete(String deleteid)
        {
            try
            {
                conn.Open();
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.InsertCommand = new SqlCommand("DELETE FROM PhanCong WHERE MSSV='" + deleteid + "'", conn);
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
        public List<DTO.PhanCong> Query()
        {
            try
            {
                conn.Open();
                SqlCommand cmd;
                SqlDataReader reader;
                cmd = new SqlCommand("Select * From PhanCong", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    pc = new DTO.PhanCong()
                    {
                        Idsteam = Convert.ToString(reader.GetValue(0)),
                        Mssv = Convert.ToString(reader.GetValue(1)),
                        Macv = Convert.ToString(reader.GetValue(2)),
                    };
                    List.Add(pc);
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
            return List;
        }       
        public List<DTO.PhanCong> Search(String searchid)
        {
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand("select * from PHANCONG where MSSV = @mssv", conn);
                command.Parameters.Add(new SqlParameter("@mssv", searchid));
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pc = new DTO.PhanCong()
                        {
                            Mssv = Convert.ToString(reader.GetValue(0)),
                            Macv = Convert.ToString(reader.GetValue(1)),
                        };
                        List.Add(pc);
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
            return List;
        }
    }
}
