using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Future
{
    class RecSum : IArraySum
    {
        public int Sum (int[] arr)
        {
            if (arr.Length == 1)
            {
                return arr[0];
            }


            int[] tempArrFirstPart = new int[arr.Length / 2];
            Array.Copy(arr, 0, tempArrFirstPart, 0, arr.Length / 2);

            Task<int> first = Task.Run(() =>
            {
                return Sum(tempArrFirstPart);
            });


            int[] tempArrSecondPart = new int[arr.Length / 2 + arr.Length % 2];
            Array.Copy(arr, arr.Length / 2, tempArrSecondPart, 0, arr.Length / 2 + arr.Length % 2);

            Task<int> second = Task.Run(() =>
            {
                return Sum(tempArrSecondPart);
            });

            Task.WaitAll();

            int sum = first.Result + second.Result;
            return sum;
        }
    }
}
