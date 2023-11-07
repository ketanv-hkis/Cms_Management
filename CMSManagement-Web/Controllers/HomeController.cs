using CMSManagement_Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
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

        public async Task<IActionResult> sessionId()
        {
            try
            {
                HttpContext.Session.SetInt32("IsAdmin", 0);
                var isAdmin = HttpContext.Session.GetInt32("IsAdmin");

                return Ok(isAdmin);
            }
            catch (Exception e) { return BadRequest(e); }

        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    client.BaseAddress = new Uri("https://localhost:7053/api/Employee/");
                    var response = await client.PostAsJsonAsync<Login>("Login", login);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseAsString = response.Content.ReadAsStringAsync().Result;

                        if (responseAsString != null)
                        {

                            JObject json = JObject.Parse(responseAsString);
                            int id = (int)json["id"];
                            int role = (int)json["role"];
                            string token = (string)json["token"];

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
        public async Task<IActionResult> Authentication()
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