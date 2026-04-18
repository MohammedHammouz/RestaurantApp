using System;
using System;

namespace RestaurantDataAccessLayer.DTOs.EmployeeDTO
{
    public class EmployeeListDTO
    {
        public Guid EmployeeID { set; get; }
        public string FullName { set; get; }
        public DateTime HireDate { set; get; }
        public decimal MonthlySalary { set; get; }
        public Guid UserID { set; get; }
        public string JobTitleName { set; get; }

        public EmployeeListDTO(Guid EmployeeID, string FullName, DateTime HireDate, decimal MonthlySalary, Guid UserID, string JobTitleName)
        {
            this.EmployeeID = EmployeeID;
            this.FullName = FullName;
            this.HireDate = HireDate;
            this.MonthlySalary = MonthlySalary;
            this.UserID = UserID;
            this.JobTitleName = JobTitleName;
        }

        public EmployeeListDTO() { }
    }
}
