using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIAuthors.Entities;

namespace WebAPIAuthors.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public BooksController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Book>> Get(int id)
        {
            return await context.Books.Include(x => x.Author).FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Post(Book book)
        {
            var existsAuthor = await context.Authors.AnyAsync(x => x.Id == book.AuthorId);

            if (!existsAuthor)
            {
                return BadRequest($"{Messages.DONT_EXISTS_THE_AUTHOR_WITH_ID}:{book.AuthorId}.");
            }

            context.Add(book);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
