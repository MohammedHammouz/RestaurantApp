using RestaurantDataAccessLayer;
using RestaurantDataAccessLayer.DTOs.AddressDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static RestaurantDataAccessLayer.DTOs.AddressDTO.AddressDTO;

namespace RetaurantBusinessLayer
{
    internal class Addresses
    {
        public enum enMode
        {
            AddNew = 0,
            Update = 1
        }
        public Guid AddressID { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string StreetAddress { get; set; }
        public AddressDTO.GeographyDTO GPSLocation { get; set; }
        public Guid CountryID { get; set; }
        public Guid CityID { get; set; }

        public enMode Mode = enMode.AddNew;



        public Addresses()
        {

        }

        private Addresses(Guid AddressID,string State,string PostalCode,string StreetAddress,AddressDTO.GeographyDTO GPSLocation,Guid CountryID,Guid CityID)
        {
            this.AddressID = AddressID;
            this.State = State;
            this.PostalCode = PostalCode;
            this.StreetAddress = StreetAddress;
            this.GPSLocation = GPSLocation;
            this.CountryID = CountryID;
            this.CityID = CityID;
            Mode = enMode.Update;
        }

        public static async Task<IEnumerable<AddressDTO>> GetAllAddresses(int PageNumber, int RowsPerPage)
        {
            return await Address.GetAllAddresses(PageNumber,RowsPerPage);
        }



        private  async Task<bool> _AddNew()
        {

            AddressID = await Address.AddNewAddress(new AddressDTO(Guid.Empty, State, PostalCode, StreetAddress, GPSLocation, CountryID, CityID));
            return AddressID != Guid.Empty;
        }

        private async Task<bool> _Update()
        {
            return await Address.UpdateAddress(new AddressDTO(AddressID,State,PostalCode,StreetAddress,GPSLocation,CountryID,CityID));
        }
        public static async Task<bool> DeleteAddress(Guid AddressID)
        {
           return await Address.DeleteAddress(AddressID);
        }
        public static async Task<bool> IsAddressExists(Guid AddressID)
        {
            return await Address.IsAddressExists(AddressID);
        }
        public static async Task<Addresses> GetAddressInfoByAddressID(Guid AddressID) {
            string State, PostalCode, StreetAddress;
            AddressDTO.GeographyDTO GPSLocation;
            Guid CountryID ,CityID;
            AddressDTO addressDTO = await Address.GetAddressInfoByAddressID(AddressID);
            State = addressDTO.State;
            PostalCode = addressDTO.PostalCode;
            StreetAddress = addressDTO.StreetAddress;
            GPSLocation = addressDTO.GPSLocation;
            CountryID = addressDTO.CountryID;
            CityID = addressDTO.CityID;
            return new Addresses(AddressID, State, PostalCode, StreetAddress, GPSLocation, CountryID, CityID);
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
