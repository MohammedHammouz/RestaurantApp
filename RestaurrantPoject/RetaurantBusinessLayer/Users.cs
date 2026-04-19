using RestaurantDataAccessLayer;
using RestaurantDataAccessLayer.DTOs.UserDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaurantBusinessLayer
{
    public class Users
    {
        public enum enMode
        {
            AddNew = 0,
            Update = 1
        }

        public enMode Mode = enMode.AddNew;
        public Guid UserID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public bool Gender { get; set; }
        public string ImagePath { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid? AddressID { get; set; }
        public Users()
        {
            this.UserID = Guid.Empty;
            this.FirstName = "";
            this.SecondName = "";
            this.LastName = "";
            this.Gender = false;
            this.ImagePath = "";
            this.DateOfBirth = DateTime.Now;
            this.AddressID = Guid.Empty;

        }
        protected Users(
           Guid UserID, string FirstName, string SecondName, string LastName, bool Gender, string ImagePath, DateTime DateOfBirth, Guid? AddressID)
        {
            this.UserID = UserID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.LastName = LastName;
            this.Gender = Gender;
            this.ImagePath = ImagePath;
            this.DateOfBirth = DateOfBirth;
            this.AddressID = AddressID;
            Mode = enMode.Update;
        }
        public static async Task<IEnumerable<UserListDTO>> GetAllTB_Users(int PageNumber, int RowsPerPage)
        {
            return await User.GetAllUsers(PageNumber,RowsPerPage);
        }
        public static async Task<Users> GetUserInfoByID(Guid UserID)
        {
            var dto =await User.GetUserInfoByID(UserID);
            if (dto == null)
            {
                return null;
            }
            return new Users(dto.UserID, dto.FirstName, dto.SecondName, dto.LastName, dto.Gender, dto.ImagePath, dto.DateOfBirth, dto.AddressID);
        }
        private async Task<bool> _AddNew()
        {
            
            UserID =
                await User.AddNewUser(
                    new UserDTO());

            return UserID != Guid.Empty;
        }
        private async Task<bool> _Update()
        {
            return  await User.UpdateUser(
                new UserDTO(UserID, FirstName, SecondName, LastName, Gender, ImagePath, DateOfBirth, AddressID));
        }
        public async Task<bool> Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (await _AddNew())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return await _Update();
            }

            return false;
        }
        public async Task<bool>DeleteUser()
        {
            return await User.DeleteUser(UserID);
        }
        public async Task<bool> IsUserExists(Guid UserID)
        {
            return await User.IsUserExists(UserID);
        }
    }
    
}