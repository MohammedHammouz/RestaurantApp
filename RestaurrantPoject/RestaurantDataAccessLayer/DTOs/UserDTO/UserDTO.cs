using System;

namespace RestaurantDataAccessLayer.DTOs.UserDTO
{
    public class UserDTO
    {
        public Guid UserID { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string LastName { set; get; }
        public bool Gender { set; get; }
        public string ImagePath { set; get; }
        public DateTime DateOfBirth { set; get; }
        public Guid? AddressID { set; get; }


        public UserDTO(Guid UserID, string FirstName, string SecondName, string LastName, bool Gender, string ImagePath, DateTime DateOfBirth, Guid? AddressID)
        {
            this.UserID = UserID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.LastName = LastName;
            this.Gender = Gender;
            this.ImagePath = ImagePath;
            this.DateOfBirth = DateOfBirth;
            this.AddressID = AddressID;
        }

        public UserDTO()
        { }
    }
}
