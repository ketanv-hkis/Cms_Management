using CMSManagement_API.Models;

namespace CMSManagement_API.Repository
{
    public interface ITaskRepository
    {
        void TaskAdd(Taskdetails taskdetails);
        Taskdetails GetTaskById(int Id);
        IEnumerable<Taskdetails> GetAllTasks();

        void UpdateTask(Taskdetails taskdetails);
        bool DeleteTask(int id);
    }
}
