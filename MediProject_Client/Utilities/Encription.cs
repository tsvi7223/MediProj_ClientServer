using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediProject_Server.Utilities
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class Encription
    {
        public static string ComputeSha256Hash(string rawData)
        {
            // יצירת מופע של SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // המרת המחרוזת למערך בתים
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // המרת מערך הבתים למחרוזת הקסדצימלית
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

      
    }
}
