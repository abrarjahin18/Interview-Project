using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MimeKit;
using User_Management.Models.EntityModels.Identity;
using User_Management.Models.User;

namespace User_Management.Controllers
{
    public class UserController : Controller
    {
        SignInManager<ProjectUser> SignInManager;
        UserManager<ProjectUser> UserManager;
        RoleManager<ProjectUserRole> RoleManager;
        public UserController(SignInManager<ProjectUser> SignInManager, UserManager<ProjectUser> UserManager, RoleManager<ProjectUserRole> RoleManager)
        {
            this.SignInManager = SignInManager;
            this.UserManager = UserManager;
            this.RoleManager = RoleManager;
        }
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(AddRoleModel model)
        {
            var userRole = new ProjectUserRole()
            {
                Name = model.Role

            };
            var resultUserRole = await RoleManager.CreateAsync(userRole);
            if(resultUserRole.Succeeded)
            {
                return View();
            }
            return View();
        }
        public IActionResult Register()
        {
            RegisterViewModel model=new RegisterViewModel();
            List<ProjectUserRole> UsersRole = RoleManager.Roles.ToList();
            model.Names = new SelectList(UsersRole, "Name", "Name");
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new ProjectUser()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    
                };
                
                var resultUser = await UserManager.CreateAsync(user, model.password);
                var resultUserRole = await UserManager.AddToRoleAsync(user, model.Name);
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Test project", "Asp.Net992@gmail.com"));
                message.To.Add( MailboxAddress.Parse(user.Email));
                message.Subject = "test mail in asp.net core";
                message.Body = new TextPart("plain")
                {
                    Text = "hello world mail"
                };
                SmtpClient client = new SmtpClient();
                try
                {
                    client.Connect("smtp.gmail.com", 465, true);
                    client.Authenticate("Asp.Net992@gmail.com", "Email");
                    client.Send(message);
                    Console.WriteLine("Email sent");  
                }
                catch (Exception ex )
                {

                    Console.WriteLine(ex);
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
                
                    
                   
                   
                  
                
                if (resultUser.Succeeded&& resultUserRole.Succeeded)
                {
                    
                    return RedirectToAction("CustomerMainPage", "Customer");
                }
            }
            return View();
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel model)
        {
            var user = await UserManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
               
                if (result.Succeeded)
                {
                    return RedirectToAction("CustomerMainPage", "Customer");
                }
                
            }
            return View();
        }

      
        public async Task<IActionResult> SignOut()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
