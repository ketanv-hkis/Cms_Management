using CMSManagement_API.Models;

namespace CMSManagement_API.Repository
{
    public interface IEmployeeRepository
    {
        Employee Login(string email, string password);
        IEnumerable<Employee> GetAllEmployee();
        void SaveEmployeeDetail(Employee employee);
        void UpdateEmployeeDetail(Employee employee);
        bool DeleteEmployeeDetail(int Id);
        Employee GetUserById(int id);
    }
}
