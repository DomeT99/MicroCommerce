using System.Security.Cryptography;

namespace MCCustomers.Utils
{
    public class RefreshToken
    {
        public string? Token { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }


        public RefreshToken Generate()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }
    }
}