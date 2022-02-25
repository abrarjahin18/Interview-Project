using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace User_Management.Models.User
{
    public class RegisterViewModel
    {
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string password { get; set; }

        public string Name { get; set; }
        public SelectList Names { get; set; }
    }
}
