using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace User_Management.Controllers
{
    [Authorize(Roles ="Customer")]
    public class CustomerController : Controller
    {
        public IActionResult CustomerMainPage()
        {
            return View();
        }
    }
}
