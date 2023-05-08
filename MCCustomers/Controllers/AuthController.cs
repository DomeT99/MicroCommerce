using MCCustomers.Models;
using MCCustomers.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MCCustomers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly MccustomersContext Database;

        public AuthController(MccustomersContext database)
        {
            Database = database;
        }

        
        [HttpPost("register")]
        public async Task<ActionResult<Customer>> Register(CustomerDto request)
        {
            Customer customer = new();
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            try
            {
                customer.Name = request.Name!;
                customer.Surname = request.Surname!;
                customer.Email = request.Email!;
                customer.Phone = request.Phone!;
                customer.Password = passwordHash;
                customer.Image = request.Image;

                await Database.Customers.AddAsync(customer);

                await Database.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return Ok();
        }
    }
}
