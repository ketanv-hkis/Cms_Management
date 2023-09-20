using CMSManagement_API.Models;
using CMSManagement_API.Repository;

namespace CMSManagement_API.Services
{
    public class TaskService:ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public void TaskAdd(Taskdetails taskdetails)
        {
            _taskRepository.TaskAdd(taskdetails);
        }

        public IEnumerable<Taskdetails> GetAllTasks()
        {
            return _taskRepository.GetAllTasks();
        }

        public Taskdetails GetTaskById(int Id)
        {
            Taskdetails taskdetails = _taskRepository.GetTaskById(Id);
            return taskdetails;
        }

        public void UpdateTask(Taskdetails taskdetails)
        {
            _taskRepository.UpdateTask(taskdetails);
        }

        public bool DeleteTask(int Id)
        {
            bool deleted = _taskRepository.DeleteTask(Id);
            return deleted;
        }


        public IEnumerable<Models.TaskStatus> GetAllTaskStatus()
        {
            return _taskRepository.GetAllTaskStatus();
        }

    }
}
