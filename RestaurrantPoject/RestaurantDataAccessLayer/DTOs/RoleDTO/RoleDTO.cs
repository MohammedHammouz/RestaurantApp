using System;

namespace RestaurantDataAccessLayer.DTOs.RoleDTO
{
    public class RoleDTO
    {
        public Guid RoleID { set; get; }
        public string RoleName { set; get; }

        public RoleDTO(Guid RoleID, string RoleName)
        {
            this.RoleID = RoleID;
            this.RoleName = RoleName;
        }

        public RoleDTO() { }
    }
}
