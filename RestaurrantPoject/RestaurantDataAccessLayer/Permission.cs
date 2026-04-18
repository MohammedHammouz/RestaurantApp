using System;
using Dapper;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using RestaurantDataAccessLayer.DTOs.PermissionDTO;

namespace RestaurantDataAccessLayer
{
    public static class Permission
    {
        public static async Task<PermissionDTO> GetPermissionInfoByID(int PermissionID)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("PermissionID", PermissionID);

                    return (await Db.QueryAsync<PermissionDTO>(
                        "sp_Permissions_GetById",
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

        public static async Task<int?> AddNewPermission(PermissionDTO Permission)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("PermissionName", Permission.PermissionName);

                    if (string.IsNullOrWhiteSpace(Permission.Description)) Parameters.Add("Description", null);
                    else Parameters.Add("Description", Permission.Description);

                    Parameters.Add("PermissionID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    await Db.ExecuteAsync("sp_Permissions_AddNew", Parameters, commandType: CommandType.StoredProcedure);
                    return Parameters.Get<int>("PermissionID");
                }
            }
            catch (Exception ex)
            {
                clsLogger.ErrorMessage(ex.Message, ex, System.Diagnostics.EventLogEntryType.Error);
            }

            return null;
        }

        public static async Task<bool> UpdatePermission(PermissionDTO Permission)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();

                    Parameters.Add("PermissionID", Permission.PermissionID);
                    Parameters.Add("PermissionName", Permission.PermissionName);

                    if (string.IsNullOrWhiteSpace(Permission.Description)) Parameters.Add("Description", null);
                    else Parameters.Add("Description", Permission.Description);

                    Parameters.Add("RowsEffected", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    await Db.ExecuteAsync("sp_Permissions_Update", Parameters, commandType: CommandType.StoredProcedure);
                    return (Parameters.Get<int>("RowsEffected") > 0);
                }
            }
            catch (Exception ex)
            {
                clsLogger.ErrorMessage(ex.Message, ex, System.Diagnostics.EventLogEntryType.Error);
            }

            return false;
        }

        public static async Task<bool> DeletePermission(Guid PermissionID)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("PermissionID", PermissionID);
                    Parameters.Add("RowsEffected", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    await Db.ExecuteAsync("sp_Permissions_Delete", Parameters, commandType: CommandType.StoredProcedure);
                    return (Parameters.Get<int>("RowsEffected") > 0);
                }
            }
            catch (Exception ex)
            {
                clsLogger.ErrorMessage(ex.Message, ex, System.Diagnostics.EventLogEntryType.Error);
            }

            return false;
        }

        public static async Task<IEnumerable<PermissionDTO>> GetAllPermissions(int PageNumber, int RowsPerPage)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("PageNumber", PageNumber);
                    Parameters.Add("RowsPerPage", RowsPerPage);

                    return await Db.QueryAsync<PermissionDTO>("sp_Permissions_GetAll", commandType: CommandType.StoredProcedure, param: Parameters);
                }
            }
            catch (Exception ex)
            {
                clsLogger.ErrorMessage(ex.Message, ex, System.Diagnostics.EventLogEntryType.Error);
            }

            return new List<PermissionDTO>();
        }

        public static async Task<bool> IsPermissionExists(Guid PermissionID)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("PermissionID", PermissionID);
                    Parameters.Add("IsExist", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await Db.ExecuteAsync("sp_Permissions_CheckExists", Parameters, commandType: CommandType.StoredProcedure);
                    return Parameters.Get<bool>("IsExist");
                }
            }
            catch (Exception ex)
            {
                clsLogger.ErrorMessage(ex.Message, ex, System.Diagnostics.EventLogEntryType.Error);
            }

            return false;
        }

    }
}