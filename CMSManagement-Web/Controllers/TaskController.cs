using CMSManagement_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace CMSManagement_Web.Controllers
{
    [Authentication]
    public class TaskController : Controller
    {
        private readonly Global _global;
        HttpClient _client = new HttpClient();
        public TaskController(Global global)
        {
            _global = global;
            _client = _global.HttpClients();
        }
        
        public async Task<IActionResult> TaskList()
        {
            try
            {
                
                HttpResponseMessage response = await _client.GetAsync("TaskApi/TaskList");

                if (response.IsSuccessStatusCode)
                {
                    string responseAsString = response.Content.ReadAsStringAsync().Result;
                    List<Taskdetails> data = JsonConvert.DeserializeObject<List<Taskdetails>>(responseAsString);
                    return View(data);

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return View();
        }


        public async Task<IActionResult> SaveTask()
        {
            HttpResponseMessage response = await _client.GetAsync("TaskApi/TaskStatusList");

            if (response.IsSuccessStatusCode)
            {
                string responseAsString = response.Content.ReadAsStringAsync().Result;
                List<Models.TaskStatus> data = JsonConvert.DeserializeObject<List<Models.TaskStatus>>(responseAsString);

                ViewBag.TaskStatuses = new SelectList(data, "T_Id", "Task_Status");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveTask(Taskdetails taskdetails)
        {
            try
            {
                List<string> elements = new List<string>();

                foreach (var file in taskdetails.ImageData)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        string imageBase64 = Convert.ToBase64String(fileBytes);
                        elements.Add(imageBase64);
                    }
                }
                string combinedImages = string.Join(", ", elements);
                taskdetails.Image = combinedImages;

                using (var ms = new MemoryStream())
                {
                    taskdetails.Videodata.CopyTo(ms);
                    var ByteArray = ms.ToArray();
                    string videoByte = Convert.ToBase64String(ByteArray);
                    taskdetails.Video = videoByte;
                }

                HttpResponseMessage response = await _client.PostAsJsonAsync<Taskdetails>("TaskApi/TaskAdd", taskdetails);

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Task Create Successfully";
                    return View();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return View();
        }


        public async Task<IActionResult> UpdateTask(int id)
        {
            try
            {

                HttpResponseMessage res =  await _client.GetAsync("TaskApi/TaskStatusList");

                if (res.IsSuccessStatusCode)
                {
                    string responseAsString = res.Content.ReadAsStringAsync().Result;
                    List<Models.TaskStatus> data = JsonConvert.DeserializeObject<List<Models.TaskStatus>>(responseAsString);

                    ViewBag.TaskStatuses = new SelectList(data, "T_Id", "Task_Status");
                }

                HttpResponseMessage response = _client.GetAsync("TaskApi/GetTaskById?id=" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = response.Content.ReadAsStringAsync().Result;
                    Taskdetails taskdetails = JsonConvert.DeserializeObject<Taskdetails>(jsonResponse);
                    return View(taskdetails);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTask(Taskdetails taskdetails)
        {
            try
            {
                string Modifiedby = HttpContext.Session.GetString("Id");
                taskdetails.Modified_by = Convert.ToInt32(Modifiedby);

                HttpResponseMessage response = await _client.PostAsJsonAsync<Taskdetails>("TaskApi/UpdateTask", taskdetails);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("TaskList");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> DeleteTask(int? Id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpResponseMessage response = await _client.DeleteAsync("TaskApi/DeleteTask?id=" + Id);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseAsString = response.Content.ReadAsStringAsync().Result;
                        return Json("success");
                    }
                }
                return Ok(null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        public async Task<IActionResult> DetailsTask(int id)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("TaskApi/GetTaskById?id=" + id);

                if (response.IsSuccessStatusCode)
                {
                    string details = response.Content.ReadAsStringAsync().Result;
                    Taskdetails taskdetails = JsonConvert.DeserializeObject<Taskdetails>(details);
                    return View(taskdetails);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return View();
        }

    }
}
