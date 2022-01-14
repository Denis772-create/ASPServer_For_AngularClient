using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Auth.Common.Infrastructure
{
    public class UsersSeed
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            try
            {
                if (context.Database.IsSqlServer())
                    context.Database.Migrate();

                if (!await context.Users.AnyAsync())
                {
                    await context.Users.AddRangeAsync(GetUsers());
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        static IEnumerable<User> GetUsers()
        {
            return new List<User>
         {
             new User()
             {
                 Id =   Guid.Parse("d8d3f620-b4a9-4a5c-bd18-02b1fbdfd697"),
                 Email = "dfsf@dsf.com",
                 Password = "user1",
                 Role = Role.User.ToString()
             },
             new User()
             {
                 Id =   Guid.Parse("24ce6052-0390-4780-b710-ff90cdaa9156"),
                 Email = "dfsfsd@dsf.com",
                 Password = "user2",
                 Role = Role.User.ToString()
             },
             new User()
             {
                 Id =   Guid.Parse("2a654947-fd73-407f-8a9f-aa0f9986780d"),
                 Email = "fsd@dsf.com",
                 Password = "admin",
                 Role = Role.Admin.ToString()
             }
         };

        }
    }
}
