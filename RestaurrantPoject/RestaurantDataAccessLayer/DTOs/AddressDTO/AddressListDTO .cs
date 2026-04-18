using System;

namespace RestaurantDataAccessLayer.DTOs.AddressDTO
{
    public class AddressListDTO
    {

        public Guid AddressID { set; get; }
        public string State { set; get; }
        public string PostalCode { set; get; }
        public string StreetAddress { set; get; }
        public string CountryName { set; get; }
        public string CityName { set; get; }

        public AddressListDTO(Guid AddressID, string State, string PostalCode, string StreetAddress, string CountryName, string CityName)
        {
            this.AddressID = AddressID;
            this.State = State;
            this.PostalCode = PostalCode;
            this.StreetAddress = StreetAddress;
            this.CountryName = CountryName;
            this.CityName = CityName;
        }

        public AddressListDTO()
        { }
    }
}
