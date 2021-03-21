using System;

namespace ParallelSorting
{
    public static class Helper
    {
        public static void Swap(int[] a, int index1, int index2)
        {
            var tmp = a[index1];
            a[index1] = a[index2];
            a[index2] = tmp;
        }
        
        public static int[] CopyArray(int[] toCopy, int from, int to)
        {
            var newArr = new int[to - from];
            for (var i = 0; i < newArr.Length; i++)
            {
                newArr[i] = toCopy[from + i];
            }

            return newArr;
        }

        public static void PrintArray(int[] arr)
        {
            foreach (var t in arr)
            {
                Console.Write($"{t} ");
            }
        }
    }
}