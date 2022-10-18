using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1.DAO
{
    class CongViecDAO
    {      
        private List<DTO.CongViec> ListCV = new List<DTO.CongViec>();
        private SqlConnection conn = Connection.GetDBConnection();
        private DTO.CongViec todo;
        public CongViecDAO() { }
        public void Add(DTO.CongViec cv)
        {
            try
            {
                conn.Open();
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.InsertCommand = new SqlCommand("Insert CongViec values ('" + cv.Macv + "',N'" + cv.Tencv + "','" + cv.Tgbd + "','" + cv.Tgkt + "',N'"+cv.Tgth+"','"+cv.Mada+"',"+cv.Trangthai+")", conn);
                adap.InsertCommand.ExecuteNonQuery();
                Console.WriteLine("Insert Successfully !");
            }
            catch (SqlException sqlex)
            {
                Console.WriteLine("Error: " + sqlex.Message);
                MessageBox.Show("Bạn cần phải điền vào bảng đồ án trước", "OOPS Lỗi !");
            }
            finally
            {
                conn.Close();
            }
        }
        public void Update(DTO.CongViec cv, String updateid)
        {
            try
            {
                conn.Open();
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.InsertCommand = new SqlCommand("UPDATE CongViec SET MACV='" + cv.Macv + "',TENCV=N'" + cv.Tencv + "',TGBD='" + cv.Tgbd + "',TGKT='" + cv.Tgkt +"',TGTH=N'"+cv.Tgth+"',MADA='"+cv.Mada+ "', STAWORK=" + cv.Trangthai + " WHERE MACV='" + updateid + "'", conn);
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
        public List<DTO.CongViec> Query()
        {
            try
            {
                conn.Open();
                SqlCommand cmd;
                SqlDataReader reader;
                cmd = new SqlCommand("Select * From CongViec", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    todo = new DTO.CongViec()
                    {
                        Macv = Convert.ToString(reader.GetValue(0)),
                        Tencv = Convert.ToString(reader.GetValue(1)),
                        Tgbd = Convert.ToString(reader.GetValue(2)),
                        Tgkt = Convert.ToString(reader.GetValue(3)),
                        Tgth = Convert.ToString(reader.GetValue(4)),
                        Mada = Convert.ToString(reader.GetValue(5)),
                        Trangthai=Convert.ToInt32(reader.GetValue(6)),
                    };
                    ListCV.Add(todo);
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
            return ListCV;
        }
        public List<DTO.CongViec> Search(String searchid)
        {
            try
            {
                conn.Open();                
                SqlCommand command = new SqlCommand("select * from Congviec where MACV = @macv or TENCV=@macv", conn);
                command.Parameters.Add(new SqlParameter("@macv", searchid));
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        todo = new DTO.CongViec()
                        {
                            Macv = Convert.ToString(reader.GetValue(0)),
                            Tencv = Convert.ToString(reader.GetValue(1)),
                            Tgbd = Convert.ToString(reader.GetValue(2)),
                            Tgkt = Convert.ToString(reader.GetValue(3)),
                            Tgth = Convert.ToString(reader.GetValue(4)),
                            Mada = Convert.ToString(reader.GetValue(5))
                        };
                        ListCV.Add(todo); 
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
            return ListCV;
        }
        public List<DTO.CongViec> SortASCBD()
        {
            try
            {
                conn.Open();
                SqlCommand cmd;
                SqlDataReader reader;
                cmd = new SqlCommand("Select * From CongViec order by tgbd", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    todo = new DTO.CongViec()
                    {
                        Macv = Convert.ToString(reader.GetValue(0)),
                        Tencv = Convert.ToString(reader.GetValue(1)),
                        Tgbd = Convert.ToString(reader.GetValue(2)),
                        Tgkt = Convert.ToString(reader.GetValue(3)),
                        Tgth = Convert.ToString(reader.GetValue(4)),
                        Mada = Convert.ToString(reader.GetValue(5))
                    };
                    ListCV.Add(todo);
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
            return ListCV;
        }
        public List<DTO.CongViec> SortDESCBD()
        {
            try
            {
                conn.Open();
                SqlCommand cmd;
                SqlDataReader reader;
                cmd = new SqlCommand("Select * From CongViec order by tgbd DESC", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    todo = new DTO.CongViec()
                    {
                        Macv = Convert.ToString(reader.GetValue(0)),
                        Tencv = Convert.ToString(reader.GetValue(1)),
                        Tgbd = Convert.ToString(reader.GetValue(2)),
                        Tgkt = Convert.ToString(reader.GetValue(3)),
                        Tgth = Convert.ToString(reader.GetValue(4)),
                        Mada = Convert.ToString(reader.GetValue(5))
                    };
                    ListCV.Add(todo);
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
            return ListCV;
        }
        public List<DTO.CongViec> SortASCKT()
        {
            try
            {
                conn.Open();
                SqlCommand cmd;
                SqlDataReader reader;
                cmd = new SqlCommand("Select * From CongViec order by tgkt", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    todo = new DTO.CongViec()
                    {
                        Macv = Convert.ToString(reader.GetValue(0)),
                        Tencv = Convert.ToString(reader.GetValue(1)),
                        Tgbd = Convert.ToString(reader.GetValue(2)),
                        Tgkt = Convert.ToString(reader.GetValue(3)),
                        Tgth = Convert.ToString(reader.GetValue(4)),
                        Mada = Convert.ToString(reader.GetValue(5))
                    };
                    ListCV.Add(todo);
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
            return ListCV;
        }
        public List<DTO.CongViec> SortDESCKT()
        {
            try
            {
                conn.Open();
                SqlCommand cmd;
                SqlDataReader reader;
                cmd = new SqlCommand("Select * From CongViec order by tgkt DESC", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    todo = new DTO.CongViec()
                    {
                        Macv = Convert.ToString(reader.GetValue(0)),
                        Tencv = Convert.ToString(reader.GetValue(1)),
                        Tgbd = Convert.ToString(reader.GetValue(2)),
                        Tgkt = Convert.ToString(reader.GetValue(3)),
                        Tgth = Convert.ToString(reader.GetValue(4)),
                        Mada = Convert.ToString(reader.GetValue(5))
                    };
                    ListCV.Add(todo);
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
            return ListCV;
        }
        public void Delete(String deleteid)
        {
            try
            {
                conn.Open();
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.InsertCommand = new SqlCommand("DELETE FROM CongViec WHERE MACV='" + deleteid + "'", conn);
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
        public List<DTO.CongViec> Search1(String searchid)
        {
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand("select * from Congviec, MonHoc  where MAMH = @mamh or TENMH=@mamh", conn);
                command.Parameters.Add(new SqlParameter("@mamh", searchid));
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        todo = new DTO.CongViec()
                        {
                            Macv = Convert.ToString(reader.GetValue(0)),
                            Tencv = Convert.ToString(reader.GetValue(1)),
                            Tgbd = Convert.ToString(reader.GetValue(2)),
                            Tgkt = Convert.ToString(reader.GetValue(3)),
                            Tgth = Convert.ToString(reader.GetValue(4)),
                            Mada = Convert.ToString(reader.GetValue(5)),
                            Trangthai=Convert.ToInt32(reader.GetValue(6))
                        };
                        ListCV.Add(todo);
                    }
                }
            }
            catch (SqlException sqlex)
            {
                Console.WriteLine("Error: " + sqlex.Message);
                MessageBox.Show("Không tìm thấy môn học này");
            }
            finally
            {
                conn.Close();
            }
            return ListCV;
        }
        public int Count(String searchid)
        {
            int count = 0;
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand("select count(MACV) from Congviec, MonHoc where MAMH = @mamh or TENMH=@mamh", conn);
                command.Parameters.Add(new SqlParameter("@mamh", searchid));
                //using (SqlDataReader reader = command.ExecuteReader())
                //{
                count = (Int32)command.ExecuteScalar();
                //}
            }
            catch (SqlException sqlex)
            {
                Console.WriteLine("Error: " + sqlex.Message);
            }
            finally
            {
                conn.Close();
            }
            return count;
        }
        public int CountFinished(String searchid)
        {
            int count = 0;
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand("select count(MACV) from Congviec, MonHoc where Stawork=1 and MAMH = @mamh", conn);
                command.Parameters.Add(new SqlParameter("@mamh", searchid));
                //using (SqlDataReader reader = command.ExecuteReader())
                //{
                    count = (Int32)command.ExecuteScalar();
                //}
            }
            catch (SqlException sqlex)
            {
                Console.WriteLine("Error: " + sqlex.Message);                
            }
            finally
            {
                conn.Close();
            }
            return count;
        }
    }
}
