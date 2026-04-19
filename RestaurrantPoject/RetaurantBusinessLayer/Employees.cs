using RestaurantDataAccessLayer;
using RestaurantDataAccessLayer.DTOs.EmployeeDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaurantBusinessLayer
{
    public class Employees
    {
        public enum enMode
        {
            AddNew = 0,
            Update = 1
        }

        public enMode Mode = enMode.AddNew;



        public Guid EmployeeID { get; set; }



        public Guid UserID { get; set; }



        public string JobTitleName { get; set; }



        public DateTime HireDate { get; set; }



        public decimal MonthlySalary { get; set; }

        public Users user{ get; set; }
        public Employees()
        {
            this.EmployeeID = Guid.Empty;
            this.UserID = Guid.Empty;
            this.JobTitleName = "";
            this.HireDate = DateTime.Now;
            this.MonthlySalary = -1;

        }
        private Employees(
            Guid EmployeeID, Guid UserID, string JobTitleName, DateTime HireDate, decimal MonthlySalary)
        {
            this.EmployeeID = EmployeeID;
            this.UserID = UserID;
            this.JobTitleName = JobTitleName;
            this.HireDate = HireDate;
            this.MonthlySalary = MonthlySalary;
            Mode = enMode.Update;
        }
        public static async Task<IEnumerable<EmployeeListDTO>> GetAllEmployees(int PageNumber, int RowsPerPage)
        {
            return await Employee.GetAllEmployees(PageNumber,RowsPerPage);
        }
        public async Task<Users> LoadUseInfo(Guid UserId)
        {
            user = await Users.GetUserInfoByID(UserID);
            return user;
        }
        private async Task<bool> _AddNew()
        {

            EmployeeDTO employeeDTO = new EmployeeDTO(Guid.Empty, HireDate, MonthlySalary, UserID, JobTitleName);
            Users user = await LoadUseInfo(UserID);
            EmployeeID = employeeDTO.EmployeeID;
            HireDate = employeeDTO.HireDate;
            MonthlySalary = employeeDTO.MonthlySalary;
            JobTitleName = employeeDTO.JobTitleName;
            CreateEmployeeDTO createEmployeeDTO = new CreateEmployeeDTO(user.FirstName, user.SecondName, user.LastName, user.Gender, user.ImagePath, user.DateOfBirth, user.AddressID, HireDate, MonthlySalary, JobTitleName);
            EmployeeID =await Employee.AddNewEmployee(createEmployeeDTO);
            return EmployeeID != Guid.Empty;
        }
        private async Task<bool> _Update()
        {
            EmployeeDTO employeeDTO = new EmployeeDTO(EmployeeID, HireDate, MonthlySalary, UserID, JobTitleName);
            return await Employee.UpdateEmployee(employeeDTO);
        }
        public static async Task<bool> DeleteEmployee(Guid EmployeeID)
        {
            return await Employee.DeleteEmployee(EmployeeID);
        }
        public static async Task<bool> IsEmployeeExists(Guid EmployeeID)
        {
            return await Employee.IsEmployeeExists(EmployeeID);
        }
        public static async Task<bool> IsEmployeeExistsByUserID(Guid UserID)
        {
            return await Employee.IsEmployeeExistsByUserID(UserID);
        }
        public async Task<Employees> GetEmployeeInfoByUserID(Guid UserID)
        {
            EmployeeDTO employeeDTO = await Employee.GetEmployeeInfoByUserID(UserID);
            return new Employees(employeeDTO.EmployeeID, UserID, employeeDTO.JobTitleName, employeeDTO.HireDate, employeeDTO.MonthlySalary);
        }
        public async Task<Employees> GetEmployeeInfoByEmployeeID(Guid EmployeeID)
        {
            EmployeeDTO employeeDTO = await Employee.GetEmployeeInfoByEmployeeID(EmployeeID);
            return new Employees(EmployeeID, employeeDTO.UserID, employeeDTO.JobTitleName, employeeDTO.HireDate, employeeDTO.MonthlySalary);
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
