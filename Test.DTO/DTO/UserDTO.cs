using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Test.Data.DTO
{
    public  class UserDTO
    {
        public Guid UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required,DataType(DataType.Password)]
        
        public string Password { get; set; }
        [Required,Compare("Password", ErrorMessage ="Password does not match"),DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public string Type { get; set; }

    }
}
