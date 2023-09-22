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

        public Employee Login(Login login)
        {
            Employee EmployeeDetails = _employeeRepository.Login(login);
            if (EmployeeDetails != null)
            {
                return EmployeeDetails;
            }
            else
            {
                return null;
            }
        }

        public Employee GetUserById(int id)
        {
            Employee employeeDetail = _employeeRepository.GetUserById(id);
            return employeeDetail;
        }


        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeRepository.GetAllEmployee();
        }


        public void SaveEmployeeDetail(Employee employee)
        {
            _employeeRepository.SaveEmployeeDetail(employee);
        }


        public void UpdateEmployeeDetail(Employee employee)
        {
            _employeeRepository.UpdateEmployeeDetail(employee);
        }

        public bool DeleteEmployeeDetail(int Id)
        {
            bool deleted = _employeeRepository.DeleteEmployeeDetail(Id);
            return deleted;
        }

    }
}
