//This class is used to hold all different types of encryption algorithms
using System.Security.Cryptography;
using System.Text;

namespace UserManagement.Security
{
    internal class Encryption
    {
        protected internal static string MD5Encryption(string password)
        {
            MD5 md5Hash = MD5.Create();

            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder hashed = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
                hashed.Append(data[i].ToString("x2"));


            return hashed.ToString();
        }
    }
}
