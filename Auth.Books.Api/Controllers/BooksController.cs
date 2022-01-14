using System.Threading.Tasks;
using Auth.Common.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Books.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly Repository _repository;

        public BooksController(Repository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            return Ok(await _repository.GetBooksAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _repository.GetBookById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Book book)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddBookAsync(book);
                return Ok(book);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Book book)
        {
            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(book);
                return Ok(book);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _repository.GetBookById(id);

            if (book != null)
                await _repository.RemoveAsync(book);

            return Ok(book);
        }
    }
}
