using Azure;
using BusinessLogic;
using DataModel;
using DataModel.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace B2B.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [LogActionFilter]
    public class LoginController : Controller
    {
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            Cookie.Set(this.Response, "authtoken", "", new int?(1440));
            Cookie.Set(this.Response, "firstname", "", new int?(1440));
            Cookie.Set(this.Response, "lastname", "", new int?(1440));
            Cookie.Set(this.Response, "email", "", new int?(1440));

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post(UserModel login)
        {
            IActionResult response = Unauthorized();
            var loginresult = UserBO.Instance.AuthenticateUser(login.Username, login.Password).Result;

            if(loginresult != null)
            {
                var tokenString = GenerateJSONWebToken(loginresult);

                if (loginresult.LastUpdateDate != null)
                {
                    Cookie.Set(this.Response, "username", loginresult.AccountName, new int?(1440));
                    Cookie.Set(this.Response, "firstname", loginresult.FirstName == null ? "" : loginresult.FirstName, new int?(1440));
                    Cookie.Set(this.Response, "lastname", loginresult.LastName == null ? "" : loginresult.LastName, new int?(1440));
                    Cookie.Set(this.Response, "email", loginresult.Email == null ? "" : loginresult.Email, new int?(1440));
                    Cookie.Set(this.Response, "authtoken", tokenString, new int?(1440));

                    response = Ok(new { redirect = true, page = "/" });
                    return response;
                }
                else
                {
                    Cookie.Set(this.Response, "username", loginresult.AccountName, new int?(1440));
                    Cookie.Set(this.Response, "authtoken", tokenString, new int?(1440));
                    //Cookie.Set(this.Response, "newpasswordrequired", tokenString, new int?(1440));

                    response = Ok(new { redirect = true, page = "/Login/ChangePassword" });
                    return response;
                }
            }

            return response;
        }

        private string GenerateJSONWebToken(Web_UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.AccountName),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email == null ? "" : userInfo.Email),
                //new Claim("DateOfJoing", userInfo.DateOfJoing.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //private string GenerateJSONWebToken(UserModel userInfo)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(_config["Jwt:Issuer"],
        //      _config["Jwt:Issuer"],
        //      null,
        //      expires: DateTime.Now.AddHours(8),
        //      signingCredentials: credentials);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        [AllowAnonymous]
        [HttpGet]
        [Route("ChangePassword")]
        public IActionResult ChangePassword()
        {
            ViewData["username"] = Cookie.Get(this.Request, "username");

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("UpdatePassword")]
        public IActionResult UpdatePassword(UserModel user)
        {
            ViewData["username"] = Cookie.Get(this.Request, "username");
            var updatepasswordresult = UserBO.Instance.ChangePassword(user).Result;

            IActionResult response = Ok(new { Result = updatepasswordresult, redirect = true, page = "/" });
            return response;
        }
    }
}
