﻿using MCCustomers.Models;
using MCCustomers.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using MCCustomers.Utils;

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
            Token token = new(_configuration);

            try
            {
                var customer = GetCustomerFromEmail(credentials.Email!);


                if (!IsSameEmail(customer!.Email, credentials.Email!))
                {
                    return BadRequest("User not found");
                }


                if (!IsPassworEncrypt(credentials.Password!, customer.Password))
                {
                    return BadRequest("Wrong password.");
                }

                string jsonWebToken = token.Create(customer.Email);
                SetRefreshToken();

                return Ok(jsonWebToken);
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void SetRefreshToken()
        {

            var refreshToken = new RefreshToken().Generate();
            var cookieParams = new CookieParams().Generate(refreshToken.Expires);

            Response.Cookies.Append("refreshToken", refreshToken.Token!, cookieParams);
        }
        private Customer GetCustomerFromEmail(string email)
        {
            var customer = Database.Customers
                                   .Select(x => new Customer
                                   {
                                       Email = x.Email,
                                       Password = x.Password
                                   })
                                   .Where(x => x.Email == email)
                                   .FirstOrDefault();

            return customer!;
        }
        private static bool IsSameEmail(string loginEmail, string dbEmail)
        {
            return loginEmail == dbEmail;
        }
        private static bool IsPassworEncrypt(string loginPassword, string dbPassword)
        {
            return BCrypt.Net.BCrypt.Verify(loginPassword, dbPassword);
        }

    }
}
