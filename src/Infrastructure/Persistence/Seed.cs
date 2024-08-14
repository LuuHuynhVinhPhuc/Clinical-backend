using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Persistence.Context;
using ClinicalBackend.Services.Constants;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalBackend.Persistence
{
    public static class Seed
    {
        public static void ApplySeeding(IServiceProvider serviceProvider)
        {
            ApplicationDbContext context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            if (context == null) throw new ArgumentNullException(nameof(context));

            if (!context.Roles.Any())
            {
                List<Role> roles = new()
                {
                    new Role(){Name="Admin",Id=(int)ROLE.Admin},
                    new Role(){Name="User",Id=(int)ROLE.User}
                };
                context.Roles.AddRange(roles);
                context.SaveChanges();
            }
            if (!context.Users.Any())
            {
                List<User> users = new()
                {
                    new User(){UserName = "Admin",HashPassword=BCrypt.Net.BCrypt.HashPassword("admin123"),RoleId=(int)ROLE.Admin },
                    new User(){UserName = "User",HashPassword=BCrypt.Net.BCrypt.HashPassword("test123"),RoleId=(int)ROLE.User }
                };
                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }
    }
}
