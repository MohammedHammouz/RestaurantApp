using System;
using System.Linq;
using System.Threading.Tasks;
using RestaurantDataAccessLayer.DTOs.AddressDTO;

namespace RetaurantBusinessLayer
{
    public class Address
    {
        public class Geography
        {
            public double? Latitude { set; get; }
            public double? Longitude { set; get; }

            public Geography(double? Latitude, double? Longitude)
            {
                this.Latitude = Latitude;
                this.Longitude = Longitude;
            }
        }

        private enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode;

        public Guid AddressID { set; get; }
        public string State { set; get; }
        public string PostalCode { set; get; }
        public string StreetAddress { set; get; }
        public Geography GPSLocation { set; get; }
        public Guid CountryID { set; get; }
        public Guid CityID { set; get; }

        public Address()
        {
            this._Mode = enMode.AddNew;
        }

        private Address(AddressDTO AddressDTO)
        {
            this.AddressID = AddressDTO.AddressID;
            this.State = AddressDTO.State;
            this.PostalCode = AddressDTO.PostalCode;
            this.StreetAddress = AddressDTO.StreetAddress;
            this.GPSLocation = new Geography(AddressDTO.GPSLocation.Latitude, AddressDTO.GPSLocation.Longitude);
            this.CountryID = AddressDTO.CountryID;
            this.CityID = AddressDTO.CityID;

            this._Mode = enMode.Update;
        }

        private async Task<bool> AddNewAddress()
        {
            this.AddressID = await RestaurantDataAccessLayer.Address.AddNewAddress(new AddressDTO(this.State, this.PostalCode, this.StreetAddress, new AddressDTO.GeographyDTO(this.GPSLocation.Latitude, this.GPSLocation.Longitude), this.CountryID, this.CityID));

            return (this.AddressID != Guid.Empty);
        }

        private async Task<bool> UpdateAddress()
        {
            return await RestaurantDataAccessLayer.Address.UpdateAddress(new AddressDTO(this.AddressID, this.State, this.PostalCode, this.StreetAddress, new AddressDTO.GeographyDTO(this.GPSLocation.Latitude, this.GPSLocation.Longitude), this.CountryID, this.CityID));
        }

        public async Task<bool> Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if(await AddNewAddress())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    break;
                case enMode.Update:
                    return await UpdateAddress();
            }

            return false;
        }

        public static async Task<Address> GetAddressInfoByID(Guid AddressID)
        {
            var Address = await RestaurantDataAccessLayer.Address.GetAddressInfoByAddressID(AddressID);

            if (Address == null)
                return null;

            return new Address(Address);
        }

        public static async Task<object> GetAllAddresses(int PageNumber = 1, int RowsPerPage = 50)
        {
            var Addresses = await RestaurantDataAccessLayer.Address.GetAllAddresses(PageNumber, RowsPerPage);

            return Addresses.Select(Address => new 
            {
                Address.AddressID,
                Address.State,
                Address.PostalCode,
                Address.StreetAddress,
                Address.CountryName,
                Address.CityName
            }).ToList();
        }

        public static async Task<bool> IsAddressExists(Guid AddressID)
        {
            return await RestaurantDataAccessLayer.Address.IsAddressExists(AddressID);
        }
    }
}
