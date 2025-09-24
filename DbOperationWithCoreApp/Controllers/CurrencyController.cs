using DbOperationWithCoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbOperationWithCoreApp.Controllers
{
    [Route("api/currencies")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly AppDbContext appDbContext;

        public CurrencyController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllCurrency()
        {
            //  var currencyList = appDbContext.Currencies.ToList();
            //var currencyList = (from Currencies in appDbContext.Currencies
            //                   select Currencies).ToList();


            //  var currencyList = await appDbContext.Currencies.ToListAsync();
            var currencyList = await (from Currencies in appDbContext.Currencies
                                select Currencies).ToListAsync();

            return Ok(currencyList);
        }



        // To get single record


        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetCurrencyByIdAsync([FromRoute] int Id)
        {



            var currencyList = await appDbContext.Currencies.FindAsync(Id);
           
            return Ok(currencyList);
        }

        // To get single record by name
        [HttpGet("{name}")]
        public async Task<IActionResult> GetCurrencyByNameAsync([FromRoute] string name)
        {



           // var currencyList = await appDbContext.Currencies.Where(x=> x.Title == name).FirstOrDefaultAsync();
            var currencyList = await appDbContext.Currencies.FirstOrDefaultAsync(x => x.Title == name);  // to get duplicate record without giving exception  (find first and return approch)

            return Ok(currencyList);
        }


        // To get single record from two parameters
        [HttpGet("{name}/{description}")]
        public async Task<IActionResult> GetCurrencyByNameDESCAsync([FromRoute] string name, [FromRoute] string description)
        {



            var currencyList = await appDbContext.Currencies.FirstOrDefaultAsync(x => x.Title == name && x.Description == description);

            return Ok(currencyList);
        }

    }
}
