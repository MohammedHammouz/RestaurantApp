using System;

namespace RestaurantDataAccessLayer.DTOs.CountryDTO
{
    public class CountryDTO
    {
        public Guid CountryID { set; get; }
        public string CountryName { set; get; }

        public CountryDTO(Guid CountryID, string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
        }

        public CountryDTO()
        { }
    }
}