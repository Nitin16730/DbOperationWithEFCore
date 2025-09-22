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

    }
}
