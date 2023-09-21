using System.Data;
using System.Text;
using System;
using System.Data.Common;
using CMSManagement_API.Models;
using System.Data.SqlClient;
using Dapper;
using CMSManagement_API.Contant;

namespace CMSManagement_API.Repository
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private IConfiguration configuration;
        TaskManagement taskManagement = new TaskManagement();
        public EmployeeRepository(IConfiguration _configuration)
        {
            configuration = _configuration;

        }

        public DbConnection GetDbConnection()
        {
            string connectionstringAppSetting = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionstringAppSetting))
            {
                return new SqlConnection(connectionstringAppSetting);
            }
            return new SqlConnection(connectionstringAppSetting);
        }


        public Employee Login(string email, string password)
        {
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            string Password = Convert.ToBase64String(encode);

            var parameter = new DynamicParameters();
            parameter.Add("@Email", email, DbType.String, ParameterDirection.Input);
            parameter.Add("@Password", Password, DbType.String, ParameterDirection.Input);
            using (IDbConnection connection = GetDbConnection())
            {
                return (Employee)SqlMapper.Query<Employee>(connection, taskManagement.Login, parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        

        public List<Employee> GetEmployeeDetail(int Id)
        {

            var parameter = new DynamicParameters();
            parameter.Add("@Id", Id, DbType.String, ParameterDirection.Input);
            using (IDbConnection connection = GetDbConnection())
            {
                List<Employee> employees = SqlMapper.Query<Employee>(connection, taskManagement.GetEmployeeDetail, parameter, commandType: CommandType.StoredProcedure).ToList();
                return employees;
            }
        }


        public List<Employee> GetEmployee()
        {
            using (IDbConnection connection = GetDbConnection())
            {
                List<Employee> employees = SqlMapper.Query<Employee>(connection, taskManagement.GetEmployee, commandType: CommandType.StoredProcedure).ToList();
                return employees;
            }
        }

    }
}
