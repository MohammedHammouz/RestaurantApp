using System;

namespace RestaurantDataAccessLayer.DTOs.PhoneDTO
{
    public class PhoneListDTO
    {
        public Guid PhoneID { set; get; }
        public string PhoneNumber { set; get; }
        public string UserName { set; get; }

        public PhoneListDTO(Guid PhoneID, string PhoneNumber, string UserName)
        {
            this.PhoneID = PhoneID;
            this.PhoneNumber = PhoneNumber;
            this.UserName = UserName;
        }

        public PhoneListDTO() { }
    }
}
