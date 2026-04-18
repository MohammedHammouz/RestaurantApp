using System;

namespace RestaurantDataAccessLayer.DTOs.AddressDTO
{
    public class AddressDTO
    {
        public class GeographyDTO
        {
            public double? Latitude { set; get; }
            public double? Longitude { set; get; }

            public GeographyDTO(double Latitude, double Longitude)
            {
                this.Latitude = Latitude;
                this.Longitude = Longitude;
            }

            public GeographyDTO() { }
        }

        public Guid AddressID { set; get; }
        public string State { set; get; }
        public string PostalCode { set; get; }
        public string StreetAddress { set; get; }
        public GeographyDTO GPSLocation { set; get; }
        public Guid CountryID { set; get; }
        public Guid CityID { set; get; }

        public AddressDTO(Guid AddressID, string State, string PostalCode, string StreetAddress, GeographyDTO GPSLocation, Guid CountryID, Guid CityID)
        {
            this.AddressID = AddressID;
            this.State = State;
            this.PostalCode = PostalCode;
            this.StreetAddress = StreetAddress;
            this.GPSLocation = GPSLocation;
            this.CountryID = CountryID;
            this.CityID = CityID;
        }

        public AddressDTO()
        { }
    }
}
