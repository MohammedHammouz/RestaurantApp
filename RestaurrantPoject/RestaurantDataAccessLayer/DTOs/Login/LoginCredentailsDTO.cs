using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDataAccessLayer.DTOs.Login
{
    public class LoginDTO
    {
        public Guid LoginID { set; get; }
        public Guid AdminID { set; get; }
        public string UserName { set; get; }
        public string Email { set; get; }
        public string PasswordHash { set; get; }
        public DateTime? LastLogin { set; get; }
        public DateTime CreatedAt { set; get; }
        public bool Status { set; get; }


        public LoginDTO(Guid LoginID, Guid AdminID, string UserName, string Email, string PasswordHash, DateTime? LastLogin, DateTime CreatedAt, bool Status)
        {
            this.LoginID = LoginID;
            this.AdminID = AdminID;
            this.UserName = UserName;
            this.Email = Email;
            this.PasswordHash = PasswordHash;
            this.LastLogin = LastLogin;
            this.CreatedAt = CreatedAt;
            this.Status = Status;
        }

        public LoginDTO()
        { }
    }
}
