using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace WindowsFormsApp1.DAO
{
    class EditAccountDAO
    {
        private String id, name, dob, pos, email, pass;
        private DTO.Member person;
        private SqlConnection conn = Connection.GetDBConnection();
        public EditAccountDAO(){}
        public DTO.Member GetUserID(String useremail)
        {
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand("select * from Thanhvien where EMAIL=@useremail", conn);
                command.Parameters.Add(new SqlParameter("@useremail", useremail));
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = Convert.ToString(reader.GetValue(0));
                        name = Convert.ToString(reader.GetValue(1));
                        dob = Convert.ToString(reader.GetValue(2));
                        pos = Convert.ToString(reader.GetValue(3));
                        email = Convert.ToString(reader.GetValue(4));
                        pass = Convert.ToString(reader.GetValue(5));
                    }
                    person = new DTO.Member()
                    {
                        Id = id,
                        Name = name,
                        DoB = dob,
                        Pos = pos,
                        Email = email,
                        Pass = pass,
                    };
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
            return person;
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
    }
}
