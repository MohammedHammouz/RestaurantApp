using System;

namespace RestaurantDataAccessLayer.DTOs.RoleUserPermissionDTO
{
    public class RoleUserPermissionListDTO
    {
        public Guid RoleUserPermissionID { set; get; }
        public string RoleName { set; get; }
        public int Permission { set; get; }
        public string AdminName { set; get; }

        public RoleUserPermissionListDTO(Guid RoleUserPermissionID, string RoleName, int Permission, string AdminName)
        {
            this.RoleUserPermissionID = RoleUserPermissionID;
            this.RoleName = RoleName;
            this.Permission = Permission;
            this.AdminName = AdminName;
        }

        public RoleUserPermissionListDTO() { }        
    }
}
