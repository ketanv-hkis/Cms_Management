using System.Data;
using System.Text;
using System;
using System.Data.Common;
using CMSManagement_API.Models;
using System.Data.SqlClient;
using Dapper;

namespace CMSManagement_API.Repository
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private IConfiguration configuration;
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


        public Employee Login(Login login)
        {
            byte[] encode = new byte[login.Password.Length];
            encode = Encoding.UTF8.GetBytes(login.Password);
            string Password = Convert.ToBase64String(encode);

            var parameter = new DynamicParameters();
            parameter.Add("@Email", login.Email, DbType.String, ParameterDirection.Input);
            parameter.Add("@Password", Password, DbType.String, ParameterDirection.Input);
            using (IDbConnection connection = GetDbConnection())
            {
                return (Employee)SqlMapper.Query<Employee>(connection, "uspGetEmployee", parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public Employee GetUserById(int Id)
        {

            var parameter = new DynamicParameters();
            parameter.Add("@Id", Id, DbType.Int32, ParameterDirection.Input);
            using (IDbConnection connection = GetDbConnection())
            {
                return SqlMapper.Query<Employee>(connection, "uspGetUserById", parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

        }


        public IEnumerable<Employee> GetAllEmployee()
        {
            using (IDbConnection connection = GetDbConnection())
            {
                var result = connection.Query<Employee>("SP_AllEmployeeDetails", commandType: CommandType.StoredProcedure);
                return result;
            }
        }


        public async void SaveEmployeeDetail(Employee employee)
        {
            var parameter = new DynamicParameters();

            byte[] encode = new byte[employee.Password.Length];
            encode = Encoding.UTF8.GetBytes(employee.Password);
            string Password = Convert.ToBase64String(encode);

            parameter.Add("@Firstname", employee.Firstname, DbType.String, ParameterDirection.Input);
            parameter.Add("@Lastname", employee.Lastname, DbType.String, ParameterDirection.Input);
            parameter.Add("@Email", employee.Email, DbType.String, ParameterDirection.Input);
            parameter.Add("@Password", Password, DbType.String, ParameterDirection.Input);
            parameter.Add("@Gender", employee.Gender, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Mobile_No", employee.Mobile_No, DbType.String, ParameterDirection.Input);
            parameter.Add("@Birthdate", Convert.ToDateTime(employee.Birthdate).ToString("MM/dd/yyyy"), DbType.String, ParameterDirection.Input);
            parameter.Add("@Createdby", employee.Created_by, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Role", employee.Role, DbType.String, ParameterDirection.Input);

            using (IDbConnection connection = GetDbConnection())
            {
                SqlMapper.Query(connection, "SP_SaveEmployeeDetail", parameter, commandType: CommandType.StoredProcedure);
            }
        }


        public void UpdateEmployeeDetail(Employee employee)
        {

            byte[] encode = new byte[employee.Password.Length];
            encode = Encoding.UTF8.GetBytes(employee.Password);
            string Password = Convert.ToBase64String(encode);

            var parameter = new DynamicParameters();
            parameter.Add("@Id", employee.Id, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Firstname", employee.Firstname, DbType.String, ParameterDirection.Input);
            parameter.Add("@Lastname", employee.Lastname, DbType.String, ParameterDirection.Input);
            parameter.Add("@Email", employee.Email, DbType.String, ParameterDirection.Input);
            parameter.Add("@Password", Password, DbType.String, ParameterDirection.Input);
            parameter.Add("@Gender", employee.Gender, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Mobile_No", employee.Mobile_No, DbType.String, ParameterDirection.Input);
            parameter.Add("@Birthdate", Convert.ToDateTime(employee.Birthdate).ToString("MM/dd/yyyy"), DbType.String, ParameterDirection.Input);
            parameter.Add("@Role", employee.Role, DbType.String, ParameterDirection.Input);
            parameter.Add("@Modified_by", employee.Modified_by, DbType.Int32, ParameterDirection.Input);
            using (IDbConnection connection = GetDbConnection())
            {
                SqlMapper.Query(connection, "SP_UpdateEmployeeDetails", parameter, commandType: CommandType.StoredProcedure);
            }

        }


        public bool DeleteEmployeeDetail(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id, DbType.Int32, ParameterDirection.Input);
            using (IDbConnection connection = GetDbConnection())
            {
                var deleted = SqlMapper.Query(connection, "SP_DeleteEmployeeDetails", parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (deleted == 0)
                    return false;
                else
                    return true;
            }
        }


    }
}
