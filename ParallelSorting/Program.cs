using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ParallelSorting
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const int numberOfRuns = 100;
            var testData = Array.Empty<int>();
            var sw = new Stopwatch();
            for (var i = 0; i < numberOfRuns; i++)
            {
                testData = TestData.GetTestData();
                sw.Start();
                // SerialQuickSort(testData);
                SerialMergeSort(testData);
                // testData = await Parallel_Merge_Sort(testData);
                // await ParallelQuickSort(testData);
                sw.Stop();
            }
            var elapsedMilliseconds = sw.Elapsed.TotalMilliseconds;
            var avgTimeMs = elapsedMilliseconds / numberOfRuns;
            Console.WriteLine($"Average execution time: {avgTimeMs.ToString()} ms.");
            
            // Helper.PrintArray(testData);
        }
        


        /***********************Parallel MergeSort***********************/

        private static async Task<int[]> ParallelMergeSort(int[] arr)
        {
            return arr.Length <= 1
                ? arr
                : await Parallel_Merge_Sort(arr);
        }

        private static async Task<int[]> Parallel_Merge_Sort(int[] arr)
        {
            if (arr.Length == 1)
                return arr;
            var middle = arr.Length / 2;
            var left = Helper.CopyArray(arr, 0, middle);
            var right = Helper.CopyArray(arr, middle, arr.Length);

            var leftTask = Parallel_Merge_Sort(left);
            var rightTask =  Parallel_Merge_Sort(right);
            var results = await Task.WhenAll(leftTask, rightTask);
            
            left = results[0];
            right = results[1];
            return Merge(left, right);
        }
        
        /***********************Serial MergeSort***********************/

        private static int[] SerialMergeSort(int[] arr)
        {
            return arr.Length <= 1
                ? arr
                : Merge_Sort(arr);
        }

        private static int[] Merge_Sort(int[] arr)
        {
            if (arr.Length == 1)
                return arr;
            var middle = arr.Length / 2;
            var left = Helper.CopyArray(arr, 0, middle);
            var right = Helper.CopyArray(arr, middle, arr.Length);
            left = Merge_Sort(left);
            right = Merge_Sort(right);
            return Merge(left, right);
        }
        
        private static int[] Merge(IReadOnlyList<int> first, IReadOnlyList<int> second)
        {
            var sizeFirst = first.Count;
            var sizeSecond = second.Count;
            var combinedArr = new int[sizeFirst + sizeSecond];
            var indexFirst = 0;
            var indexSecond = 0;
            for (var i = 0; i < combinedArr.Length; i++)
            {
                int nextVal;
                if (indexFirst == sizeFirst)
                {
                    nextVal = second[indexSecond++];
                }
                else if (indexSecond == sizeSecond)
                {
                    nextVal = first[indexFirst++];
                }
                else if (first[indexFirst] < second[indexSecond])
                {
                    nextVal = first[indexFirst++];
                }
                else
                {
                    nextVal = second[indexSecond++];
                }

                combinedArr[i] = nextVal;
            }

            return combinedArr;
        }
        
        /***********************Parallel QuickSort***********************/
        
        private static async Task ParallelQuickSort(int[] arr)
        {
            if (arr.Length <= 1)
                return;
            await Parallel_Quick_Sort(arr, 0, arr.Length - 1);
        }
        
        private static async Task Parallel_Quick_Sort(int[] arr, int left, int right)
        {
            if (left >= right)
                return;
            var pivot = Partition(arr, left, right);
            Parallel_Quick_Sort(arr, left, pivot - 1); 
            Parallel_Quick_Sort(arr, pivot + 1, right);
        }

        
        /***********************Serial QuickSort***********************/
        private static void SerialQuickSort(int[] arr)
        {
            if (arr.Length <= 1)
                return;
            Quick_Sort(arr, 0, arr.Length - 1);
        }

        private static void Quick_Sort(int[] arr, int left, int right)
        {
            if (left >= right)
                return;
            var pivot = Partition(arr, left, right);
            Quick_Sort(arr, left, pivot - 1);
            Quick_Sort(arr, pivot + 1, right);
        }

        private static int Partition(int[] arr, int left, int right)
        {
            var start = left;
            var end = right;
            var pivot = arr[right];
            while (left < right)
            {
                while (arr[left] < pivot && left < end)
                    left++;
                while (arr[right] >= pivot && right > start)
                    right--;
                if (left < right)
                    Helper.Swap(arr, left, right);
            }

            if (arr[left] > pivot)
                Helper.Swap(arr, left, end);

            return left;
        }
    }
}