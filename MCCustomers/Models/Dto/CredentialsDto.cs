using MCCustomers.Interface;

namespace MCCustomers.Models.Dto
{
    public class CredentialsDto : ICustomerCredentials
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
