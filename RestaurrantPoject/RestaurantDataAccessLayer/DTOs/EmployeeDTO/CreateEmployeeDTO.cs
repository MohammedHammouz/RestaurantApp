using System;

namespace RestaurantDataAccessLayer.DTOs.EmployeeDTO
{
    public class CreateEmployeeDTO
    {
        public Guid EmployeeID { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string LastName { set; get; }
        public bool Gender { set; get; }
        public string ImagePath { set; get; }
        public DateTime DateOfBirth { set; get; }
        public Guid? AddressID { set; get; }
        public DateTime HireDate { set; get; }
        public decimal MonthlySalary { set; get; }
        public Guid UserID { set; get; }
        public string JobTitleName { set; get; }

        public CreateEmployeeDTO(Guid EmployeeID, string FirstName, string SecondName, string LastName, bool Gender, string ImagePath, DateTime DateOfBirth, Guid? AddressID, DateTime HireDate, decimal MonthlySalary, string JobTitleName)
        {
            this.EmployeeID = EmployeeID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.LastName = LastName;
            this.Gender = Gender;
            this.ImagePath = ImagePath;
            this.DateOfBirth = DateOfBirth;
            this.AddressID = AddressID;
            this.HireDate = HireDate;
            this.MonthlySalary = MonthlySalary;
            this.JobTitleName = JobTitleName;
        }

        public CreateEmployeeDTO(string FirstName, string SecondName, string LastName, bool Gender, string ImagePath, DateTime DateOfBirth, Guid? AddressID, DateTime HireDate, decimal MonthlySalary, string JobTitleName)
        {
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.LastName = LastName;
            this.Gender = Gender;
            this.ImagePath = ImagePath;
            this.DateOfBirth = DateOfBirth;
            this.AddressID = AddressID;
            this.HireDate = HireDate;
            this.MonthlySalary = MonthlySalary;
            this.JobTitleName = JobTitleName;
        }

        public CreateEmployeeDTO() { }
    }
}
