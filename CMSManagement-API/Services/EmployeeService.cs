using CMSManagement_API.Models;
using CMSManagement_API.Repository;
using System;

namespace CMSManagement_API.Services
{
    public class EmployeeService:IEmployeeService
    {
        public readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository) 
        {
            _employeeRepository = employeeRepository;
        }

        public Employee Login(string email, string password)
        {
            Employee EmployeeDetails = _employeeRepository.Login(email, password);
            if (EmployeeDetails != null)
            {
                return EmployeeDetails;
            }
            else
            {
                return null;
            }
        }
    }
}
