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




        // to update book details query hit 2 times first to et details and second to update

        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int bookId ,[FromBody] Book model)
        {
            var book = appDbContext.Books.FirstOrDefault(x => x.Id == bookId);
            if(book == null)
            {
                return NotFound("Book not found");
            }
            book.Title = model.Title;
            book.Description = model.Description; 
            book.NoOfPages = model.NoOfPages;
            await appDbContext.SaveChangesAsync();

            return Ok(model);

        }





        // to add book with author details

        //      {

        //"title": "Testing with Author 1",
        //"description": "ABC",
        //"noOfPages": 13,
        //"isActive": true,
        //"createdOn": "2025-09-26T07:28:43.483Z",
        //"languageId": 2,

        //"author": {

        //  "name": "Nitin",
        //  "email": "nitin@gmail.com"
        //}
        //}




    }
}
