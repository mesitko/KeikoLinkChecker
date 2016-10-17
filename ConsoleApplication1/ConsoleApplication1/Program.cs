using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var dat = new DateTime();
            int k;
            k = 0;
        }
    }

    public static class StringExtensions
    {
        public static string Format(this string str, params object[] args)
        {
            return string.Format(str, args);
        }
    }

    public class Hash
    {
        public static string PasswordSalt
        {
            get
            {
                var rng = new RNGCryptoServiceProvider();
                var buff = new byte[32];
                rng.GetBytes(buff);
                return Convert.ToBase64String(buff);
            }
        }

        public static string EncodePassword(string password, string salt)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] src = Encoding.Unicode.GetBytes(salt);
            byte[] dst = new byte[src.Length + bytes.Length];
            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inarray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inarray);
        }
    }

    public static class DateTimeAndDayOfWeekExtension
    {
        public static string[] SortedDays(this DateTime now)
        {
            var startIndex = (int)now.DayOfWeek;
            var j = 0;
            var days = new string[7];
            for (var i = startIndex; i < startIndex + 7; i++)
            {
                var day = (System.DayOfWeek)(i % 7);
                days[j] = day.ToString();
                j++;
            }
            return days;
        }

        public static string FormatedHourAmPm(this DateTime time)
        {
            return "{0}:{1}{2}".Format(time.Hour % 12, time.Minute, time.Hour / 12 > 0 ? "PM" : "AM");
        }
    }

    
}
