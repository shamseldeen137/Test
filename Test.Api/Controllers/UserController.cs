using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Data.DTO;
using Test.DTO.DTO;
using Test.Service.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Test.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private IConfiguration _config;
        public UserController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;

        }
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(UserDTO vm)
        {


            var exist = await _userService.GetuserByemail(vm.Email);
            if (exist)
            {

              //  ViewBag.Error = "User Already Exists";

                return NoContent();
            }



            try
            {

                var result = await _userService.Register(vm);
                if (result != null)
                {


                   // ViewBag.Error = "User Registeration Completed Successfually";

                    return RedirectToAction(nameof(Index), "Product");
                }
                else
                {

               //    ViewBag.Error = "Error At Register";

                    return NoContent();

                }

            }
            catch (Exception ex)
            {


                return NoContent();
            }


        }


        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginVM login)
        {




           
            var user = await _userService.Login(new UserDTO() { Email=login.EmailAddress,Password=login.Password});

            
            //HttpContext.Session.SetString("Token", response);
            return Ok(user);
        }


        // GET: api/<UserController>
      
    }
}
