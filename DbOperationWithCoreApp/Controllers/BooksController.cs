using DbOperationWithCoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbOperationWithCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(AppDbContext appDbContext) : ControllerBase
    {


        // to insert records in table

        [HttpPost("")]
        public async Task<IActionResult> AddNewBook([FromBody] Book model)
        {
            appDbContext.Books.Add(model);
            await appDbContext.SaveChangesAsync();

            return Ok(model);

        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var BookList = await (from Books in appDbContext.Books
                                      select Books).ToListAsync();

            return Ok(BookList);
        }

        // insert bulk records
        [HttpPost("bulk")]
        public async Task<IActionResult> AddNewBooks([FromBody] List<Book> model)
        {
            appDbContext.Books.AddRange(model);
            await appDbContext.SaveChangesAsync();

            return Ok(model);

        }
    }
}
