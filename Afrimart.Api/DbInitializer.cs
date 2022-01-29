using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Afrimart.DataAccess;
using Afrimart.DataAccess.DataModels;
using Afrimart.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Afrimart.Api
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new AfrimartDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AfrimartDbContext>>());
            context.Database.EnsureCreated();

            AddRoles(new List<string>()
            {
                AfrimartConstants.SELLER_ROLE, AfrimartConstants.ADMIN_ROLE, AfrimartConstants.SUPER_ADMIN_ROLE
            }, context); 

            context.SaveChanges();
        }

        private static void AddRoles(List<string> roles, AfrimartDbContext ctx)
        {
            foreach (var name in roles)
            {
                var role = ctx.Roles.SingleOrDefault(r => r.Name.Equals(name));
                if (role == null)
                {
                    ctx.Roles.Add(new Role()
                    {
                        Name = name
                    });
                }
            }
        }
    }
}
