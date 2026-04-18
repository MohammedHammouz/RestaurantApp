using System;
using Dapper;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using RestaurantDataAccessLayer.DTOs.CountryDTO;

namespace RestaurantDataAccessLayer
{

    public static class Country
    {
        public static async Task<CountryDTO> GetCountryInfoByCountryID(Guid CountryID)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("CountryID", CountryID);

                    return (await Db.QueryAsync<CountryDTO>(
                        "sp_Countries_GetById",
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

        public static async Task<CountryDTO> GetCountryInfoByCountryName(string CountryName)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("CountryName", CountryName);

                    return (await Db.QueryAsync<CountryDTO>(
                        "sp_Countries_GetByName",
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

        public static async Task<Guid> AddNewCountry(CountryDTO Country)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("CountryName", Country.CountryName);

                    Parameters.Add("CountryID", dbType: DbType.Guid, direction: ParameterDirection.Output);

                    await Db.ExecuteAsync("sp_Countries_AddNew", Parameters, commandType: CommandType.StoredProcedure);
                    return Parameters.Get<Guid>("CountryID");
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

        public static async Task<bool> UpdateCountry(CountryDTO Country)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();

                    Parameters.Add("CountryID", Country.CountryName);
                    Parameters.Add("CountryName", Country.CountryName);
                    Parameters.Add("RowsEffected", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    await Db.ExecuteAsync("sp_Countries_Update", Parameters, commandType: CommandType.StoredProcedure);
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

        public static async Task<bool> DeleteCountry(Guid CountryID)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("CountryID", CountryID);
                    Parameters.Add("RowsEffected", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    await Db.ExecuteAsync("sp_Countries_Delete", Parameters, commandType: CommandType.StoredProcedure);
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

        public static async Task<IEnumerable<CountryDTO>> GetAllCountries(int PageNumber, int RowsPerPage)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("PageNumber", PageNumber);
                    Parameters.Add("RowsPerPage", RowsPerPage);

                    return await Db.QueryAsync<CountryDTO>("sp_Countries_GetAll", commandType: CommandType.StoredProcedure, param: Parameters);
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

            return new List<CountryDTO>();
        }

        public static async Task<bool> IsCountryExists(Guid CountryID)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("CountryID", CountryID);
                    Parameters.Add("IsExist", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await Db.ExecuteAsync("sp_Countries_CheckExistsById", Parameters, commandType: CommandType.StoredProcedure);
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

        public static async Task<bool> IsCountryExists(string CountryName)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("CountryName", CountryName);
                    Parameters.Add("IsExist", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await Db.ExecuteAsync("sp_Countries_CheckExistsByName", Parameters, commandType: CommandType.StoredProcedure);
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