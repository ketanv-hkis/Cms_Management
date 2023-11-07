using CMSManagement_API.Contant;
using CMSManagement_API.Models;
using Dapper;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System;

namespace CMSManagement_API.Repository
{
    public class TeamRepository:ITeamRepository
    {
        private IConfiguration configuration;
        TaskManagement taskManagement = new TaskManagement();
        public TeamRepository(IConfiguration _configuration)
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


        public List<Team> GetLanguage()
        {
            using (IDbConnection connection = GetDbConnection())
            {
                List<Team> teams =  SqlMapper.Query<Team>(connection, taskManagement.GetLanguage, commandType: CommandType.StoredProcedure).ToList();
                return teams;
            }
        }

        public void SaveTeamLanguage(Team team)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Name", team.Name, DbType.String, ParameterDirection.Input);
            parameter.Add("@Created_by", team.Created_by, DbType.Int16, ParameterDirection.Input);

            using (IDbConnection connection = GetDbConnection())
            {
                SqlMapper.Query(connection, taskManagement.SaveTeamLanguage, parameter, commandType: CommandType.StoredProcedure);
            }
        }

        public void SaveTeamAssign(TeamAssign teamAssign)

        {
            var parameter = new DynamicParameters();
            parameter.Add("@TeamAssignId", teamAssign.TeamAssignId, DbType.Int64, ParameterDirection.Input);
            parameter.Add("@TeamId", teamAssign.TeamId, DbType.Int64, ParameterDirection.Input);
            parameter.Add("@EmpId", teamAssign.EmpId, DbType.String, ParameterDirection.Input);
            parameter.Add("@Created_by", teamAssign.Created_by, DbType.Int16, ParameterDirection.Input);

            using (IDbConnection connection = GetDbConnection())
            {
                SqlMapper.Query(connection, taskManagement.SaveTeamAssign, parameter, commandType: CommandType.StoredProcedure);
            }

        }

        public List<TeamAssign> GetTeamAssign(int TeamAssignId)
        {

            var parameter = new DynamicParameters();
            parameter.Add("@TeamAssignId", TeamAssignId, DbType.Int16, ParameterDirection.Input);
            using (IDbConnection connection = GetDbConnection())
            {
                List<TeamAssign> TeamAssigns = SqlMapper.Query<TeamAssign>(connection, taskManagement.GetTeamAssign, parameter, commandType: CommandType.StoredProcedure).ToList();
                return TeamAssigns;
            }
        }

        public void UpdateTeamAssign(TeamAssign teamAssign)
        {
            var parameter = new DynamicParameters();

            parameter.Add("@TeamAssignId", teamAssign.TeamAssignId, DbType.Int64, ParameterDirection.Input);
            parameter.Add("@TeamId", teamAssign.TeamId, DbType.Int64, ParameterDirection.Input);
            parameter.Add("@EmpId", teamAssign.EmpId, DbType.String, ParameterDirection.Input);
            parameter.Add("@Modified_by", teamAssign.Modified_by, DbType.Int16, ParameterDirection.Input);
            using (IDbConnection connection = GetDbConnection())
            {
                SqlMapper.Query(connection, taskManagement.UpdateTeamAssign, parameter, commandType: CommandType.StoredProcedure);
            }

        }

    }
}
