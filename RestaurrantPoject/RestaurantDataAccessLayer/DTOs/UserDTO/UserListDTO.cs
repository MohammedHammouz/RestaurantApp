using System;

namespace RestaurantDataAccessLayer.DTOs.UserDTO
{
    public class UserListDTO
    {
        public Guid UserID { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string LastName { set; get; }
        public string Gender { set; get; }
        public DateTime DateOfBirth { set; get; }


        public UserListDTO(Guid UserID, string FirstName, string SecondName, string LastName, string Gender, DateTime DateOfBirth)
        {
            this.UserID = UserID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.LastName = LastName;
            this.Gender = Gender;
            this.DateOfBirth = DateOfBirth;
        }

        public UserListDTO()
        { }
    }
}
