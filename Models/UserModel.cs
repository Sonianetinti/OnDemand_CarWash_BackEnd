using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnDemandCarWashSystem.Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long PhoneNo { get; set; }
        public string Email { get; set; }
      
        public string Password { get; set; }
        //[Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
    }
}

