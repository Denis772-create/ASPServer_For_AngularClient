using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auth.Common.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace Auth.Books.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private Repository _repository;

        public OrdersController(Repository repository)
        {
            _repository = repository;
        }
        private Guid UserId => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);


        [Authorize(Roles = "User")]
        [HttpGet("")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _repository.GetOrdersAsync();
            var books = await _repository.GetBooksAsync();

            if (!orders.Any(o => o.UserId == UserId)) 
                return Ok(Enumerable.Empty<Book>());

            var ordered = orders.Where(o => o.UserId == UserId);

            var booksOrderedId = ordered.Select(o => o.BookId);

            var orderedBooks = books.Where( ob => booksOrderedId.Contains(ob.Id));

            return Ok(orderedBooks);
        }
    }
}
