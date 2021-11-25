using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DTO.DTO
{
    public class UserModel
    {
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public Guid UserId { get; set; }

        public string Name { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
    }
    public class LoginVM
    {
   
        [Required, DataType(DataType.EmailAddress)]
    
        public string EmailAddress { get; set; }
        [Required, DataType(DataType.Password)]
   
        public string Password { get; set; }
    }

    public class userdata
    {

        public string Token { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
