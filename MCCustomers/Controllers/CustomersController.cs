using MCCustomers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MCCustomers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly MccustomersContext Database;

        public CustomersController(MccustomersContext _database)
        {
            this.Database = _database;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            try
            {
                if (Database.Customers is null)
                {
                    return NotFound();
                }

                Customer? customer = await Database.Customers
                                       .Select(x => x)
                                       .Where(x => x.Id == id)
                                       .FirstOrDefaultAsync();

                return Ok(customer);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
