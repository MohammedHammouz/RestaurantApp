using RestaurantDataAccessLayer;
using RestaurantDataAccessLayer.DTOs.Admins;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaurantBusinessLayer
{
    public class Admins
    {
        public enum enMode
        {
            AddNew = 0,
            Update = 1
        }

        public enMode Mode = enMode.AddNew;

        public Guid AdminID { get; set; }

        public Guid UserID { get; set; }

        public decimal MonthlySalary { get; set; }
        public Users users { get; set; }

        public Admins()
        {
            this.AdminID = Guid.Empty;
            this.UserID = Guid.Empty;
            this.MonthlySalary = -1;

        }

        private Admins(
            Guid AdminID, Guid UserID, decimal MonthlySalary)
        {
            this.AdminID = AdminID;
            this.UserID = UserID;
            this.MonthlySalary = MonthlySalary;
            Mode = enMode.Update;
        }
        public static async Task<Users>LoadUserInfo(Guid UserID)
        {
            Users users = await Users.GetUserInfoByID(UserID);
            return users;

        } 
        public static async Task<IEnumerable<AdminListDTO>> GetAllAdmins(int PageNumber, int RowsPerPage)
        {
            return await Admin.GetAllAdmins(PageNumber,RowsPerPage);
        }
        public static async Task<bool> DeleteAdmin(Guid AdminID)
        {
            return await Admin.UnLinkUserAdmin(AdminID)&& await Admin.DeleteAdmin(AdminID);
        }
        private async Task <bool> _AddNew()
        {
            users = await LoadUserInfo(UserID);
            CreateAdminDTO cadminDTO = new CreateAdminDTO(users.FirstName, users.SecondName, users.LastName, users.Gender, users.ImagePath, users.DateOfBirth, users.AddressID, MonthlySalary);
            AdminID = await Admin.AddNewAdmin(cadminDTO);
            AdminDTO adminDTO = new AdminDTO();
            if (AdminID != Guid.Empty)
            {
                return false;
            }
            adminDTO.AdminID = AdminID;
            adminDTO.UserID = UserID;
            adminDTO.MonthlySalary = MonthlySalary;

            UserID =await Admin.LinkUserAdmin(adminDTO);
            return AdminID != Guid.Empty;
        }
        private async Task<bool> _Update()
        {
            AdminDTO adminDTO = new AdminDTO();
            adminDTO.AdminID = AdminID;
            adminDTO.MonthlySalary = MonthlySalary;
            adminDTO.UserID = UserID;
            return await Admin.UpdateAdmin(adminDTO);
        }
        public static async Task<Admins> GetAdminInfoByAdminID(Guid AdminID)
        {
            Guid UserID = Guid.Empty;
            decimal MonthlySalary = -1;
            AdminDTO adminDTO =await Admin.GetAdminInfoByAdminID(AdminID);
            UserID = adminDTO.UserID;
            MonthlySalary = adminDTO.MonthlySalary;
            return new Admins(AdminID, UserID, MonthlySalary);

        }
        public static async Task<bool> IsAdminExistsByAdminID(Guid AdminID)
        {
            return await Admin.IsAdminExistsByAdminID(AdminID);
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
