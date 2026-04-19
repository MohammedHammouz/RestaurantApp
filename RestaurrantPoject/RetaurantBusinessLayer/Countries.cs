using RestaurantDataAccessLayer;
using RestaurantDataAccessLayer.DTOs.CountryDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaurantBusinessLayer
{
    public class Countries
    {
        public enum enMode
        {
            AddNew = 0,
            Update = 1
        }

        public enMode Mode = enMode.AddNew;



        public Guid CountryID { get; set; }



        public string CountryName { get; set; }


        public Countries()
        {
            this.CountryID = Guid.Empty;
            this.CountryName = "";

        }

        private Countries(
            Guid CountryID, string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
            Mode = enMode.Update;
        }

        public static async Task<IEnumerable<CountryDTO>> GetAllCountries(int PageNumber, int RowsPerPage)
        {
            return await Country.GetAllCountries(PageNumber,RowsPerPage);
        }
        public static async Task<Countries> GetCountryInfoByCountryID(Guid CountryID)
        {
            var DTO = await Country.GetCountryInfoByCountryID(CountryID);
            
            return new Countries(CountryID, DTO.CountryName);
        }
        public async Task<Countries> GetCountryInfoByCountryName(string CountryName)
        {
            CountryDTO DTO = await Country.GetCountryInfoByCountryName(CountryName);
            CountryID = DTO.CountryID;
            return new Countries(CountryID, CountryName);
        }
        
        private async Task <bool> _AddNew()
        {
            CountryDTO dto = new CountryDTO(Guid.Empty,CountryName);
            
            CountryID =await Country.AddNewCountry(dto);
            return CountryID != Guid.Empty;
        }
        private async Task<bool> _Update()
        {
            CountryDTO dto = new CountryDTO(Guid.Empty, CountryName);
            return await Country.UpdateCountry(dto);
        }
          
        public async Task<bool> DeleteCountry(Guid CountryID)
        {
            return await Country.DeleteCountry(CountryID);
        }
        public async Task<bool> IsCountryExistsByID(Guid CountryID)
        {
            return await Country.IsCountryExists(CountryID);
        }
        public async Task<bool> IsCountryExistsByName(string CountryName)
        {
            return await Country.IsCountryExists(CountryName);
        }

        public async Task<bool> Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (await _AddNew())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return await _Update();
            }
            return false;
        }
    }
}
