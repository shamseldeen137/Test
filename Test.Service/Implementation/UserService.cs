using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Test.Data.DTO;
using Test.Data.Models;
using Test.DTO.DTO;
using Test.Repo.Interface;
using Test.Service.Interface;

namespace Test.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _IUserRepo;
        private readonly IMapper _Imapper;
        private IConfiguration _config;
        public UserService(IUserRepo IUserRepository,IMapper imapper, IConfiguration config)
        {
            this._IUserRepo = IUserRepository;
            this._Imapper = imapper;
            _config = config;
        }

        public async Task<bool> GetuserByemail(string email)
        {
           return await _IUserRepo.GetuserByemail(email);
        }

        public async Task<userdata> Login(UserDTO viemodel)
        {
           var user = _Imapper.Map<User>(viemodel);
            user.Password = EncodePasswordToBase64(viemodel.Password);
            var result =await  _IUserRepo.Login(user);
            var loggeduser = _Imapper.Map<UserDTO>(result);
          

            string response = "";
            var authuser = await AuthenticateUser(new LoginVM() { EmailAddress= result.Email,Password= result.Password});

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(authuser);
                response = tokenString;
            }
            else
            {
                return null;
            }

            return new userdata { Token = response, Email = result.Email, Name = result.UserName, UserId = user.UserId };
        }

        public async Task<UserDTO> Register(UserDTO viemodel)
        {
            var user = _Imapper.Map<User>(viemodel);
            user.Password = EncodePasswordToBase64(viemodel.Password);
          var result= await _IUserRepo.Register(user);
            var loggeduser = _Imapper.Map<UserDTO>(result);
            return loggeduser;
        }
        string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }


        public string GenerateJSONWebToken(UserModel userInfo)
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

        public async Task<UserModel> AuthenticateUser(LoginVM login)
        {
            UserModel user = null;


            var logeuser = await _IUserRepo.Login(new User { Email = login.EmailAddress, Password = login.Password });
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
                 else user.Role = "Customer";


            }
            else user.Role = "Customer";
            return user;
        }
    }
}
