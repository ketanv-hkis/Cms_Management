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
        private readonly Initial _initial;

        public EmployeeController(Initial initial)
        {
            _initial = initial;
        }

        public IActionResult EmployeeList()
        {
            try
            {
                HttpClient _client = _initial.HttpClients();
                HttpResponseMessage response = _client.GetAsync("Employee/GetAllEmployee").Result;

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
        public IActionResult EmployeeAdd(Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    HttpClient _client = _initial.HttpClients();
                    HttpResponseMessage response = _client.PostAsJsonAsync<Employee>("Employee/SaveEmployee", employee).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string responseAsString = response.Content.ReadAsStringAsync().Result;
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


        public IActionResult EmployeeUpdate(int id)
        {
            try
            {
                HttpClient _client = _initial.HttpClients();
                HttpResponseMessage response = _client.GetAsync("Employee/GetUserById?id=" + id).Result;

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
        public IActionResult EmployeeUpdate(Employee employees)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string Modifiedby = HttpContext.Session.GetString("Id");
                    employees.Modified_by = Convert.ToInt32(Modifiedby);
                    HttpClient _client = _initial.HttpClients();
                    HttpResponseMessage response = _client.PostAsJsonAsync<Employee>("Employee/UpdateEmployee", employees).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string responseAsString = response.Content.ReadAsStringAsync().Result;
                        return View();
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
        public IActionResult EmployeeDelete(int? Id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpClient _client = _initial.HttpClients();
                    HttpResponseMessage response = _client.DeleteAsync("Employee/DeleteEmployee?id=" + Id).Result;

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
    }
}