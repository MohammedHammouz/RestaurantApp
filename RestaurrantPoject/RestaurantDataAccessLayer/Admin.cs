using System;
using Dapper;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using RestaurantDataAccessLayer.DTOs.Admins;

namespace RestaurantDataAccessLayer
{
    public static class Admin
    {
        public static async Task<AdminDTO> GetAdminInfoByAdminID(Guid AdminID)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("AdminID", AdminID);

                    return (await Db.QueryAsync<AdminDTO>(
                        "sp_Admins_GetByAdminID",
                        commandType: CommandType.StoredProcedure,
                        param: Parameters)).FirstOrDefault();
                }
            }
            catch (SqlException ex)
            {
                clsLogger.ErrorMessage(ex.Message, ex, System.Diagnostics.EventLogEntryType.Error);
            }
            catch (Exception ex)
            {

                clsLogger.ErrorMessage(ex.Message, ex, System.Diagnostics.EventLogEntryType.Error);
            }

            return null;
        }

        public static async Task<Guid> AddNewAdmin(CreateAdminDTO Admin)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("FirstName", Admin.FirstName);
                    Parameters.Add("SecondName", Admin.SecondName);
                    Parameters.Add("LastName", Admin.LastName);
                    Parameters.Add("Gender", Admin.Gender);

                    if (string.IsNullOrWhiteSpace(Admin.ImagePath)) Parameters.Add("ImagePath", null);
                    else Parameters.Add("ImagePath", Admin.ImagePath);

                    Parameters.Add("DateOfBirth", Admin.DateOfBirth);

                    if (Admin.AddressID.HasValue) Parameters.Add("AddressID", null);
                    else Parameters.Add("AddressID", Admin.AddressID);

                    Parameters.Add("MonthlySalary", Admin.MonthlySalary);

                    Parameters.Add("AdminID", dbType: DbType.Guid, direction: ParameterDirection.Output);

                    await Db.ExecuteAsync("sp_Admins_AddNew", Parameters, commandType: CommandType.StoredProcedure);
                    return Parameters.Get<Guid>("AdminID");
                }
            }
            catch (SqlException ex)
            {
                clsLogger.ErrorMessage(ex.Message, ex, System.Diagnostics.EventLogEntryType.Error);
            }
            catch (Exception ex)
            {
                clsLogger.ErrorMessage(ex.Message, ex, System.Diagnostics.EventLogEntryType.Error);
            }

            return Guid.Empty;
        }

        public static async Task<Guid> LinkUserAdmin(AdminDTO Admin)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("UserID", Admin.UserID);
                    Parameters.Add("MonthlySalary", Admin.MonthlySalary);

                    Parameters.Add("AdminID", dbType: DbType.Guid, direction: ParameterDirection.Output);

                    await Db.ExecuteAsync("sp_Admins_LinkUserAdmin", Parameters, commandType: CommandType.StoredProcedure);
                    return Parameters.Get<Guid>("AdminID");
                }
            }
            catch (SqlException ex)
            {
                clsLogger.ErrorMessage(ex.Message, ex, System.Diagnostics.EventLogEntryType.Error);
            }
            catch (Exception ex)
            {
                clsLogger.ErrorMessage(ex.Message, ex, System.Diagnostics.EventLogEntryType.Error);
            }

            return Guid.Empty;
        }

        public static async Task<bool> UnLinkUserAdmin(Guid AdminID)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("AdminID", AdminID);

                    Parameters.Add("RowsEffected", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    await Db.ExecuteAsync("sp_Admins_UnLinkUserAdmin", Parameters, commandType: CommandType.StoredProcedure);
                    return (Parameters.Get<int>("RowsEffected") > 0);
                }
            }
            catch (SqlException ex)
            {
                clsLogger.ErrorMessage(ex.Message, ex, System.Diagnostics.EventLogEntryType.Error);
            }
            catch (Exception ex)
            {
                clsLogger.ErrorMessage(ex.Message, ex, System.Diagnostics.EventLogEntryType.Error);
            }

            return false;
        }

        public static async Task<bool> UpdateAdmin(AdminDTO Admin)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();

                    Parameters.Add("AdminID", Admin.AdminID);
                    Parameters.Add("UserID", Admin.UserID);
                    Parameters.Add("MonthlySalary", Admin.MonthlySalary);

                    Parameters.Add("RowsEffected", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    await Db.ExecuteAsync("sp_Admins_Update", Parameters, commandType: CommandType.StoredProcedure);
                    return (Parameters.Get<int>("RowsEffected") > 0);
                }
            }
            catch (SqlException ex)
            {
                clsLogger.ErrorMessage(ex.Message, ex, System.Diagnostics.EventLogEntryType.Error);
            }
            catch (Exception ex)
            {
                clsLogger.ErrorMessage(ex.Message, ex, System.Diagnostics.EventLogEntryType.Error);
            }

            return false;
        }

        public static async Task<bool> DeleteAdmin(int AdminID)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("AdminID", AdminID);
                    Parameters.Add("RowsEffected", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    await Db.ExecuteAsync("sp_Admins_Delete", Parameters, commandType: CommandType.StoredProcedure);
                    return (Parameters.Get<int>("RowsEffected") > 0);
                }
            }
            catch (SqlException ex)
            {
                clsLogger.ErrorMessage(ex.Message, ex, System.Diagnostics.EventLogEntryType.Error);
            }
            catch (Exception ex)
            {
                clsLogger.ErrorMessage(ex.Message, ex, System.Diagnostics.EventLogEntryType.Error);
            }

            return false;
        }

        public static async Task<IEnumerable<AdminListDTO>> GetAllAdmins(int PageNumber, int RowsPerPage)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("PageNumber", PageNumber);
                    Parameters.Add("RowsPerPage", RowsPerPage);

                    return await Db.QueryAsync<AdminListDTO>("sp_Admins_GetAll", commandType: CommandType.StoredProcedure, param: Parameters);
                }
            }
            catch (SqlException ex)
            {
                clsLogger.ErrorMessage(ex.Message, ex, System.Diagnostics.EventLogEntryType.Error);
            }
            catch (Exception ex)
            {
                clsLogger.ErrorMessage(ex.Message, ex, System.Diagnostics.EventLogEntryType.Error);
            }

            return new List<AdminListDTO>();
        }

        public static async Task<bool> IsAdminExistsByAdminID(Guid AdminID)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("AdminID", AdminID);
                    Parameters.Add("IsExist", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await Db.ExecuteAsync("sp_Admins_CheckExists", Parameters, commandType: CommandType.StoredProcedure);
                    return Parameters.Get<bool>("IsExist");
                }
            }
            catch (SqlException ex)
            {
                clsLogger.ErrorMessage(ex.Message, ex, System.Diagnostics.EventLogEntryType.Error);
            }
            catch (Exception ex)
            {
                clsLogger.ErrorMessage(ex.Message, ex, System.Diagnostics.EventLogEntryType.Error);
            }

            return false;
        }

    }
}