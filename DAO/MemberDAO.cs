using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace WindowsFormsApp1.DAO
{
    class MemberDAO
    {
        private String id, name, dob, pos, email, pass;
        private DTO.Member person;
        private List<DTO.Member> ListofMem = new List<DTO.Member>();
        private SqlConnection conn = Connection.GetDBConnection();
        private DTO.Member mem;
        public MemberDAO() { }
        public void Add(DTO.Member mb)
        {
            try
            {
                conn.Open();
                SqlDataAdapter adap = new SqlDataAdapter();
                //adap.InsertCommand = new SqlCommand("Insert Thanhvien values ('" + mb.Id + "',N'" + mb.Name + "','" + mb.DoB + "',N'" + mb.Pos + "','"+mb.Email+"','"+mb.Pass+"')", conn);
                adap.InsertCommand = new SqlCommand("insert THANHVIEN (MSSV,HOTEN,NGAYSINH,VITRI,EMAIL,PASS) values ('" + mb.Id + "',N'" + mb.Name + "','" + mb.DoB + "',N'" + mb.Pos + "','" + mb.Email + "','" + mb.Pass + "')", conn);
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
        public void Update(DTO.Member mb, String updateid)
        {
            try
            {
                conn.Open();
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.InsertCommand = new SqlCommand("UPDATE Thanhvien SET MSSV='" + mb.Id + "',HOTEN=N'" + mb.Name + "',NGAYSINH='" + mb.DoB + "',VITRI=N'" + mb.Pos + "', EMAIL='" + mb.Email + "', PASS='" + mb.Pass + "' WHERE MSSV='" + updateid + "'", conn);
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
        public List<DTO.Member> Query()
        {
            try
            {
                conn.Open();
                SqlCommand cmd;
                SqlDataReader reader;
                cmd = new SqlCommand("Select * From Thanhvien", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    mem = new DTO.Member()
                    {
                        Id = Convert.ToString(reader.GetValue(0)),
                        Name = Convert.ToString(reader.GetValue(1)),
                        DoB = Convert.ToString(reader.GetValue(2)),
                        Pos = Convert.ToString(reader.GetValue(3)),
                        Email = Convert.ToString(reader.GetValue(4)),
                    };
                    ListofMem.Add(mem);
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
            return ListofMem;
        }
        public List<DTO.Member> Search(String searchid)
        {
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand("select * from Thanhvien where MSSV = @mssv or HOTEN=@mssv", conn);
                command.Parameters.Add(new SqlParameter("@mssv", searchid));
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        mem = new DTO.Member()
                        {
                            Id = Convert.ToString(reader.GetValue(0)),
                            Name = Convert.ToString(reader.GetValue(1)),
                            DoB = Convert.ToString(reader.GetValue(2)),
                            Pos = Convert.ToString(reader.GetValue(3))
                        };
                        ListofMem.Add(mem);
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
            return ListofMem;
        }
        public void Delete(String deleteid)
        {
            try
            {
                conn.Open();
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.InsertCommand = new SqlCommand("DELETE FROM THANHVIEN WHERE MSSV='" + deleteid + "'", conn);
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

