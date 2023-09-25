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
                    List<string> imagePaths = new List<string>();
                    string[] imageList = list.Image.Split(", ");
                    foreach (var file in imageList)
                    {
                        string path = Path.Combine($"{file}");
                        var photo = System.IO.File.ReadAllBytes(path);
                        var image = Convert.ToBase64String(photo);
                        imagePaths.Add(image);
                    }
                    string joinImagepath = string.Join(", ", imagePaths);
                    list.Image = joinImagepath;
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


        [HttpGet]
        public IActionResult TaskStatusList()
        {
            try
            {
                var data = _taskService.GetAllTaskStatus();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
