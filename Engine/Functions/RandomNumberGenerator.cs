using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Functions
{
    /// <summary>
    /// 随机数
    /// </summary>
    public static class RandomNumberGenerator
    {
        private static readonly RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
        public static int NumberBetween(int minimumValue, int maximumValue)
        {
            byte[] randomNumber = new byte[1];
            rNGCryptoServiceProvider.GetBytes(randomNumber);
            double asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);

            double multiplier = Math.Max(0, (asciiValueOfRandomCharacter / 255d) - 0.00000000001d);

            int range=maximumValue - minimumValue+1;
            double randomValueRange = Math.Floor(multiplier * range);
            return (int)(minimumValue + randomValueRange);
        }

        private static readonly Random random=new Random();
        public static int SimpleNumberBetween(int minimumValue, int maximumValue) { 
            return random.Next(minimumValue, maximumValue+1);
        }

    }

    
}
