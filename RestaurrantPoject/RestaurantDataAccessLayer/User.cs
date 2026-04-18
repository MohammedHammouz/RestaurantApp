using System;
using Dapper;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using RestaurantDataAccessLayer.DTOs.UserDTO;

namespace RestaurantDataAccessLayer
{
    public static class User
    {
        public static async Task<UserDTO> GetUserInfoByID(Guid UserID)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("UserID", UserID);

                    return (await Db.QueryAsync<UserDTO>(
                        "sp_Users_GetByID",
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

        public static async Task<Guid> AddNewUser(UserDTO User)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("FirstName", User.FirstName);
                    Parameters.Add("SecondName", User.SecondName);
                    Parameters.Add("LastName", User.LastName);
                    Parameters.Add("Gender", User.Gender);

                    if (string.IsNullOrWhiteSpace(User.ImagePath)) Parameters.Add("ImagePath", null);
                    else Parameters.Add("ImagePath", User.ImagePath);

                    Parameters.Add("DateOfBirth", User.DateOfBirth);

                    if (User.AddressID.HasValue) Parameters.Add("AddressID", null);
                    else Parameters.Add("AddressID", User.AddressID.Value);

                    Parameters.Add("UserID", dbType: DbType.Guid, direction: ParameterDirection.Output);

                    await Db.ExecuteAsync("sp_Users_AddNew", Parameters, commandType: CommandType.StoredProcedure);

                    return Parameters.Get<Guid>("UserID");
                }
            }
            catch (Exception ex)
            {
                clsLogger.ErrorMessage(ex.Message, ex, System.Diagnostics.EventLogEntryType.Error);
            }

            return Guid.Empty;
        }

        public static async Task<bool> UpdateUser(UserDTO User)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();

                    Parameters.Add("UserID", User.FirstName);
                    Parameters.Add("FirstName", User.FirstName);
                    Parameters.Add("SecondName", User.SecondName);
                    Parameters.Add("LastName", User.LastName);
                    Parameters.Add("Gender", User.Gender);

                    if (string.IsNullOrWhiteSpace(User.ImagePath)) Parameters.Add("ImagePath", null);
                    else Parameters.Add("ImagePath", User.ImagePath);

                    Parameters.Add("DateOfBirth", User.DateOfBirth);

                    if (User.AddressID.HasValue) Parameters.Add("AddressID", null);
                    else Parameters.Add("AddressID", User.AddressID.Value);

                    Parameters.Add("RowsEffected", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    await Db.ExecuteAsync("sp_Users_Update", Parameters, commandType: CommandType.StoredProcedure);

                    return (Parameters.Get<int>("RowsEffected") > 0);
                }
            }
            catch (Exception ex)
            {
                clsLogger.ErrorMessage(ex.Message, ex, System.Diagnostics.EventLogEntryType.Error);
            }

            return false;
        }

        public static async Task<bool> DeleteUser(Guid UserID)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("UserID", UserID);
                    Parameters.Add("RowsEffected", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    await Db.ExecuteAsync("sp_Users_Delete", Parameters, commandType: CommandType.StoredProcedure);
                    return (Parameters.Get<int>("RowsEffected") > 0);
                }
            }
            catch (Exception ex)
            {
                clsLogger.ErrorMessage(ex.Message, ex, System.Diagnostics.EventLogEntryType.Error);
            }

            return false;
        }

        public static async Task<IEnumerable<UserListDTO>> GetAllUsers(int PageNumber, int RowsPerPage)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("PageNumber", PageNumber);
                    Parameters.Add("RowsPerPage", RowsPerPage);

                    return await Db.QueryAsync<UserListDTO>("sp_Users_GetAll", commandType: CommandType.StoredProcedure, param: Parameters);
                }
            }
            catch (Exception ex)
            {
                clsLogger.ErrorMessage(ex.Message, ex, System.Diagnostics.EventLogEntryType.Error);
            }

            return new List<UserListDTO>();
        }

        public static async Task<bool> IsUserExists(Guid UserID)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("UserID", UserID);
                    Parameters.Add("IsExist", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await Db.ExecuteAsync("sp_Users_CheckExists", Parameters, commandType: CommandType.StoredProcedure);
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