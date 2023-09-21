using CMSManagement_API.Models;

namespace CMSManagement_API.Services
{
    public interface IEmployeeService
    {
        public Employee Login(string email, string password);

        public List<Employee> GetEmployeeDetail(int Id);

        List<Employee> GetEmployee();


    }
}
