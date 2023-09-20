using CMSManagement_API.Models;

namespace CMSManagement_API.Services
{
    public interface ITaskService
    {
        void TaskAdd(Taskdetails taskdetails);
        Taskdetails GetTaskById(int Id);
        IEnumerable<Taskdetails> GetAllTasks();
        void UpdateTask(Taskdetails taskdetails);
        bool DeleteTask(int id);

        IEnumerable<Models.TaskStatus> GetAllTaskStatus();
    }
}
