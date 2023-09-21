using CMSManagement_Web.Models;
using Microsoft.AspNetCore.Http;
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

        public async Task<IActionResult> sessionId()
        {
            try {
                HttpContext.Session.SetInt32("IsAdmin",0);
                var isAdmin = HttpContext.Session.GetInt32("IsAdmin");

                return Ok(isAdmin);
            }
            catch(Exception e) { return BadRequest(e); }
            
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

        public IActionResult Admin()
        {
            return View("~/Views/Admin/Index.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}