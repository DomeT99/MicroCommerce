using MCCustomers.Models;
using MCCustomers.Models.Dto;
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
            Database = _database;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            try
            {
                Customer? customer = await Database.Customers
                                           .Select(x => x)
                                           .Where(x => x.Id == id)
                                           .FirstOrDefaultAsync();

                if (customer == null)
                {
                    return NotFound("Customer is not found.");
                }


                return Ok(customer);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateInfo(int id, CustomerDto newCustomerInfo)
        {
            try
            {
                Customer? customerInfo = await Database.Customers
                                              .Select(x => x)
                                              .Where(x => x.Id == id)
                                              .FirstOrDefaultAsync();


                customerInfo!.Name = newCustomerInfo.Name ?? customerInfo.Name;
                customerInfo!.Surname = newCustomerInfo.Surname ?? customerInfo.Surname;
                customerInfo!.Email = newCustomerInfo.Email ?? customerInfo.Email;
                customerInfo!.Phone = newCustomerInfo.Phone ?? customerInfo.Phone;


                await Database.SaveChangesAsync();

                return Ok(customerInfo);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
