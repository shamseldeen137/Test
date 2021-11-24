using System;
using System.Collections.Generic;

#nullable disable

namespace Test.Data.Models
{
    public partial class User
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
