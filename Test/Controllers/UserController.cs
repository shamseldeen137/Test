using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Test.Data.DTO;
using Test.DTO.DTO;
using Test.Service.Interface;

namespace Test.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private IConfiguration _config;
        public UserController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;

        }
        [HttpPost]
        public async Task<IActionResult> RegisterAsync( UserDTO vm)
        {

           
            var exist = await _userService.GetuserByemail(vm.Email);
            if (exist)
            {

                ViewBag.Error = "User Already Exists";

                return View();
            }



            try
            {

                var result = await _userService.Register(vm);
                if (result != null)
                {

                   
                        ViewBag.Error = "User Registeration Completed Successfually" ;
                   
                    return RedirectToAction(nameof(Index),"Product");
                }
                else
                {

                    ViewBag.Error = "Error At Register";

                    return View();

                }

            }
            catch (Exception ex)
            {


                return View();
            }


        }












     

        
      [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpGet]
               public async Task<IActionResult> Login()
        {
            return View();
        }
            [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync( LoginVM login)
        {




            string response = "";
            var user = await AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = tokenString;
            }
            else
            {
                return Ok(false);
            }
            HttpContext.Session.SetString("Token", response);
            return Ok(new userdata { Token = response, Email = user.EmailAddress, Name = user.Name, UserId = user.UserId });
        }










        private string GenerateJSONWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);




            var claims = new[] {
        new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
        new Claim(JwtRegisteredClaimNames.Email, userInfo.EmailAddress),
       new Claim("roles", userInfo.Role),

        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };



            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                  _config["Jwt:Issuer"],
                  claims,
                  expires: DateTime.Now.AddMinutes(120),
                  signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<UserModel> AuthenticateUser(LoginVM login)
        {
            UserModel user = null;


            var logeuser = await _userService.Login(new UserDTO   { Email = login.EmailAddress, Password = login.Password });
            if (logeuser == null)
            {
                return null;

            }
            //Validate the User Credentials    
            //Demo Purpose, I have Passed HardCoded User Information    

            user = new UserModel
            {
                Username = logeuser.UserName,
                EmailAddress = logeuser.Email
                ,
                Name = logeuser.UserName,
                UserId = logeuser.UserId
            };
            if (logeuser.Type != null)
            {


                if (logeuser.Type == "0") user.Role = "Admin";
                if (logeuser.Type == "1") user.Role = "Manager";
                if (logeuser.Type == "2") user.Role = "Customer";


            }
            else user.Role = "Customer";
            return user;
        }
    }


}
    

