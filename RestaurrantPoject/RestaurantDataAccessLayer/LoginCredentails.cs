using System;
using Dapper;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using RestaurantDataAccessLayer.DTOs.Login;

namespace RestaurantDataAccessLayer
{
    public static class LoginCredentails
    {
        public static async Task<LoginDTO> GetLoginInfo(Guid LoginID)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("LoginID", LoginID);

                    return (await Db.QueryAsync<LoginDTO>(
                        "sp_LoginCredentials_GetByLoginID",
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

        public static async Task<LoginDTO> GetLoginInfo(string UserName)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("UserName", UserName);

                    return (await Db.QueryAsync<LoginDTO>(
                        "sp_LoginCredentials_GetByUserName",
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

        public static async Task<int?> AddNewLogin(LoginDTO Login)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();

                    Parameters.Add("AdminID", Login.AdminID);
                    Parameters.Add("UserName", Login.UserName);
                    Parameters.Add("Email", Login.Email);
                    Parameters.Add("PasswordHash", Login.PasswordHash);

                    if (Login.LastLogin.HasValue) Parameters.Add("LastLogin", Login.LastLogin.Value);
                    else Parameters.Add("LastLogin", null);

                    Parameters.Add("CreatedAt", Login.CreatedAt);
                    Parameters.Add("Status", Login.Status);
                    Parameters.Add("LoginID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    await Db.ExecuteAsync("sp_LoginCredentials_AddNew", Parameters, commandType: CommandType.StoredProcedure);
                    return Parameters.Get<int>("LoginID");
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

        public static async Task<bool> UpdateLogin(LoginDTO Login)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();

                    Parameters.Add("LoginID", Login.LoginID);
                    Parameters.Add("AdminID", Login.AdminID);
                    Parameters.Add("UserName", Login.UserName);
                    Parameters.Add("Email", Login.Email);
                    Parameters.Add("PasswordHash", Login.PasswordHash);

                    if (Login.LastLogin.HasValue) Parameters.Add("LastLogin", Login.LastLogin.Value);
                    else Parameters.Add("LastLogin", null);

                    Parameters.Add("CreatedAt", Login.CreatedAt);
                    Parameters.Add("Status", Login.Status);
                    Parameters.Add("RowsEffected", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    await Db.ExecuteAsync("sp_LoginCredentials_Update", Parameters, commandType: CommandType.StoredProcedure);
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

        public static async Task<bool> DeleteLogin(Guid LoginID)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("LoginID", LoginID);
                    Parameters.Add("RowsEffected", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    await Db.ExecuteAsync("sp_LoginCredentials_Delete", Parameters, commandType: CommandType.StoredProcedure);
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
        
        public static async Task<bool> Login(string UserName, string Password)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("UserName", UserName);
                    Parameters.Add("Password", Password);
                    Parameters.Add("IsExist", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await Db.ExecuteAsync("sp_LoginCredentials_Login", Parameters, commandType: CommandType.StoredProcedure);
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

        public static async Task<IEnumerable<LoginDTO>> GetAllLogins(int PageNumber, int RowsPerPage)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("PageNumber", PageNumber);
                    Parameters.Add("RowsPerPage", RowsPerPage);

                    return await Db.QueryAsync<LoginDTO>("sp_LoginCredentials_GetAll", commandType: CommandType.StoredProcedure, param: Parameters);
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

            return new List<LoginDTO>();
        }

        public static async Task<bool> IsLoginExists(Guid LoginID)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("LoginID", LoginID);
                    Parameters.Add("IsExist", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await Db.ExecuteAsync("sp_LoginCredentials_CheckExistsByID", Parameters, commandType: CommandType.StoredProcedure);
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

        public static async Task<bool> IsLoginExists(string UserName)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("UserName", UserName);
                    Parameters.Add("IsExist", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await Db.ExecuteAsync("sp_LoginCredentials_CheckExistsByUserName", Parameters, commandType: CommandType.StoredProcedure);
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