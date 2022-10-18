using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace WindowsFormsApp1.DAO
{
    class STeamDAO
    {
        private List<DTO.Subteam> ListSubt = new List<DTO.Subteam>();
        private SqlConnection conn = Connection.GetDBConnection();
        private DTO.Subteam su;
        public STeamDAO() { }
        public void Add(DTO.Subteam su)
        {
            try
            {
                conn.Open();
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.InsertCommand = new SqlCommand("Insert SUBTEAM values ('" + su.Idsteam + "',N'" + su.nameofsteam + "'," + su.numofmem + ")", conn);
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
        public void Update(DTO.Subteam su, String updateid)
        {
            try
            {
                conn.Open();
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.InsertCommand = new SqlCommand("UPDATE SubTeam SET IDSTEAM ='" + su.Idsteam + "',TENSTEAM=N'" + su.nameofsteam + "', SOLUONG=" + su.numofmem + " WHERE IDSTEAM='" + updateid + "'", conn);
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
        public List<DTO.Subteam> Query()
        {
            try
            {
                conn.Open();
                SqlCommand cmd;
                SqlDataReader reader;
                cmd = new SqlCommand("Select * From SUBTEAM", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    su = new DTO.Subteam()
                    {
                        Idsteam = Convert.ToString(reader.GetValue(0)),
                        nameofsteam = Convert.ToString(reader.GetValue(1)),
                        numofmem = Convert.ToInt32(reader.GetValue(2)),
                    };
                    ListSubt.Add(su);
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
            return ListSubt;
        }
        public List<DTO.Subteam> Search(String searchid)
        {
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand("select * from SUBTEAM where IDSTEAM = @idsteam", conn);
                command.Parameters.Add(new SqlParameter("@idsteam", searchid));
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        su = new DTO.Subteam()
                        {
                            Idsteam = Convert.ToString(reader.GetValue(0)),
                            nameofsteam = Convert.ToString(reader.GetValue(1)),
                            numofmem = Convert.ToInt32(reader.GetValue(2)),
                        };
                        ListSubt.Add(su);
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
            return ListSubt;
        }
        public void Delete(String deleteid)
        {
            try
            {
                conn.Open();
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.InsertCommand = new SqlCommand("DELETE FROM SUBTEAM WHERE IDSTEAM='" + deleteid + "'", conn);
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
