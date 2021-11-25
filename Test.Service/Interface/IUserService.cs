using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Data.DTO;


namespace Test.Service.Interface
{
 public   interface IUserService
    {
        public Task<UserDTO> Register(UserDTO viemodel);
        public Task<UserDTO> Login(UserDTO viemodel);
       public Task<bool> GetuserByemail(string email);
    }
}
