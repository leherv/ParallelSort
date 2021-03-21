using System;
using System.Linq;

namespace ParallelSorting
{
    public static class TestData
    {
        public static int[] GetTestData()
        {
            var random = new Random();
            return Enumerable.Range(0, 100000)
                .Select(i => random.Next(0, 100000))
                .ToArray();
        }
    }
}