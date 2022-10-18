using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace WindowsFormsApp1.DAO
{
    class CheckSignin
    {
        private String id, name, dob, pos , email, pass;
        private DTO.Member person;
        private SqlConnection conn = Connection.GetDBConnection();
        private DTO.Member mem;
        public CheckSignin() { }
        public DTO.Member Checked(String searchid)
        {
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand("select * from Thanhvien where MSSV = @masv", conn);
                command.Parameters.Add(new SqlParameter("@masv", searchid));
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
                        Pass=pass,
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
    }
}
