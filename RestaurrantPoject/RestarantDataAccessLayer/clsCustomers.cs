
using RestarantDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsTB_CustomersDataacess
{
    public class TB_CustomersData
    {
        public static DataTable GetAllTB_Customers()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.connectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_Customers_GetAll", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        dt.Load(command.ExecuteReader());
                    }
                }
            }
            catch
            {

            }
            
            return dt;

        }

        public static int AddNewTB_Customers(Guid CustomerID, string FullName, DateTime CreatedDate, byte LoyalityPoints, string PhoneNumber, Guid CreatedByAdminID)
        {
            int TB_CustomersID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_Customers_AddNew", connection))
                    {
                        command.Parameters.AddWithValue("@CustomerID", CustomerID);
                        command.Parameters.AddWithValue("@FullName", FullName);
                        command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                        command.Parameters.AddWithValue("@LoyalityPoints", LoyalityPoints);
                        command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                        command.Parameters.AddWithValue("@CreatedByAdminID", CreatedByAdminID);
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            TB_CustomersID = insertedID;
                        }
                    }
                }
            }
            catch
            {

            }
            
            return TB_CustomersID;
        }

        public static bool DeleteTB_Customers(Guid CustomersID)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_Customers_Delete", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CustomersID", CustomersID);
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch
            {

            }
            return (rowsAffected > 0);
        }

        public static bool Update(Guid CustomerID, string FullName, DateTime CreatedDate, byte LoyalityPoints, string PhoneNumber, Guid CreatedByAdminID)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_Customers_Update", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CustomerID", CustomerID);
                        command.Parameters.AddWithValue("@FullName", FullName);
                        command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                        command.Parameters.AddWithValue("@LoyalityPoints", LoyalityPoints);
                        command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                        command.Parameters.AddWithValue("@CreatedByAdminID", CreatedByAdminID);
                        rowsAffected = command.ExecuteNonQuery();

                    }

                }
            }
            catch
            {

            }
            return rowsAffected > 0;
        }
    }
}
