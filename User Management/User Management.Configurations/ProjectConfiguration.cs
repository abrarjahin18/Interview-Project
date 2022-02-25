using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using User_Management.Databases;

namespace User_Management.Configurations
{
    public class ProjectConfiguration
    {
       public static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ProjectDbContext>(c => c.UseSqlServer(@"Server=DESKTOP-QSU3AFC;Database=UserManagement; Integrated Security=true"));
        }
    }
}
