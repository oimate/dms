using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmspl.common
{
    static public class Obfuscation
    {
        public static string Code(string u, string p)
        {
            string result = "";
            int numeric;
            string convertedPassword = "", hexNumeric;

            u = u.ToLower();
            for (int i = 0; i < p.Length; i++)
            {
                numeric = u[i % u.Length] + p[i];
                hexNumeric = string.Format("{0:X3}", numeric);
                convertedPassword += hexNumeric;
            }
            byte[] encodedPassword = System.Text.Encoding.UTF8.GetBytes(convertedPassword.ToUpper());
            string sEncodedPassword = System.Convert.ToBase64String(encodedPassword);
            char[] encodedChars = sEncodedPassword.ToCharArray();
            for (int i = 0; i < sEncodedPassword.Length; i++)
            {
                result += String.Format("{0:X}", (int)sEncodedPassword[i]);
            }
            return result;
        }

        public static string Decode(string user, string password)
        {
            string result = "", usr = user.ToLower();
            char[] ch1 = new char[password.Length / 2];
            byte[] b1;

            for (int i = 0; i < password.Length / 2; i++)
            {
                ch1[i] = (char)(int.Parse(password.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber));
            }

            b1 = System.Convert.FromBase64CharArray(ch1, 0, ch1.Length);

            int[] i1 = new int[b1.Length / 3];

            for (int i = 0; i < i1.Length; i++)
            {
                string a = ((char)b1[i * 3]).ToString() + ((char)b1[i * 3 + 1]).ToString() + ((char)b1[i * 3 + 2]).ToString();
                i1[i] = int.Parse(a, System.Globalization.NumberStyles.HexNumber);
            }

            int usi = 0;
            for (int i = 0; i < i1.Length; i++)
            {
                int c = i1[i] - (int)usr[usi];

                result += (char)(c);

                usi++;
                if (usi > (usr.Length - 1))
                {
                    usi = 0;
                }
            }

            return result;
        }
    }
}
