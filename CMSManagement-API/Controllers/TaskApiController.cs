using CMSManagement_API.Models;
using CMSManagement_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CMSManagement_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class TaskApiController : Controller
    {
        public readonly ITaskService _taskService;

        public TaskApiController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public IActionResult TaskList()
        {
            try
            {
                var data = _taskService.GetAllTasks();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult TaskAdd(Taskdetails taskdetails)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _taskService.TaskAdd(taskdetails);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetTaskById(int id)
        {
            try
            {
                var list = _taskService.GetTaskById(id);
                if (list.Image != null)
                {
                    string path = Path.Combine($"{list.Image}");
                    var photo = System.IO.File.ReadAllBytes(path);
                    list.Image = Convert.ToBase64String(photo);

                }
                if (list.Video != null)
                {
                    string videoPath = Path.Combine($"{list.Video}");
                    var video = System.IO.File.ReadAllBytes(videoPath);
                    list.Video = Convert.ToBase64String(video);

                }
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTask(Taskdetails taskdetails)
        {
            try
            {
                _taskService.UpdateTask(taskdetails);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                if (id != 0)
                {
                    bool deleted = _taskService.DeleteTask(id);
                    return Ok(deleted);
                }
                else
                {
                    return BadRequest(false);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
