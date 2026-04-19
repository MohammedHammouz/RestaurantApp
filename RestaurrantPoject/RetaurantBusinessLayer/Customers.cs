using RestaurantDataAccessLayer;
using RestaurantDataAccessLayer.DTOs.CustomerDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaurantBusinessLayer
{
    public class Customers
    {
        public enum enMode
        {
            AddNew = 0,
            Update = 1
        }

        public enMode Mode = enMode.AddNew;



        public Guid CustomerID { get; set; }



        public string FullName { get; set; }



        public DateTime CreatedAt { get; set; }



        public int LoyalityPoints { get; set; }



        public string PhoneNumber { get; set; }



        public Guid CreatedByAdminID { get; set; }


        public Customers()
        {
            this.CustomerID = Guid.Empty;
            this.FullName = "";
            this.CreatedAt = DateTime.Now;
            this.LoyalityPoints = 0;
            this.PhoneNumber = "";
            this.CreatedByAdminID = Guid.Empty;

        }

        private Customers(
            Guid CustomerID, string FullName, DateTime CreatedDate, int LoyalityPoints, string PhoneNumber, Guid CreatedByAdminID)
        {
            this.CustomerID = CustomerID;
            this.FullName = FullName;
            this.CreatedAt = CreatedDate;
            this.LoyalityPoints = LoyalityPoints;
            this.PhoneNumber = PhoneNumber;
            this.CreatedByAdminID = CreatedByAdminID;
            Mode = enMode.Update;
        }
        public async Task<Customers> GetCustomerInfoByCustomerID(Guid CustomerID)
        {
            CustomerDTO customerDTO =await Customer.GetCustomerInfoByCustomerID(CustomerID);
            FullName = customerDTO.FullName;
            CreatedAt = customerDTO.CreatedAt;
            LoyalityPoints = customerDTO.LoyalityPoints;
            PhoneNumber = customerDTO.PhoneNumber;
            CreatedByAdminID = customerDTO.CreatedByAdminID;
            return new Customers(CustomerID, FullName, CreatedAt, LoyalityPoints, PhoneNumber, CreatedByAdminID);
        }
        public static async Task<bool> DeleteCustomer(Guid CustomerID)
        {
            return await Customer.DeleteCustomer(CustomerID);
        }
        public static async Task<IEnumerable<CustomerListDTO>> GetAllCustomers(int PageNumber, int RowsPerPage)
        {
            return await Customer.GetAllCustomers(PageNumber,RowsPerPage);
        }
        public static async Task<bool> IsCustomerExists(Guid CustomerID)
        {
            return await Customer.IsCustomerExists(CustomerID);
        }
        private async Task <bool> _AddNew()
        {
            var customerDTO = new CustomerDTO(Guid.Empty, FullName, CreatedAt, LoyalityPoints, PhoneNumber,CreatedByAdminID);
            CustomerID = await Customer.AddNewCustomer(customerDTO);
            return CustomerID != Guid.Empty;
        }
        private async Task<bool> _Update()
        {
            var customerDTO = new CustomerDTO(CustomerID, FullName, CreatedAt, LoyalityPoints, PhoneNumber, CreatedByAdminID);
            return await Customer.UpdateCustomer(customerDTO);
        }

         public async Task<bool> Save(){
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
