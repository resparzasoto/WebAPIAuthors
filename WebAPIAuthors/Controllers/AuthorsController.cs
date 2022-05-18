using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIAuthors.Entities;

namespace WebAPIAuthors.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public AuthorsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Author>>> Get()
        {
            return await context.Authors.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Author author)
        {
            context.Add(author);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
