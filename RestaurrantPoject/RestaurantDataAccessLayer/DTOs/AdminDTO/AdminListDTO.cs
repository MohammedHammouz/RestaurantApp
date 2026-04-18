using System;

namespace RestaurantDataAccessLayer.DTOs.Admins
{
    public class AdminListDTO
    {
        public Guid AdminID { set; get; }
        public string FullName { set; get; }
        public decimal MonthlySalary { set; get; }
    }
}
