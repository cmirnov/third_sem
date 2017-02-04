using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Future
{
    class IterSum : IArraySum
    {
        private const int NumOfParts = 40;

        private int GetSum (int[] arr)
        {
            int sum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                sum += arr[i];
            }
            return sum;
        }

        public int Sum (int[] arr)
        {
            List<Task<int>> tasks = new List<Task<int>>();

            int sum = 0;

            for (int i = 0; i < NumOfParts - 1; i++)
            {
                int len = arr.Length / NumOfParts;
                int[] tempArr = new int[len];
                Array.Copy(arr, i * len, tempArr, 0, len);
                tasks.Add(Task.Run(() =>
                {
                    return GetSum(tempArr);
                }));
            }

            int lastLen = arr.Length - (NumOfParts - 1) * (arr.Length / NumOfParts);
            int[] LastTempArr = new int[lastLen];
            Array.Copy(arr, (NumOfParts - 1) * (arr.Length / NumOfParts), LastTempArr, 0, lastLen);
            tasks.Add(Task.Run(() =>
            {
                return GetSum(LastTempArr);
            }));

            Task.WaitAll();

            foreach (var task in tasks)
            {
                sum += task.Result;
            }
            return sum;
        }
    }
}
