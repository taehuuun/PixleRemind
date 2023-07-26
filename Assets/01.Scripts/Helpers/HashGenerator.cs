using System.Security.Cryptography;
using System.Text;

public static class HashGenerator
{
    public static string GeneratorHash(string input)
    {
        using (SHA256 sha256 = SHA256.Create(input))
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}