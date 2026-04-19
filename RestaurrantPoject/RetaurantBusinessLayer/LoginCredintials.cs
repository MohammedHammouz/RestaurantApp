using RestaurantDataAccessLayer;
using RestaurantDataAccessLayer.DTOs.Login;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaurantBusinessLayer
{
    public class LoginCredintial
    {
        public enum enMode
        {
            AddNew = 0,
            Update = 1
        }

        public enMode Mode = enMode.AddNew;



        public int? LoginID { get; set; }



        public Guid AdminID { get; set; }



        public string UserName { get; set; }



        public string Email { get; set; }



        public string PasswordHash { get; set; }



        public bool Status { get; set; }



        public DateTime LastLogin { get; set; }



        public DateTime CreatedAt { get; set; }


        public LoginCredintial()
        {
            this.LoginID = -1;
            this.AdminID = Guid.Empty;
            this.UserName = "";
            this.Email = "";
            this.PasswordHash = "";
            this.Status = false;
            this.LastLogin = DateTime.Now;
            this.CreatedAt = DateTime.Now;

        }

        private LoginCredintial(
            int? LoginID, Guid AdminID, string UserName, string Email, string PasswordHash, bool Status, DateTime LastLogin, DateTime CreatedAt)
        {
            this.LoginID = LoginID;
            this.AdminID = AdminID;
            this.UserName = UserName;
            this.Email = Email;
            this.PasswordHash = PasswordHash;
            this.Status = Status;
            this.LastLogin = LastLogin;
            this.CreatedAt = CreatedAt;
            Mode = enMode.Update;
        }

        public static async Task<IEnumerable<LoginDTO>> GetAllLogins(int PageNumber, int RowsPerPage)
        {
            return await LoginCredentails.GetAllLogins(PageNumber,RowsPerPage);
        }



        private async Task<bool> _AddNew()
        {
            LoginID =await LoginCredentails.AddNewLogin(new LoginDTO(Guid.Empty, AdminID, UserName, Email, PasswordHash, LastLogin, CreatedAt, Status));
            return LoginID != -1;
        }

        private async Task<bool> _Update()
        {
            return true;
            //return LoginCredentails.UpdateLogin(
            //    new LoginDTO(LoginID, AdminID, UserName, Email, PasswordHash, Status, LastLogin, CreatedAt));
        }
        public static async Task<bool> IsLoginExists(Guid LoginID)
        {
            return await LoginCredentails.IsLoginExists(LoginID);
        }
        public static async Task<bool> IsLoginExists(string UserName)
        {
            return await LoginCredentails.IsLoginExists(UserName);
        }
        public static async Task<bool> Login(string UserName, string Password)
        {
            if(await IsLoginExists(UserName))
            {
                return await LoginCredentails.Login(UserName, Password);
            }
            return false;
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
    }
}
