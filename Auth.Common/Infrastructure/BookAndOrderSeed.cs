using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Auth.Common.Infrastructure
{
    public class BookAndOrderSeed
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            try
            {
                if (context.Database.IsSqlServer())
                    context.Database.Migrate();

                if (!await context.Books.AnyAsync())
                {
                    await context.Books.AddRangeAsync(GetBooks());
                    await context.SaveChangesAsync();
                }

                if (!await context.Orders.AnyAsync())
                {
                    await context.Orders.AddRangeAsync(GetOrders());
                    await context.SaveChangesAsync();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        static IEnumerable<Order> GetOrders()
        {
            return new List<Order>
            {
                new Order(){BookId = 1, UserId = Guid.Parse("d8d3f620-b4a9-4a5c-bd18-02b1fbdfd697")},
                new Order(){BookId = 2, UserId = Guid.Parse("24ce6052-0390-4780-b710-ff90cdaa9156")}
            };

        }

        static IEnumerable<Book> GetBooks()
        {
            return new List<Book>()
         {
             new Book(){Author = "Ssdfadf", Title = "DFFDF", Price = 12M},
             new Book(){Author = "Ssdfadf", Title = "DFFDF", Price = 12M},
             new Book(){Author = "Ssdfadf", Title = "DFFDF", Price = 12M},
             new Book(){Author = "Ssdfadf", Title = "DFFDF", Price = 12M}
         };

        }
    }
}

