using System;
using System.Linq;
using System.Threading.Tasks;
using ITSchool.Core.IRepositories;
using ITSchool.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ITSchool.DAL.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

                // Apply migrations if they are not applied
                context.Database.Migrate();

            }
        }
    }
}
