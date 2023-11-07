using CMSManagement_API.Models;

namespace CMSManagement_API.Services
{
    public interface IEmployeeService
    {
        Employee Login(Login login);
        IEnumerable<Employee> GetAllEmployee();
        void SaveEmployeeDetail(Employee employee);
        void UpdateEmployeeDetail(Employee employee);
        bool DeleteEmployeeDetail(int Id);
        Employee GetUserById(int id);
    }
}
