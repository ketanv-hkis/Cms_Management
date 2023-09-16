using CMSManagement_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace CMSManagement_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        HttpClient client = new HttpClient();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string session = HttpContext.Session.GetString("Id");
            ViewBag.response = (session);
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    client.BaseAddress = new Uri("https://localhost:7053/api/Employee/");
                    var response = client.PostAsJsonAsync<Login>("Login", login).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string responseAsString = response.Content.ReadAsStringAsync().Result;

                        if (responseAsString != "")
                        {

                            JObject json = JObject.Parse(responseAsString);
                            int id = (int)json["id"];
                            int role = (int)json["role"];
                            string token = (string)json["token"];

                            //HttpContext.Session.SetString("Id", responseAsString);
                            HttpContext.Session.SetString("Id", id.ToString());
                            HttpContext.Session.SetString("Role", role.ToString());
                            HttpContext.Session.SetString("Token", token);


                            return Ok(responseAsString);
                        }
                        else
                        {
                            return Ok(null);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return Ok(null);
        }


        [HttpGet]
        public IActionResult Authentication()
        {
            return Json(HttpContext.Session.GetString("Token"));
        }

        [HttpGet]
        [Authentication]
        public async Task<IActionResult> Logout()
        {
            try
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Home");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }
    }
}