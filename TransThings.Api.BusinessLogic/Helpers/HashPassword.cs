using System.Security.Cryptography;
using System.Text;

namespace TransThings.Api.BusinessLogic.Helpers
{
    public class HashPassword
    {
        private readonly string password;
        private readonly string salt;
        public string HashedPassword { get; private set; }

        public HashPassword(string password, string login)
        {
            this.password = password;
            salt = login;
            HashedPassword = CreateHashedPassword();
        }

        private string CreateHashedPassword()
        {
            SHA256 hashedPassword = SHA256.Create();
            var salted = password + salt;
            byte[] bytes = hashedPassword.ComputeHash(Encoding.UTF8.GetBytes(salted));

            StringBuilder stringBuilder = new StringBuilder();

            foreach (byte x in bytes)
                stringBuilder.Append(x.ToString("x2"));

            return stringBuilder.ToString();
        }
    }
}
