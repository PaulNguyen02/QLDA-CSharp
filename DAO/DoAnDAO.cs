using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1.DAO
{
    class DoAnDAO
    {
        private List<DTO.DoAn> ListDA = new List<DTO.DoAn>();
        private SqlConnection conn = Connection.GetDBConnection();
        private DTO.DoAn project;
        public DoAnDAO() { }
        public void Add(DTO.DoAn da)
        {
            try
            {
                conn.Open();
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.InsertCommand = new SqlCommand("Insert DoAn values ('" + da.MADA + "',N'" + da.TenDA + "','" + da.Tgbd + "','" + da.Tgkt + "',N'" + da.Tgth + "','" + da.Mamh + "',"+da.Tiendo+")", conn);
                adap.InsertCommand.ExecuteNonQuery();
                Console.WriteLine("Insert Successfully !");
            }
            catch (SqlException sqlex)
            {
                Console.WriteLine("Error: " + sqlex.Message);
                MessageBox.Show("Bạn cần phải điền vào bảng môn học trước","OOPS Lỗi !");
            }
            finally
            {
                conn.Close();
            }
        }
        public void Update(DTO.DoAn da, String updateid)
        {
            try
            {
                conn.Open();
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.InsertCommand = new SqlCommand("UPDATE DoAn SET MADA='" + da.MADA + "',TENDA=N'" + da.TenDA + "',NGAYNHAN='" + da.Tgbd + "',NGAYNOP='" + da.Tgkt + "',TGTH=N'" + da.Tgth + "',MAMH='" + da.Mamh + "' WHERE MADA='" + updateid + "'", conn);
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
        public List<DTO.DoAn> Query()
        {
            try
            {
                conn.Open();
                SqlCommand cmd;
                SqlDataReader reader;
                cmd = new SqlCommand("Select * From DoAn", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    project = new DTO.DoAn()
                    {
                        MADA= Convert.ToString(reader.GetValue(0)),
                        TenDA = Convert.ToString(reader.GetValue(1)),
                        Tgbd = Convert.ToString(reader.GetValue(2)),
                        Tgkt =  Convert.ToString(reader.GetValue(3)),
                        Tgth =  Convert.ToString(reader.GetValue(4)),
                        Mamh = Convert.ToString(reader.GetValue(5)),
                    };
                    ListDA.Add(project);
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
            return ListDA;
        }
        public List<DTO.DoAn> Search(String searchid)
        {
            try
            {
                conn.Open();
                /*SqlCommand cmd;
                SqlDataReader reader;
                cmd = new SqlCommand("Select * From DoAn where MADA='"+searchid+"'", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    project = new DTO.DoAn()
                    {
                        MADA = Convert.ToString(reader.GetValue(0)),
                        TenDA = Convert.ToString(reader.GetValue(1)),
                        Tgbd = Convert.ToString(reader.GetValue(2)),
                        Tgkt = Convert.ToString(reader.GetValue(3)),
                        Tgth = Convert.ToString(reader.GetValue(4)),
                        Mamh = Convert.ToString(reader.GetValue(5)),
                    };
                    ListDA.Add(project);
                }*/
                SqlCommand command = new SqlCommand("select * from DOAN where MADA = @mada", conn);
                command.Parameters.Add(new SqlParameter("@mada", searchid));
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        project = new DTO.DoAn()
                        {
                            MADA = Convert.ToString(reader.GetValue(0)),
                            TenDA = Convert.ToString(reader.GetValue(1)),
                            Tgbd = Convert.ToString(reader.GetValue(2)),
                            Tgkt = Convert.ToString(reader.GetValue(3)),
                            Tgth = Convert.ToString(reader.GetValue(4)),
                            Mamh = Convert.ToString(reader.GetValue(5)),
                        };
                        ListDA.Add(project);
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
            return ListDA;
        }
        public void Delete(String deleteid)
        {
            try
            {
                conn.Open();
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.InsertCommand = new SqlCommand("DELETE FROM DoAn WHERE MADA='" + deleteid + "'", conn);
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
        public List<DTO.DoAn> SortASCBD()
        {
            try
            {
                conn.Open();
                SqlCommand cmd;
                SqlDataReader reader;
                cmd = new SqlCommand("Select * From DoAn order by NGAYNHAN", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    project = new DTO.DoAn()
                    {
                        MADA = Convert.ToString(reader.GetValue(0)),
                        TenDA = Convert.ToString(reader.GetValue(1)),
                        Tgbd = Convert.ToString(reader.GetValue(2)),
                        Tgkt = Convert.ToString(reader.GetValue(3)),
                        Tgth = Convert.ToString(reader.GetValue(4)),
                        Mamh = Convert.ToString(reader.GetValue(5))
                    };
                    ListDA.Add(project);
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
            return ListDA;
        }
        public List<DTO.DoAn> SortDESCBD()
        {
            try
            {
                conn.Open();
                SqlCommand cmd;
                SqlDataReader reader;
                cmd = new SqlCommand("Select * From DoAn order by NGAYNHAN Desc", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    project = new DTO.DoAn()
                    {
                        MADA = Convert.ToString(reader.GetValue(0)),
                        TenDA = Convert.ToString(reader.GetValue(1)),
                        Tgbd = Convert.ToString(reader.GetValue(2)),
                        Tgkt = Convert.ToString(reader.GetValue(3)),
                        Tgth = Convert.ToString(reader.GetValue(4)),
                        Mamh = Convert.ToString(reader.GetValue(5))
                    };
                    ListDA.Add(project);
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
            return ListDA;
        }
        public List<DTO.DoAn> SortASCKT()
        {
            try
            {
                conn.Open();
                SqlCommand cmd;
                SqlDataReader reader;
                cmd = new SqlCommand("Select * From DoAn order by NGAYNOP", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    project = new DTO.DoAn()
                    {
                        MADA = Convert.ToString(reader.GetValue(0)),
                        TenDA = Convert.ToString(reader.GetValue(1)),
                        Tgbd = Convert.ToString(reader.GetValue(2)),
                        Tgkt = Convert.ToString(reader.GetValue(3)),
                        Tgth = Convert.ToString(reader.GetValue(4)),
                        Mamh = Convert.ToString(reader.GetValue(5))
                    };
                    ListDA.Add(project);
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
            return ListDA;
        }
        public List<DTO.DoAn> SortDESCKT()
        {
            try
            {
                conn.Open();
                SqlCommand cmd;
                SqlDataReader reader;
                cmd = new SqlCommand("Select * From DoAn order by NGAYNOP Desc", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    project = new DTO.DoAn()
                    {
                        MADA = Convert.ToString(reader.GetValue(0)),
                        TenDA = Convert.ToString(reader.GetValue(1)),
                        Tgbd = Convert.ToString(reader.GetValue(2)),
                        Tgkt = Convert.ToString(reader.GetValue(3)),
                        Tgth = Convert.ToString(reader.GetValue(4)),
                        Mamh = Convert.ToString(reader.GetValue(5))
                    };
                    ListDA.Add(project);
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
            return ListDA;
        }
    }
}
