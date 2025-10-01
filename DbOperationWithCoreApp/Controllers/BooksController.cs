using DbOperationWithCoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
        public async Task<IActionResult> UpdateBook([FromRoute] int bookId, [FromBody] Book model)
        {
            var book = appDbContext.Books.FirstOrDefault(x => x.Id == bookId);
            if (book == null)
            {
                return NotFound("Book not found");
            }
            book.Title = model.Title;
            book.Description = model.Description;
            book.NoOfPages = model.NoOfPages;
            await appDbContext.SaveChangesAsync();

            return Ok(model);

        }



        // to update book details query hit 1 times 

        [HttpPut("")]
        public async Task<IActionResult> UpdateBookWithSingleQuery([FromBody] Book model)
        {
            appDbContext.Books.Update(model);
            await appDbContext.SaveChangesAsync();

            return Ok(model);

        }

        //// give input like this
        //        {

        //  "id": 6,
        //        "title": "Testing with Author 1 with 1 query update",
        //        "description": "ABC xyx",
        //        "noOfPages": 1003,
        //        "isActive": true,
        //        "createdOn": "2025-09-26T07:28:43.483",
        //        "languageId": 3,
        //        "authorId": 1

        //}



    // bulk update with single query

    [HttpPut("bulk")]
        public async Task<IActionResult> UpdateBookinbulk()
        {
            await appDbContext.Books.Where(x => x.Id < 3)
                .ExecuteUpdateAsync(x => x
                .SetProperty(p => p.Description, "This is bookdescription 2")
                .SetProperty(p => p.NoOfPages, 550)
                .SetProperty(p => p.Title, "This is book title 1 ")
            );

            return Ok();
        }





        // delete single record 

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> Deletebookbyuid([FromBody]  int bookId)
        {

            ////////// Double query hit

         //  // var book = appDbContext.Books.FirstOrDefault(x => x.Id == bookId.Id);
            //var book = await appDbContext.Books.FindAsync(bookId);
            //if (book == null)
            //{
            //    return NotFound("Book not found");
            //}
            //appDbContext.Books.Remove(book);
            //await appDbContext.SaveChangesAsync();
         

            ///////////Single query hit
            ///

            var book = new Book() { Id = bookId };
            appDbContext.Entry(book).State = EntityState.Deleted;
            await appDbContext.SaveChangesAsync();


            return Ok();

        }

        // delete bulk records


        [HttpDelete("bulk")]
            
        public async Task<IActionResult> Deletebookinbulk()
        {

            // multiple query hit

            //var books = await appDbContext.Books.Where(x => x.Id < 4).ToListAsync();
            //appDbContext.Books.RemoveRange(books);
            //await appDbContext.SaveChangesAsync();
               


            //single query hit

            var books = await appDbContext.Books.Where(x => x.Id < 7).ExecuteDeleteAsync();


            return Ok();

        }

        // Get Relational Data using navigational properties

        //[HttpGet("All")]
        //public async Task<IActionResult> GetBookswithAuthor()
        //{
        //    var BookList = await appDbContext.Books.Select(x => new
        //    {
        //      Id =  x.Id,
        //       Title = x.Title,
        //       Author = x.Author != null ? x.Author.Name:"NA",
        //       Language = x.Language 
              
                
        //    }).ToListAsync();
        //    return Ok(BookList);
        //}


        // Eager Loading in EF core

        //[HttpGet("AllWithEagerLoading")]
        //public async Task<IActionResult> GetBookswithAuthorEagerLoading()
        //{
        //    var BookList = await appDbContext.Books
        //        .Include(x => x.Author)
        //       // .Include(x => x.Language)
        //        .ToListAsync();
        //    return Ok(BookList);
        //}
         



        // Explicit Loading in EF core

        [HttpGet("AllWithExplicitLoading")] 
        public async Task<IActionResult> GetBookswithAuthorExplicitLoading()
        {
            var BookList = await appDbContext.Books.ToListAsync();

            foreach (var book in BookList)
            {
                await appDbContext.Entry(book).Reference(x => x.Author).LoadAsync();
               //  await appDbContext.Entry(book).Reference(x => x.Language).LoadAsync();
            }
           
            return Ok(BookList);
        }

        [HttpGet("AllWithExplicitLoading2")]

        public async Task<IActionResult> GetBookswithAuthorExplicitLoading2()
        {
          var LanguageList = await appDbContext.Language.ToListAsync();

            foreach (var languages in LanguageList)

            {
                await appDbContext.Entry(languages).Collection(x => x.Books).LoadAsync();
            }

            return Ok(LanguageList);

        }



        // Lazy Loading in EF core

        [HttpGet("AllWithLazyLoading")]
        public async Task<IActionResult> GetBookswithAuthorLazyLoading()
        {
            var books = await appDbContext.Books.ToListAsync();

            // Access navigation property -> triggers Lazy Loading (if proxies are enabled)
            foreach (var book in books)
            {
                var author = book.Author; // Lazy load happens here
            }

            return Ok(books);
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



        // Get data using SQL Qury


        [HttpGet("GetBooksUsingSQLQuery")]
        public async Task<IActionResult> GetBooksUsingSQLQuery()
        {
            //var bookList = await appDbContext.Books
            //    .FromSqlRaw("SELECT * FROM Books")
            //    .ToListAsync();

            var columnvalue = 8;
            var columnName = "Id";

            var parameter = new SqlParameter("Columnvalue", columnvalue);

            var bookList = await appDbContext.Books
                .FromSqlRaw($"SELECT * FROM Books WHERE {columnName} = @columnvalue", parameter)
                .ToListAsync();
            return Ok(bookList);
        }

        // Execute Sql Query on database

        [HttpPost("ExecuteSqlQuery")]

        public async Task<IActionResult> ExecuteSqlQuery()
        {
             var result = await appDbContext.Database.SqlQuery<int>($" SELECT COUNT(*) FROM Books").ToListAsync();
        // var  result = await appDbContext.Database.ExecuteSqlAsync($" UPDATE Books SET NoOfPages = 1200 WHERE Id = 8");
            return Ok(result);
        }
    }
}
