using CMSManagement_Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Web;

namespace CMSManagement_Web.Controllers
{
    [Authentication]
    public class EmployeeController : Controller
    {
        private readonly Global _global;
        HttpClient _client = new HttpClient();

        public EmployeeController(Global global)
        {
            _global = global;
            _client = _global.HttpClients();
        }

        public async Task<IActionResult> EmployeeList()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("Employee/GetAllEmployee");

                if (response.IsSuccessStatusCode)
                {
                    string responseAsString = response.Content.ReadAsStringAsync().Result;
                    List<Employee> data = JsonConvert.DeserializeObject<List<Employee>>(responseAsString);
                    return View(data);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return View();
        }


        public IActionResult EmployeeAdd()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EmployeeAdd(Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    HttpResponseMessage response = await _client.PostAsJsonAsync<Employee>("Employee/SaveEmployee", employee);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseAsString = response.Content.ReadAsStringAsync().Result;
                        ViewBag.Message = "Employee Add SuccessFully !";
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return Ok(null);
        }


        public async Task<IActionResult> EmployeeUpdate(int id)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("Employee/GetUserById?id=" + id);

                if (response.IsSuccessStatusCode)
                {
                    string responseAsString = response.Content.ReadAsStringAsync().Result;
                    Employee data = JsonConvert.DeserializeObject<Employee>(responseAsString);
                    return View(data);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EmployeeUpdate(Employee employees)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string Modifiedby = HttpContext.Session.GetString("Id");
                    employees.Modified_by = Convert.ToInt32(Modifiedby);
                    HttpResponseMessage response = await _client.PostAsJsonAsync<Employee>("Employee/UpdateEmployee", employees);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseAsString = response.Content.ReadAsStringAsync().Result;
                        return RedirectToAction("EmployeeList");
                    }
                }
                return Ok(null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EmployeeDelete(int? Id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpResponseMessage response = await _client.DeleteAsync("Employee/DeleteEmployee?id=" + Id);

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

        public async Task<IActionResult> Index()
        {

            IEnumerable<Employee> employees = null;
            using (var client = new HttpClient())
            {

                HttpResponseMessage response = await _client.GetAsync("Employee/GetAllEmployee");

                if (response.IsSuccessStatusCode)
                {

                    var readTask = response.Content.ReadAsStringAsync().Result;
                    List<Employee> deptObj = JsonConvert.DeserializeObject<List<Employee>>(readTask);


                    return Ok(deptObj);
                    //return Ok(deptObj);
                }
                else
                {
                    employees = Enumerable.Empty<Employee>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    return BadRequest();
                }
            }
        }

    }
}