using System;

namespace RestaurantDataAccessLayer.DTOs.CityDTO
{
    public class CityListDTO
    {
        public Guid CityID { set; get; }
        public string CityName { set; get; }
        public string CountryName { set; get; }

        public CityListDTO(Guid CityID, string CityName, string CounryName)
        {
            this.CityID = CityID;
            this.CityName = CityName;
            this.CountryName = CountryName;
        }

        public CityListDTO() { }
    }
}
