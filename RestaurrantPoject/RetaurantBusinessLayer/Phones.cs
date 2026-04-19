using RestaurantDataAccessLayer;
using RestaurantDataAccessLayer.DTOs.PhoneDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaurantBusinessLayer
{
    internal class Phones
    {
        public enum enMode
        {
            AddNew = 0,
            Update = 1
        }

        public enMode Mode = enMode.AddNew;



        public Guid PhoneID { get; set; }



        public string PhoneNumber { get; set; }



        public Guid UserID { get; set; }


        public Phones()
        {
            this.PhoneID = Guid.Empty;
            this.PhoneNumber = "";
            this.UserID = Guid.Empty;

        }

        protected Phones(
            Guid PhoneID, string PhoneNumber, Guid UserID)
        {
            this.PhoneID = PhoneID;
            this.PhoneNumber = PhoneNumber;
            this.UserID = UserID;
            Mode = enMode.Update;
        }

        public static async Task<IEnumerable<PhoneListDTO>> GetAllTB_Phones(int PageNumber, int RowsPerPage)
        {
            return await Phone.GetAllPhones(PageNumber,RowsPerPage);
        }



        private async Task <bool> _AddNew()
        {

            PhoneID = await Phone.AddNewPhone(new PhoneDTO(Guid.Empty, this.PhoneNumber, this.UserID));
            return PhoneID != Guid.Empty;
        }

        private async Task<bool> _Update()
        {
            return await Phone.UpdatePhone(
                new PhoneDTO(PhoneID, PhoneNumber, UserID));
        }
        public async Task<Phones> GetPhoneInfoByID(Guid PhoneID)
        {
            var dto=await Phone.GetPhoneInfoByID(PhoneID);
            if(dto == null)
            {
                return null;
            }
            return new Phones(dto.PhoneID, dto.PhoneNumber, dto.UserID);
        }
        public async Task<Phones> GetPhoneInfoByPhoneNumber(string PhoneNumber)
        {
            var dto = await Phone.GetPhoneInfoByPhoneNumber(PhoneNumber);
            if (dto == null)
            {
                return null;
            }
            return new Phones(dto.PhoneID, dto.PhoneNumber, dto.UserID);
        }
        public async Task<bool> DeletePhone()
        {
            return await Phone.DeletePhone(PhoneID);
        }
        public async Task<bool> IsExistByPhoneID()
        {
            return await Phone.IsPhoneExists(PhoneID);
        }
        public async Task<bool> IsExistByPhoneNumber()
        {
            return await Phone.IsPhoneExists(PhoneNumber);
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