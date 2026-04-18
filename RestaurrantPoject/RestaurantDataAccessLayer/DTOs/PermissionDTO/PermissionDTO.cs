using System;

namespace RestaurantDataAccessLayer.DTOs.PermissionDTO
{
    public class PermissionDTO
    {
        public int PermissionID { set; get; }
        public string PermissionName { set; get; }
        public string Description { set; get; }

        public PermissionDTO(int PermissionID, string PermissionName, string Description)
        {
            this.PermissionID = PermissionID;
            this.PermissionName = PermissionName;
            this.Description = Description;
        }

        public PermissionDTO() { }
    }
}
