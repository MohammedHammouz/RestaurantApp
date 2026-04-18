using System;

namespace RestaurantDataAccessLayer.DTOs.RoleUserPermissionDTO
{
    public class RoleUserPermissionDTO
    {
        public Guid RoleUserPermissionID { set; get; }
        public Guid RoleID { set; get; }
        public int Permission { set; get; }
        public Guid AdminID { set; get; }

        public RoleUserPermissionDTO(Guid RoleUserPermissionID, Guid RoleID, int Permission, Guid AdminID)
        {
            this.RoleUserPermissionID = RoleUserPermissionID;
            this.RoleID = RoleID;
            this.Permission = Permission;
            this.AdminID = AdminID;
        }

        public RoleUserPermissionDTO() { }        
    }
}
