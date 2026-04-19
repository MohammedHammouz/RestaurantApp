using RestaurantDataAccessLayer;
using RestaurantDataAccessLayer.DTOs.PermissionDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaurantBusinessLayer
{
    public class Permissions
    {
        public enum enMode
        {
            AddNew = 0,
            Update = 1
        }

        public enMode Mode = enMode.AddNew;



        public int? PermissionID { get; set; }



        public string PermissionName { get; set; }



        public string Description { get; set; }


        public Permissions()
        {
            this.PermissionID = -1;
            this.PermissionName = "";
            this.Description = "";

        }

        private Permissions(
            int PermissionID, string PermissionName, string Description)
        {
            this.PermissionID = PermissionID;
            this.PermissionName = PermissionName;
            this.Description = Description;
            Mode = enMode.Update;
        }

        public static async Task<IEnumerable<PermissionDTO>> GetAllPermissions(int PageNumber, int RowsPerPage)
        {
            return await Permission.GetAllPermissions(PageNumber,RowsPerPage);
        }
        public static async Task<Permissions> GetPermissionInfoByID(int PermissionID)
        {
            PermissionDTO permissionDTO =await Permission.GetPermissionInfoByID(PermissionID);
            return new Permissions(PermissionID, permissionDTO.PermissionName, permissionDTO.Description);
        }
        public static async Task<bool> DeletePermission(int PermissionID)
        {
            return await Permission.DeletePermission(PermissionID);
        }
        public static async Task<bool> IsPermissionExists(int PermissionID)
        {
            return await Permission.IsPermissionExists(PermissionID);
        }
        private async Task<bool> _AddNew()
        {
            PermissionID =await Permission.AddNewPermission(new PermissionDTO(-1, PermissionName, Description));
            return PermissionID != -1;
        }

        private async Task<bool> _Update()
        {
            return await Permission.UpdatePermission(new PermissionDTO(-1, PermissionName, Description));
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
