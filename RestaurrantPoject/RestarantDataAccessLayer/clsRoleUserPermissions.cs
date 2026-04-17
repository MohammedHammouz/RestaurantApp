
using RestarantDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsTB_RoleUserPermissionsDataacess
{
    public class TB_RoleUserPermissionsData
    {
        public static DataTable GetAllTB_RoleUserPermissions(int PageNumber)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_RoleUserPermissions_GetAll", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PageNumber", PageNumber);
                        dt.Load(command.ExecuteReader());
                    }
                }
            }
            catch
            {

            }
            return dt;
        }

        public static int AddNewTB_RoleUserPermissions(Guid RoleUserPermissionID, byte RoleID, Guid AdminID, int Permission)
        {
            int TB_RoleUserPermissionsID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_RoleUserPermissions_AddNew", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RoleUserPermissionID", RoleUserPermissionID);
                        command.Parameters.AddWithValue("@RoleID", RoleID);
                        command.Parameters.AddWithValue("@AdminID", AdminID);
                        command.Parameters.AddWithValue("@Permission", Permission);
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                            {
                                TB_RoleUserPermissionsID = insertedID;
                            }
                        
                    }
                }
            }
            catch
            {

            }
            return TB_RoleUserPermissionsID;
        }

        public static bool DeleteTB_RoleUserPermissions(Guid RoleUserPermissionsID)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_RoleUserPermissions_Delete", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RoleUserPermissionsID", RoleUserPermissionsID);
                        rowsAffected = command.ExecuteNonQuery();
                       
                    }
                }
            }
            catch
            {

            }
            return (rowsAffected > 0);
        }

        public static bool Update(Guid RoleUserPermissionID, byte RoleID, Guid AdminID, int Permission)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataSettings.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_RoleUserPermissions_Update", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RoleUserPermissionID", RoleUserPermissionID);
                        command.Parameters.AddWithValue("@RoleID", RoleID);
                        command.Parameters.AddWithValue("@AdminID", AdminID);
                        command.Parameters.AddWithValue("@Permission", Permission);  
                        
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