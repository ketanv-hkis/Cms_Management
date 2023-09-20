using CMSManagement_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Common;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Text.Json;

namespace CMSManagement_Web.Controllers
{
    public class EmployeeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {

            IEnumerable<Employee> employees = null;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:7053/api/Employee/GetEmployee");
                var response = client.GetAsync("").Result;

                if (response.IsSuccessStatusCode)
                {

                    var readTask = response.Content.ReadAsStringAsync().Result;
                    List<Employee> deptObj = JsonSerializer.Deserialize<List<Employee>>(readTask);
                    
                    
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

