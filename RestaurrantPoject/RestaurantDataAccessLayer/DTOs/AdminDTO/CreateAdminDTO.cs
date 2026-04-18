using System;

namespace RestaurantDataAccessLayer.DTOs.Admins
{
    public class CreateAdminDTO
    {
        public Guid AdminID { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string LastName { set; get; }
        public bool Gender { set; get; }
        public string ImagePath { set; get; }
        public DateTime DateOfBirth { set; get; }
        public Guid? AddressID { set; get; }
        public decimal MonthlySalary { set; get; }

        public CreateAdminDTO(Guid AdminID, string FirstName, string SecondName, string LastName, bool Gender, String ImagePAth, DateTime DateOfBirth, Guid? AddressID, decimal MonthlySalary)
        {
            this.AdminID = AdminID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.LastName = LastName;
            this.Gender = Gender;
            this.ImagePath = ImagePath;
            this.DateOfBirth = DateOfBirth;
            this.AddressID = AddressID;
            this.MonthlySalary = MonthlySalary;
        }

        public CreateAdminDTO(string FirstName, string SecondName, string LastName, bool Gender, String ImagePAth, DateTime DateOfBirth, Guid? AddressID, decimal MonthlySalary)
        {
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.LastName = LastName;
            this.Gender = Gender;
            this.ImagePath = ImagePath;
            this.DateOfBirth = DateOfBirth;
            this.AddressID = AddressID;
            this.MonthlySalary = MonthlySalary;

        }

        public CreateAdminDTO() { }
    }
}
