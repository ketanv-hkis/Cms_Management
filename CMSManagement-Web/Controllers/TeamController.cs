using CMSManagement_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace CMSManagement_Web.Controllers
{
    public class TeamController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Team> Teams = null;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:7053/api/Team/GetLanguage");
                var response = client.GetAsync("").Result;

                if (response.IsSuccessStatusCode)
                {

                    var readTask = response.Content.ReadAsStringAsync().Result;
                    List<Team> deptObj = System.Text.Json.JsonSerializer.Deserialize<List<Team>>(readTask);
                    List<SelectListItem> items = new List<SelectListItem>();
                    for (int i = 0; i < deptObj.Count; i++)
                    {
                        items.Add(new SelectListItem
                        {
                            Text = deptObj[i].name,
                            Value = Convert.ToString(deptObj[i].id)
                        });
                    }
                    ViewData["teams"] = items;
                    //return Ok(deptObj);
                }
                else
                {
                    Teams = Enumerable.Empty<Team>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    return BadRequest();
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult create(string name)
        {
            var Id = Convert.ToInt32(HttpContext.Session.GetString("Id"));

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7053/api/Team/SaveTeamLanguage?name=" + name + "&Id=" + Id);
                var response = client.Send(request);

                if (response.IsSuccessStatusCode)
                {
                    string responseAsString = response.Content.ReadAsStringAsync().Result;


                    return Json(responseAsString);

                }
            }
            return BadRequest();
        }


        [HttpPost]
        public  async Task<IActionResult> saveTeamAssign(TeamAssign teamAssign)
        {

            var Id = Convert.ToInt32(HttpContext.Session.GetString("Id"));
            teamAssign.TeamAssignId = 0;
            teamAssign.Created_by = Id;
            teamAssign.Is_status = 1;
            teamAssign.Is_delete = 0;
            teamAssign.Created_date = DateTime.Now;
            teamAssign.Modified_date = DateTime.Now;
            teamAssign.Modified_by = Id;
            using (var client = new HttpClient())
            {

                //client.BaseAddress = new Uri("https://localhost:7053/api/Team/SaveTeamAssign");
                ////var response = client.PostAsJsonAsync<TeamAssign>("SaveTeamAssign", teamAssign).Result;

                ////var response = client.PostAsync(ReportApiRoutes.SaveReportGeneralData,
                ////    new StringContent(JsonConvert.SerializeObject(teamAssign), Encoding.UTF8, "application/json"));

                //var json = JsonConvert.SerializeObject(teamAssign);// or JavaScriptSerializer().Serialize
                //var data = new StringContent(json, Encoding.UTF8, "application/json");
                //var response =  client.PostAsJsonAsync("SaveTeamAssign", teamAssign).Result;

                //client.BaseAddress = new Uri("https://localhost:7053/api/Team/SaveTeamAssign");
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //HttpResponseMessage response =  client.PostAsync("https://localhost:7053/api/Team/SaveTeamAssign");

                string apiUrl = "https://localhost:7053/api/Team/SaveTeamAssign";
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string json = JsonConvert.SerializeObject(teamAssign);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(apiUrl,new StringContent(JsonConvert.SerializeObject(teamAssign), Encoding.UTF8, "application/json"));


                if (response.IsSuccessStatusCode)
                {
                    string responseAsString = response.Content.ReadAsStringAsync().Result;
                    return Ok(responseAsString);

                }
            }
            return BadRequest();
        }
    
    }
}
