using CMSManagement_API.Models;
using Dapper;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Xml.Linq;

namespace CMSManagement_API.Repository
{
    public class TaskRepository:ITaskRepository
    {
        private IConfiguration configuration;

        public TaskRepository(IConfiguration _configuration)
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

        public void TaskAdd(Taskdetails taskdetails)
        {
            var parameter = new DynamicParameters();
            if (!string.IsNullOrEmpty(taskdetails.Image) && !string.IsNullOrEmpty(taskdetails.Video))
            {
                List<string> imagePaths = new List<string>();
                string[] imageList = taskdetails.Image.Split(", ");
                foreach (var file in imageList)
                {
                    byte[] image64 = Convert.FromBase64String(file);
                    string uniqueName = Guid.NewGuid().ToString("N");
                    string imagePath = Path.Combine("Images", uniqueName + ".jpeg");
                    File.WriteAllBytes(imagePath, image64);
                    imagePaths.Add(imagePath);
                    
                }
                string joinImagepath = string.Join(", ", imagePaths);
                taskdetails.Image = joinImagepath;

                byte[] video = Convert.FromBase64String(taskdetails.Video);
                string videoName = Guid.NewGuid().ToString("N");
                string videoPath = Path.Combine("Images", videoName + ".mp4");
                File.WriteAllBytes(videoPath, video);
                taskdetails.Video = videoPath;

            }
            parameter.Add("@Task_Id", 0, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Name", taskdetails.Name, DbType.String, ParameterDirection.Input);
            parameter.Add("@Description", taskdetails.Description, DbType.String, ParameterDirection.Input);
            parameter.Add("@Image", taskdetails.Image, DbType.String, ParameterDirection.Input);
            parameter.Add("@Video", taskdetails.Video, DbType.String, ParameterDirection.Input);
            parameter.Add("@Estimatedtime", Convert.ToDateTime(taskdetails.Estimatedtime).ToString("MM/dd/yyyy"), DbType.String, ParameterDirection.Input);
            parameter.Add("@Comment", taskdetails.Comment, DbType.String, ParameterDirection.Input);
            parameter.Add("@TaskStatus", taskdetails.TaskStatus, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Created_by", taskdetails.Created_by, DbType.Int32, ParameterDirection.Input);

            using (IDbConnection connection = GetDbConnection())
            {
                SqlMapper.Query(connection, "SP_AddUpdateTask", parameter, commandType: CommandType.StoredProcedure);
            }
        }

        public Taskdetails GetTaskById(int Id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Task_Id", Id, DbType.Int32, ParameterDirection.Input);
            using (IDbConnection connection = GetDbConnection())
            {
                return SqlMapper.Query<Taskdetails>(connection, "sp_GetTaskbyId", parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public IEnumerable<Taskdetails> GetAllTasks()
        {
            using (IDbConnection connection = GetDbConnection())
            {
                var result = connection.Query<Taskdetails>("sp_GetAllTask", commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public void UpdateTask(Taskdetails taskdetails)
        {
            if (!string.IsNullOrEmpty(taskdetails.Image) && !string.IsNullOrEmpty(taskdetails.Video))
            {
                byte[] image64 = Convert.FromBase64String(taskdetails.Image);
                string uniqueName = Guid.NewGuid().ToString("N");
                string imagePath = Path.Combine("Images", uniqueName + ".jpeg");
                File.WriteAllBytes(imagePath, image64);
                taskdetails.Image = imagePath;


                byte[] video = Convert.FromBase64String(taskdetails.Video);
                string videoName = Guid.NewGuid().ToString("N");
                string videoPath = Path.Combine("Images", videoName + ".mp4");
                File.WriteAllBytes(videoPath, video);
                taskdetails.Video = videoPath;

            }

            var parameter = new DynamicParameters();
            parameter.Add("@Task_Id", taskdetails.Task_Id, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Name", taskdetails.Name, DbType.String, ParameterDirection.Input);
            parameter.Add("@Description", taskdetails.Description, DbType.String, ParameterDirection.Input);
            parameter.Add("@Image", taskdetails.Image, DbType.String, ParameterDirection.Input);
            parameter.Add("@Video", taskdetails.Video, DbType.String, ParameterDirection.Input);
            parameter.Add("@Estimatedtime", taskdetails.Estimatedtime, DbType.DateTime, ParameterDirection.Input);
            parameter.Add("@Comment", taskdetails.Comment, DbType.String, ParameterDirection.Input);
            parameter.Add("@TaskStatus", taskdetails.TaskStatus, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Created_by", taskdetails.Modified_by, DbType.Int32, ParameterDirection.Input);
            using (IDbConnection connection = GetDbConnection())
            {
                SqlMapper.Query(connection, "SP_AddUpdateTask", parameter, commandType: CommandType.StoredProcedure);
            }
        }

        public bool DeleteTask(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Task_Id", id, DbType.Int32, ParameterDirection.Input);
            using (IDbConnection connection = GetDbConnection())
            {
                var deleted = SqlMapper.Query(connection, "SP_DeleteTask", parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (deleted == 0)
                    return false;
                else
                    return true;
            }
        }



        public IEnumerable<Models.TaskStatus> GetAllTaskStatus()
        {
            using (IDbConnection connection = GetDbConnection())
            {
                var result = connection.Query<Models.TaskStatus>("sp_GetAllTaskStatus", commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
