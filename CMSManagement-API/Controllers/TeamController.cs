using CMSManagement_API.Models;
using CMSManagement_API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Xml.Linq;

namespace CMSManagement_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TeamController : Controller
    {

        private readonly ITeamService _service;
        public TeamController(ITeamService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetLanguage()
        {
            try
            {
                var list = _service.GetLanguage();
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> SaveTeamLanguage(string name, int Id)
        {
            try
            {
                Team team = new Team();
                team.Name = name;
                team.Created_by = Id;
                _service.SaveTeamLanguage(team);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveTeamAssign(TeamAssign teamAssign)
        {
            try
            {
                _service.SaveTeamAssign(teamAssign);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }



        }
    }
}
