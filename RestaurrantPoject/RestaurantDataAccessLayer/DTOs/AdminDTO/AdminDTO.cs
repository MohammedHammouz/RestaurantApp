using System;

namespace RestaurantDataAccessLayer.DTOs.Admins
{
    public class AdminDTO
    {
        public Guid AdminID { set; get; }
        public Guid UserID { set; get; }
        public decimal MonthlySalary { set; get; }
    }
}
