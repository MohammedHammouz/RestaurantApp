using System;
using Dapper;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using RestaurantDataAccessLayer.DTOs.RoleUserPermissionDTO;

namespace RestaurantDataAccessLayer
{
    public static class RoleUserPermission
    {
        public static async Task<RoleUserPermissionDTO> GetRoleUserPermissionInfo(Guid RoleUserPermissionID)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("RoleUserPermissionID", RoleUserPermissionID);

                    return (await Db.QueryAsync<RoleUserPermissionDTO>(
                        "sp_RoleUserPermissions_GetById",
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

        public static async Task<RoleUserPermissionDTO> GetRoleUserPermissionInfoByAdminID(Guid AdminID)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("AdminID", AdminID);

                    return (await Db.QueryAsync<RoleUserPermissionDTO>(
                        "sp_RoleUserPermissions_GetByAdminID",
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

        public static async Task<Guid> AddNewRoleUserPermission(RoleUserPermissionDTO RoleUserPermission)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();

                    Parameters.Add("RoleID", RoleUserPermission.RoleID);
                    Parameters.Add("Permission", RoleUserPermission.Permission);
                    Parameters.Add("AdminID", RoleUserPermission.AdminID);
                    Parameters.Add("RoleUserPermissionID", DbType.Guid, direction: ParameterDirection.Output);

                    await Db.ExecuteAsync("sp_RoleUserPermissions_AddNew", Parameters, commandType: CommandType.StoredProcedure);
                    return Parameters.Get<Guid>("RoleUserPermissionID");
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

        public static async Task<bool> UpdateRoleUserPermission(RoleUserPermissionDTO RoleUserPermission)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();

                    Parameters.Add("RoleUserPermissionID", RoleUserPermission.RoleUserPermissionID);
                    Parameters.Add("RoleID", RoleUserPermission.RoleID);
                    Parameters.Add("Permission", RoleUserPermission.Permission);
                    Parameters.Add("AdminID", RoleUserPermission.AdminID);
                    Parameters.Add("RowsEffected", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    await Db.ExecuteAsync("sp_RoleUserPermissions_Update", Parameters, commandType: CommandType.StoredProcedure);

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

        public static async Task<bool> DeleteRoleUserPermission(Guid RoleUserPermissionID)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("RoleUserPermissionID", RoleUserPermissionID);
                    Parameters.Add("RowsEffected", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    await Db.ExecuteAsync("sp_RoleUserPermissions_Delete", Parameters, commandType: CommandType.StoredProcedure);
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

        public static async Task<IEnumerable<RoleUserPermissionListDTO>> GetAllRoleUserPermissions(int PageNumber, int RowsPerPage)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("PageNumber", PageNumber);
                    Parameters.Add("RowsPerPage", RowsPerPage);

                    return await Db.QueryAsync<RoleUserPermissionListDTO>("sp_RoleUserPermissions_GetAll", commandType: CommandType.StoredProcedure, param: Parameters);
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

            return new List<RoleUserPermissionListDTO>();
        }

        public static async Task<bool> IsRoleUserPermissionExistsByRoleUserPermissionID(Guid RoleUserPermissionID)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("RoleUserPermissionID", RoleUserPermissionID);
                    Parameters.Add("IsExist", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await Db.ExecuteAsync("sp_RoleUserPermissions_CheckExists", Parameters, commandType: CommandType.StoredProcedure);
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