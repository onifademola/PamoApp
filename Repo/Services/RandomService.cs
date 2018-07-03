using System;
using System.Security.Cryptography;

namespace Repo.Services
{
    public static class RandomService
    {
        public static readonly RNGCryptoServiceProvider _rngProvider = new RNGCryptoServiceProvider();

        public static int NextInt32()
        {
            var randomBuffer = new byte[4];
            _rngProvider.GetBytes(randomBuffer);
            return BitConverter.ToInt32(randomBuffer, 0);
        }

        // Generate a random real number within range [0.0, 1.0]
        public static double Next()
        {
            var buffer = new byte[sizeof(uint)];
            _rngProvider.GetBytes(buffer); // Fill the buffer
            uint random = BitConverter.ToUInt32(buffer, 0);
            return random / (1.0 + uint.MaxValue);
        }

        // Generate an int within range [min, max - 1] if max > min, and min if min == max
        public static int Next(int min, int max)
        {
            if (min > max) throw new ArgumentException($"Second input ({max}) must be greater than first input ({min}))");
            return (int)(min + (max - min) * Next());
        }

        // Generate an int within range [0, max - 1] if max > 1, and 0 if max == {0, 1}
        public static int Next(int max) => Next(0, max);
    }
}
