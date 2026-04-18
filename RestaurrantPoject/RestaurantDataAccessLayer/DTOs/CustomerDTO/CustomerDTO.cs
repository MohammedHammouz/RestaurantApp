using System;

namespace RestaurantDataAccessLayer.DTOs.CustomerDTO
{
    public class CustomerDTO
    {
        public Guid CustomerID { set; get; }
        public string FullName { set; get; }
        public DateTime CreatedAt { set; get; }
        public int LoyalityPoints { set; get; }
        public string PhoneNumber { set; get; }
        public Guid CreatedByAdminID { set; get; }

        public CustomerDTO(Guid CustomerID, string FullName, DateTime CreatedAt, int LoyalityPoints, string PhoneNumber, Guid CreatedByAdminID)
        {
            this.CustomerID = CustomerID;
            this.FullName = FullName;
            this.CreatedAt = CreatedAt;
            this.LoyalityPoints = LoyalityPoints;
            this.PhoneNumber = PhoneNumber;
            this.CreatedByAdminID = CreatedByAdminID;
        }

        public CustomerDTO() { }
    }
}
