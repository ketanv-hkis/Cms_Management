using CMSManagement_API.Models;

namespace CMSManagement_API.Repository
{
    public interface IEmployeeRepository
    {
        public Employee Login(string email, string password);
    }
}
