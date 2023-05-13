using System.Security.Claims;
using MCCustomers.Models;
using MCCustomers.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MCCustomers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly MccustomersContext Database;

        public AuthController(MccustomersContext database, IConfiguration configuration)
        {
            Database = database;
            _configuration = configuration;
        }


        [HttpPost("register")]
        public ActionResult<Customer> Register(CustomerDto request)
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

                Database.Customers.Add(customer);

                Database.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return Ok();
        }

        [HttpPost("login")]
        public ActionResult<string> Login(CredentialsDto credentials)
        {
            try
            {
                var customer = Database.Customers
                .Select(x => new Customer
                {
                    Email = x.Email,
                    Password = x.Password
                })
                .Where(x => x.Email == credentials.Email)
                .FirstOrDefault();


                if (customer!.Email != credentials.Email)
                {
                    return BadRequest("User not found");
                }


                if (!BCrypt.Net.BCrypt.Verify(credentials.Password, customer.Password))
                {
                    return BadRequest("Wrong password.");
                }

                string token = CreateToken(customer);

                //var refreshToken = GenerateRefreshToken();
                //SetRefreshToken(refreshToken);

                return Ok(token);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        
        
        private string CreateToken(Customer customer)
        {
            string? appSettingsToken = _configuration.GetSection("AppSettings:Token").Value;

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, customer.Email),
                new Claim(ClaimTypes.Role, "User")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettingsToken!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
