
using RestaurantDataAccessLayer;
using RestaurantDataAccessLayer.DTOs.RoleUserPermissionDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class RoleUserPermissions
    {
        public enum enMode
        {
            AddNew = 0,
            Update = 1
        }

        public enMode Mode = enMode.AddNew;

        Roles roles = new Roles();

        public Guid RoleUserPermissionID { get; set; }



        public Guid RoleID { get; set; }



        public Guid AdminID { get; set; }



        public int Permission { get; set; }


        public RoleUserPermissions()
        {
            this.RoleUserPermissionID = Guid.Empty;
            this.RoleID = Guid.Empty;
            this.AdminID = Guid.Empty;
            this.Permission = -1;

        }
        public async Task LoadRole()
        {
            roles = await Roles.GetRoleInfoByID(RoleID);
        }
        private RoleUserPermissions(
            Guid RoleUserPermissionID, Guid RoleID, Guid AdminID, int Permission)
        {
            this.RoleUserPermissionID = RoleUserPermissionID;
            this.RoleID = RoleID;
            this.AdminID = AdminID;
            this.Permission = Permission;
            Mode = enMode.Update;
        }

        public static async Task<IEnumerable<RoleUserPermissionListDTO>> GetAllRoleUserPermissions(int PageNumber, int RowsPerPage)
        {
            return await RoleUserPermission.GetAllRoleUserPermissions(PageNumber,RowsPerPage);
        }
        private async Task< bool> _AddNew()
        {
            RoleUserPermissionDTO DTO = new RoleUserPermissionDTO(Guid.Empty, RoleID, Permission, AdminID);
            RoleUserPermissionID =await RoleUserPermission.AddNewRoleUserPermission(DTO);
            return RoleUserPermissionID != Guid.Empty;
        }
        private async Task<bool> _Update()
        {
            RoleUserPermissionDTO DTO = new RoleUserPermissionDTO(RoleUserPermissionID, RoleID, Permission, AdminID);
            return await RoleUserPermission.UpdateRoleUserPermission(DTO);
        }
        public async Task<RoleUserPermissions> GetRoleUserPermissionInfoByRoleUserPermissionID(Guid RoleUserPermissionID)
        {
            
            RoleUserPermissionDTO DTO =await RoleUserPermission.GetRoleUserPermissionInfo(RoleUserPermissionID);
            RoleID = DTO.RoleID;
            Permission = DTO.Permission;
            AdminID = DTO.AdminID;
            return new RoleUserPermissions(RoleUserPermissionID, RoleID, AdminID, Permission);
        }
        public async Task<RoleUserPermissions> GetRoleUserPermissionInfoByAdminID(Guid AdminID)
        {

            RoleUserPermissionDTO DTO = await RoleUserPermission.GetRoleUserPermissionInfoByAdminID(AdminID);
            RoleID = DTO.RoleID;
            Permission = DTO.Permission;
            RoleUserPermissionID = DTO.RoleUserPermissionID;
            return new RoleUserPermissions(RoleUserPermissionID, RoleID, AdminID, Permission);
        }
        public async Task<bool> DeletePermission()
        {
           return await RoleUserPermission.DeleteRoleUserPermission(RoleUserPermissionID);
        }
        public async Task<bool> IsRoleUserPermissionExistsByRoleUserPermissionID(Guid RoleUserPermissionID)
        {
            return await RoleUserPermission.IsRoleUserPermissionExistsByRoleUserPermissionID(RoleUserPermissionID);
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
