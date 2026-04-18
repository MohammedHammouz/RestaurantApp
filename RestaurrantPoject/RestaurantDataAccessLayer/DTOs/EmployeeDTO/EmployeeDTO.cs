using System;
using System;

namespace RestaurantDataAccessLayer.DTOs.EmployeeDTO
{
    public class EmployeeDTO
    {
        public Guid EmployeeID { set; get; }
        public DateTime HireDate { set; get; }
        public decimal MonthlySalary { set; get; }
        public Guid UserID { set; get; }
        public string JobTitleName { set; get; }

        public EmployeeDTO(Guid EmployeeID, DateTime HireDate, decimal MonthlySalary, Guid UserID, string JobTitleName)
        {
            this.EmployeeID = EmployeeID;
            this.HireDate = HireDate;
            this.MonthlySalary = MonthlySalary;
            this.UserID = UserID;
            this.JobTitleName = JobTitleName;
        }

        public EmployeeDTO() { }
    }
}
