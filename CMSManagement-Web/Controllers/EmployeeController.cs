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

namespace CMSManagement_Web.Controllers
{
    public class EmployeeController : Controller
    {
        HttpClient client = new HttpClient();


        public IActionResult EmployeeList()
        {
            try
            {
                string token = HttpContext.Session.GetString("Token");

                client.BaseAddress = new Uri("https://localhost:7053/api/Employee/");
                //var response = client.PostAsJsonAsync<Employee>("SaveEmployee", employee).Result;

                var request = new HttpRequestMessage(HttpMethod.Get, "GetAllEmployee");

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result;

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

        [AllowAnonymous]
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
                    string token = HttpContext.Session.GetString("Token");

                    client.BaseAddress = new Uri("https://localhost:7053/api/Employee/");
                    //var response = client.PostAsJsonAsync<Employee>("SaveEmployee", employee).Result;

                    var request = new HttpRequestMessage(HttpMethod.Post, "SaveEmployee");

                    request.Content = new ObjectContent<Employee>(employee, new JsonMediaTypeFormatter());

                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

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
                string token = HttpContext.Session.GetString("Token");

                client.BaseAddress = new Uri("https://localhost:7053/api/Employee/");
                //var response = client.PostAsJsonAsync<Employee>("SaveEmployee", employee).Result;

                var request = new HttpRequestMessage(HttpMethod.Get, "GetUserById?id=" + id);

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result;

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

                    string token = HttpContext.Session.GetString("Token");

                    client.BaseAddress = new Uri("https://localhost:7053/api/Employee/");

                    var request = new HttpRequestMessage(HttpMethod.Post, "UpdateEmployee");

                    request.Content = new ObjectContent<Employee>(employees, new JsonMediaTypeFormatter());

                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage response = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result;

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


        [HttpGet]
        public IActionResult EmployeeDelete(int ID)
        {
            try
            {
                string token = HttpContext.Session.GetString("Token");

                client.BaseAddress = new Uri("https://localhost:7053/api/Employee/");
                //var response = client.PostAsJsonAsync<Employee>("SaveEmployee", employee).Result;

                var request = new HttpRequestMessage(HttpMethod.Get, "GetUserById?id=" + ID);

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result;

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
        public IActionResult EmployeeDelete(int? Id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string token = HttpContext.Session.GetString("Token");

                    client.BaseAddress = new Uri("https://localhost:7053/api/Employee/");

                    var request = new HttpRequestMessage(HttpMethod.Delete, "DeleteEmployee?id=" + Id);

                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage response = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result;

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

    }
}
