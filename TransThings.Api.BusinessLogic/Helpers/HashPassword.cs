using System.Security.Cryptography;
using System.Text;

namespace TransThings.Api.BusinessLogic.Helpers
{
    public class HashPassword
    {
        private readonly string password;
        public string HashedPassword { get; private set; }

        public HashPassword(string password)
        {
            this.password = password;
            HashedPassword = CreateHashedPassword();
        }

        private string CreateHashedPassword()
        {
            SHA256 hashedPassword = SHA256.Create();
            byte[] bytes = hashedPassword.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder stringBuilder = new StringBuilder();

            foreach (byte x in bytes)
                stringBuilder.Append(x.ToString("x2"));

            return stringBuilder.ToString();
        }
    }
}
