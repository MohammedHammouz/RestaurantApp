using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using RestaurantDataAccessLayer.DTOs.AddressDTO;

namespace RestaurantDataAccessLayer
{
   public static class Address
    {
        public static async Task<AddressDTO> GetAddressInfoByAddressID(Guid AddressID)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();

                    Parameters.Add("AddressID", AddressID);

                    return (await Db.QueryAsync<AddressDTO, AddressDTO.GeographyDTO, AddressDTO>(
                        "sp_Addresses_GetByAddressID",
                        (address, location) =>
                        {
                            address.GPSLocation = location;
                            return address;
                        },
                        splitOn: "Lat",
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

        public static async Task<Guid> AddNewAddress(AddressDTO Address)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();

                    if (string.IsNullOrWhiteSpace(Address.State)) Parameters.Add("State", null);
                    else Parameters.Add("State", Address.State);

                    if (string.IsNullOrWhiteSpace(Address.PostalCode)) Parameters.Add("PostalCode", null);
                    else Parameters.Add("PostalCode", Address.PostalCode);
                    
                    Parameters.Add("StreetAddress", Address.StreetAddress);
                    if (!Address.GPSLocation.Latitude.HasValue || Address.GPSLocation.Longitude.HasValue) Parameters.Add("GPSLocation", null);
                    else Parameters.Add("GPSLocation", $"POINT({Address.GPSLocation.Latitude} {Address.GPSLocation.Longitude})");

                    Parameters.Add("CountryID", Address.CountryID);
                    Parameters.Add("CityID", Address.CityID);
                    
                    Parameters.Add("AddressID", dbType: DbType.Guid, direction: ParameterDirection.Output);

                    await Db.ExecuteAsync("sp_Addresses_AddNew", Parameters, commandType: CommandType.StoredProcedure);

                    return Parameters.Get<Guid>("AddressID");
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

        public static async Task<bool> UpdateAddress(AddressDTO Address)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();

                    Parameters.Add("AddressID", Address.AddressID);
                    if (string.IsNullOrWhiteSpace(Address.State)) Parameters.Add("State", null);
                    else Parameters.Add("State", Address.State);

                    if (string.IsNullOrWhiteSpace(Address.PostalCode)) Parameters.Add("PostalCode", null);
                    else Parameters.Add("PostalCode", Address.PostalCode);

                    Parameters.Add("StreetAddress", Address.StreetAddress);
                    if (!Address.GPSLocation.Latitude.HasValue || Address.GPSLocation.Longitude.HasValue) Parameters.Add("GPSLocation", null);
                    else Parameters.Add("GPSLocation", $"POINT({Address.GPSLocation.Latitude} {Address.GPSLocation.Longitude})");

                    Parameters.Add("CountryID", Address.CountryID);
                    Parameters.Add("CityID", Address.CityID);

                    Parameters.Add("RowsEffected", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    await Db.ExecuteAsync("SP_UpdateAddress", Parameters, commandType: CommandType.StoredProcedure);
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

        public static async Task<bool> DeleteAddress(Guid AddressID)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("AddressID", AddressID);

                    Parameters.Add("RowsEffected", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    await Db.ExecuteAsync("sp_Addresses_Delete", Parameters, commandType: CommandType.StoredProcedure);
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

        public static async Task<IEnumerable<AddressDTO>> GetAllAddresses(int PageNumber, int RowsPerPage)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("PageNumber", PageNumber);
                    Parameters.Add("RowsPerPage", RowsPerPage);


                    return await Db.QueryAsync<AddressDTO>(
                                "sp_Addresses_GetAll",
                                commandType: CommandType.StoredProcedure, 
                                param: Parameters);
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

            return new List<AddressDTO>();
        }

        public static async Task<bool> IsAddressExists(Guid AddressID)
        {
            try
            {
                using (IDbConnection Db = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    var Parameters = new DynamicParameters();
                    Parameters.Add("AddressID", AddressID);
                    Parameters.Add("IsExist", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await Db.ExecuteAsync("sp_Addresses_CheckExists", Parameters, commandType: CommandType.StoredProcedure);
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