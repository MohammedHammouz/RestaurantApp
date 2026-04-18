using System;

namespace RestaurantDataAccessLayer.DTOs.CityDTO
{
    public class CityDTO
    {
        public Guid CityID { set; get; }
        public string CityName { set; get; }
        public Guid CountryID { set; get; }

        public CityDTO(Guid CityID, string CityName, Guid CountryID)
        {
            this.CityID = CityID;
            this.CityName = CityName;
            this.CountryID = CountryID;
        }

        public CityDTO() { }
    }
}
