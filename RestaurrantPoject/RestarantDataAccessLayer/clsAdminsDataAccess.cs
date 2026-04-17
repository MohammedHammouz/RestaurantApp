

using RestarantDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsTB_AdminsDataacess
{
    public class TB_AdminsData
    {
        public static DataTable GetAllTB_Admins()
        {
            string query = "SELECT * FROM TB_Admins";

            using (SqlConnection connection = new SqlConnection(clsDataSettings.connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                DataTable dt = new DataTable();
                try
                {
                    connection.Open();
                    dt.Load(command.ExecuteReader());
                }
                catch { }
                return dt;
            }
        }

        public static int AddNewTB_Admins(Guid AdminID, Guid UserID, decimal MonthlySalary)
        {
            int TB_AdminsID = -1;

            string query = @"INSERT INTO TB_Admins
                 AdminID
UserID
MonthlySalary

                VALUES
                @AdminID
@UserID
@MonthlySalary
;
                SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(clsDataSettings.connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@AdminID", AdminID);
                command.Parameters.AddWithValue("@UserID", UserID);
                command.Parameters.AddWithValue("@MonthlySalary", MonthlySalary);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        TB_AdminsID = insertedID;
                    }
                }
                catch { }
            }

            return TB_AdminsID;
        }

        public static bool DeleteTB_Admins(int AdminsID)
        {
            int rowsAffected = 0;
            string query = @"DELETE FROM TB_Admins 
                            WHERE AdminsID = @AdminsID";

            using (SqlConnection connection = new SqlConnection(clsDataSettings.connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@AdminsID", AdminsID);
                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch { }
            }
            return (rowsAffected > 0);
        }

        public static bool Update(Guid AdminID, Guid UserID, decimal MonthlySalary)
        {
            string query = @"UPDATE TB_Admins  
                            SET AdminID= @AdminID);
UserID= @UserID);

                            WHERE TB_AdminsID = @TB_AdminsID";

            using (SqlConnection connection = new SqlConnection(clsDataSettings.connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@AdminID", AdminID);
                command.Parameters.AddWithValue("@UserID", UserID);
                command.Parameters.AddWithValue("@MonthlySalary", MonthlySalary);

                int rowsAffected = 0;
                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch { return false; }

                return rowsAffected > 0;
            }
        }
    }
}