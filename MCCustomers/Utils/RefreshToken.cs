
using System.Security.Cryptography;

namespace MCCustomers.Utils
{
    public class RefreshToken
    {
        private string? Token { get; set; }
        private DateTime Expires { get; set; }
        private DateTime Created { get; set; } = DateTime.Now;


        public RefreshToken Generate()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
            };

            return refreshToken;
        }
    }
}