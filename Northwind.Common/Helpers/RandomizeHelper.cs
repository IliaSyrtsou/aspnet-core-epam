using System;

namespace Northwind.Common.Helpers
{
    public static class RandomizeHelper
    {
        public static byte[] GenerateBufferFromSeed(int size)
        {
            var _random = new Random();

            byte[] buffer = new byte[size];

            _random.NextBytes(buffer);

            return buffer;
        }
    }
}