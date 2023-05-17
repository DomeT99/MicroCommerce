namespace MCCustomers.Utils
{
    public class CookieParams
    {
        public CookieOptions Generate(DateTime dateExpires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = dateExpires
            };

            return cookieOptions;
        }
    }
}