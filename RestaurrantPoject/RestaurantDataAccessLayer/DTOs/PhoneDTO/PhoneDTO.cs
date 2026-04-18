using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDataAccessLayer.DTOs.PhoneDTO
{
    public class PhoneDTO
    {
        public Guid PhoneID { set; get; }
        public string PhoneNumber { set; get; }
        public Guid UserID { set; get; }

        public PhoneDTO(Guid PhoneID, string PhoneNumber, Guid UserID)
        {
            this.PhoneID = PhoneID;
            this.PhoneNumber = PhoneNumber;
            this.UserID = UserID;
        }

        public PhoneDTO() { }
    }
}
