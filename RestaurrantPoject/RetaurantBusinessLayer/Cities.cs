using RestaurantDataAccessLayer;
using RestaurantDataAccessLayer.DTOs.CityDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaurantBusinessLayer
{
    public class Cities
    {
        public enum enMode
        {
            AddNew = 0,
            Update = 1
        }
        
        public enMode Mode = enMode.AddNew;



        public Guid CityID { get; set; }



        public string CityName { get; set; }



        public Guid CountryID { get; set; }

        public Countries countries { get; set; }
        public Cities()
        {
            this.CityID = Guid.Empty;
            this.CityName = "";
            this.CountryID = Guid.Empty;

        }

        private Cities(Guid CityID, string CityName, Guid CountryID)
        {
            this.CityID = CityID;
            this.CityName = CityName;
            this.CountryID = CountryID;
            Mode = enMode.Update;
        }
        public async Task LoadCountry()
        {
            countries = await Countries.GetCountryInfoByCountryID(CountryID);
        }
        public static async Task<IEnumerable<CityListDTO>> GetAllCities(int PageNumber, int RowsPerPage)
        {
            return await City.GetAllCities(PageNumber,RowsPerPage);
        }
        public async Task<Cities> GetCityInfoByCityID(Guid CityID)
        {
            CityDTO DTO = await City.GetCityInfoByCityID(CityID);
            if (DTO == null)
                return null;
            return new Cities(CityID, DTO.CityName, DTO.CountryID);
        }
        private async Task<bool> _AddNew()
        {
            CityDTO DTO = new CityDTO(Guid.Empty, CityName, CountryID);
            CityID =await City.AddNewCity(DTO);
            return CityID != Guid.Empty;
        }
        private async Task<bool> _Update()
        {
            CityDTO DTO = new CityDTO(CityID, CityName, CountryID);
            return await City.UpdateCity(DTO);
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
        public static async Task<bool> DeleteCity(Guid CityID)
        {
            return await City.DeleteCity(CityID);
        }
        public static async Task<bool> IsCityExists(Guid CityID)
        {
           return await City.IsCityExists(CityID);
        }
    }
}
