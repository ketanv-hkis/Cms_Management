﻿using CMSManagement_API.Models;
using CMSManagement_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CMSManagement_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class EmployeeController : Controller
    {
        public readonly IEmployeeService _employeeService;

        public EmployeeController (IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(Login login)
        {
            try
            {
                var list = _employeeService.Login(login.Email, login.Password);

                string base64EncodedPassword = list.Password;
                byte[] encodedBytes = Convert.FromBase64String(base64EncodedPassword);
                string decodedPassword = Encoding.UTF8.GetString(encodedBytes);
                list.Password = decodedPassword;

                if (list != null)
                {
                    var token = GenerateJSONWebToken(login);
                    var id = list.Id;
                    var role = list.Role;

                    var response = new
                    {
                        Id = id,
                        Role= role,
                        Token = token
                    };

                    return Ok(response);
                }
                else
                {
                    return Ok(null);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        private string GenerateJSONWebToken(Login login)
        {
            var securityKey = Encoding.ASCII.GetBytes("ThisismySecretKey");
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("key", "key"),
                    new Claim("value", "Value"),
                    new Claim("DateOfJoing", DateTime.Now.ToString())
                }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }


        [HttpGet]
        public async Task<IActionResult> GetEmployeeDetail(int Id)
        {
            try
            {
                var list = _employeeService.GetEmployeeDetail(Id);
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetEmployee()
        {
            try
            {
                var list = _employeeService.GetEmployee();
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
