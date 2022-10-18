using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace WindowsFormsApp1.DAO
{
    class TrackerDAO
    {
        private List<DTO.Tracker> ListofMem = new List<DTO.Tracker>();
        private SqlConnection conn = Connection.GetDBConnection();
        private DTO.Tracker tkr;        
        public TrackerDAO() { }
        public List<DTO.Tracker> Query()
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
                     tkr = new DTO.Tracker()
                    {
                        Id = Convert.ToString(reader.GetValue(0)),
                        Name = Convert.ToString(reader.GetValue(1)),
                        LastModify= Convert.ToString(reader.GetValue(6)),
                         User= Convert.ToString(reader.GetValue(7)),
                     };
                    ListofMem.Add(tkr);
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
        public int Count()
        {
            int count = 0;
            try
            {               
                conn.Open();
                SqlCommand cmd;
                SqlDataReader reader;
                cmd = new SqlCommand("Select * From Thanhvien", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    count++;
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
            return count;
        }
    }      
}


