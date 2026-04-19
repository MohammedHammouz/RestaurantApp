
using RestaurantDataAccessLayer;
using RestaurantDataAccessLayer.DTOs.RoleDTO;
using RetaurantBusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Roles
    {
        public enum enMode
        {
            AddNew = 0,
            Update = 1
        }

        public enMode Mode = enMode.AddNew;



        public Guid RoleID { get; set; }



        public string RoleName { get; set; }


        public Roles()
        {
            this.RoleID = Guid.Empty;
            this.RoleName = "";

        }

        private Roles(
            Guid RoleID, string RoleName)
        {
            this.RoleID = RoleID;
            this.RoleName = RoleName;
            Mode = enMode.Update;
        }

        public static async Task<IEnumerable<RoleDTO>> GetAllTB_Roles(int PageNumber,int RowsPerPage)
        {
            return await Role.GetAllRoles(PageNumber,RowsPerPage);
        }



        private async Task<bool> _AddNew()
        {
            
            RoleID =
               await Role.AddNewRole(new RoleDTO(Guid.Empty, RoleName));
            return RoleID != Guid.Empty;
        }
        private async Task<bool> _Update()
        {
            return await Role.UpdateRole(new RoleDTO(RoleID, RoleName));
        }


        public static async Task<Roles> GetRoleInfoByID(Guid RoleID)
        {
            var dto = await Role.GetRoleInfo(RoleID);
            if (dto == null)
            {
                return null;
            }
            return new Roles(dto.RoleID,dto.RoleName);
        }
        public async Task<Roles> GetRoleInfoByRoleName(string RoleName)
        {
            var dto = await Role.GetRoleInfo(RoleName);
            if (dto == null)
            {
                return null;
            }
            return new Roles(dto.RoleID, dto.RoleName);
        }
        public static async Task<bool> DeleteRole(Guid RoleID)
        {
            return await Role.DeleteRole(RoleID); 
        }
        public static async Task<bool> IsRoleExistsByRoleID(Guid RoleID)
        {
            return await Role.IsRoleExists(RoleID);
        }
        public static async Task<bool> IsRoleExistsByRoleName(string RoleName)
        {
            return await Role.IsRoleExists(RoleName);
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

