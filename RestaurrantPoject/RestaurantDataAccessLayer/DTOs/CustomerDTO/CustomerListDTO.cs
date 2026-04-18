using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDataAccessLayer.DTOs.CustomerDTO
{
    public class CustomerListDTO
    {
        public Guid CustomerID { set; get; }
        public string FullName { set; get; }
        public DateTime CreatedAt { set; get; }
        public int LoyalityPoints { set; get; }
        public string PhoneNumber { set; get; }
        public string CreatedBy { set; get; }

        public CustomerListDTO(Guid CustomerID, string FullName, DateTime CreatedAt, int LoyalityPoints, string PhoneNumber, string CreatedBy)
        {
            this.CustomerID = CustomerID;
            this.FullName = FullName;
            this.CreatedAt = CreatedAt;
            this.LoyalityPoints = LoyalityPoints;
            this.PhoneNumber = PhoneNumber;
            this.CreatedBy = CreatedBy;
        }

        public CustomerListDTO() { }
    }
}
