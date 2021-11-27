using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Data.DTO;
using Test.DTO.DTO;

namespace Test.Service.Interface
{
 public   interface IUserService
    {
        public Task<UserDTO> Register(UserDTO viemodel);
        public Task<userdata> Login(UserDTO viemodel);
       public Task<bool> GetuserByemail(string email);
        public string GenerateJSONWebToken(UserModel userInfo);
        public Task<UserModel> AuthenticateUser(LoginVM login);
    }
}
