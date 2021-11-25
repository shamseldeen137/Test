using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Data.Models;

namespace Test.Repo.Interface
{
 public   interface IUserRepo
    {
        public Task<User> Register(User viemodel);
        public Task<User> Login(User viemodel);
        public Task<bool> GetuserByemail(string email);
    }
}
