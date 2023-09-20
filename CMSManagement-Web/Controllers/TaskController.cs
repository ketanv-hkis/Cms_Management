using CMSManagement_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CMSManagement_Web.Controllers
{
    [Authentication]
    public class TaskController : Controller
    {
        private readonly Initial _initial;

        public TaskController(Initial initial)
        {
            _initial = initial;
        }

        public IActionResult TaskList()
        {
            try
            {
                HttpClient _client = _initial.HttpClients();
                HttpResponseMessage response = _client.GetAsync("TaskApi/TaskList").Result;

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


        public ActionResult SaveTask()
        {
            HttpClient _client = _initial.HttpClients();
            HttpResponseMessage response = _client.GetAsync("TaskApi/TaskStatusList").Result;

            if (response.IsSuccessStatusCode)
            {
                string responseAsString = response.Content.ReadAsStringAsync().Result;
                List<Models.TaskStatus> data = JsonConvert.DeserializeObject<List<Models.TaskStatus>>(responseAsString);

                ViewBag.TaskStatuses = new SelectList(data, "T_Id", "Task_Status");
            }
            return View();
        }

        [HttpPost]
        public IActionResult SaveTask(Taskdetails taskdetails)
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

                HttpClient _client = _initial.HttpClients();
                HttpResponseMessage response = _client.PostAsJsonAsync<Taskdetails>("TaskApi/TaskAdd", taskdetails).Result;

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


        public ActionResult UpdateTask(int id)
        {
            try
            {

                HttpClient _client = _initial.HttpClients();
                HttpResponseMessage res = _client.GetAsync("TaskApi/TaskStatusList").Result;

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

                    //if (taskdetails.Image != null)
                    //{
                    //    taskdetails.Image = "data:image/jpeg;base64," + taskdetails.Image;
                    //}
                    //if (taskdetails.Video != null)
                    //{
                    //    taskdetails.Video = "data:video/mp4;base64," + taskdetails.Video;
                    //}
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
        public ActionResult UpdateTask(Taskdetails taskdetails)
        {
            try
            {
                string Modifiedby = HttpContext.Session.GetString("Id");
                taskdetails.Modified_by = Convert.ToInt32(Modifiedby);

                //using (var ms = new MemoryStream())
                //{
                //    taskdetails.ImageData.CopyTo(ms);
                //    var fileByte = ms.ToArray();
                //    string ImageByte = Convert.ToBase64String(fileByte);
                //    taskdetails.Image = ImageByte;
                //}

                //using (var ms = new MemoryStream())
                //{
                //    taskdetails.Videodata.CopyTo(ms);
                //    var ByteArray = ms.ToArray();
                //    string videoByte = Convert.ToBase64String(ByteArray);
                //    taskdetails.Video = videoByte;
                //}

                HttpClient _client = _initial.HttpClients();
                HttpResponseMessage response = _client.PostAsJsonAsync<Taskdetails>("TaskApi/UpdateTask", taskdetails).Result;

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
        public IActionResult DeleteTask(int? Id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpClient _client = _initial.HttpClients();
                    HttpResponseMessage response = _client.DeleteAsync("TaskApi/DeleteTask?id=" + Id).Result;

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


        public ActionResult DetailsTask(int id)
        {
            try
            {
                HttpClient _client = _initial.HttpClients();
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

    }
}
