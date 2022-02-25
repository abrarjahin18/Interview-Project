using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using User_Management.Models.EntityModels.Identity;

namespace User_Management.Databases
{
    public class ProjectDbContext: IdentityDbContext<ProjectUser,ProjectUserRole,int>
    {
        public ProjectDbContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<ProjectUser> ProjectUsers { get; set; }
    }
}
