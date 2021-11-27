using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
            userdata result = new userdata();

            using (var httpClient = new HttpClient())
            {
                string token = "";

                var myContent = JsonConvert.SerializeObject(login);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                token = HttpContext.Session.GetString("JWToken");
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                using (var response = await httpClient.PostAsync("https://localhost:5001/api/user/Login", byteContent))
                {

                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        return View("Unauthorized");
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();




                    result = JsonConvert.DeserializeObject<userdata>(apiResponse);
                }

            }

           
            

          
            HttpContext.Session.SetString("JWToken", result.Token);
            return RedirectToAction("index","Product");
        }










    }


}
    

