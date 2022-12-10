using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class ArrayExtension
    {
        public static Queue<T> ToQueue<T>(this string[] array, Func<string,T> func)
        {
            Queue<T> res = new Queue<T>();

            foreach (var entry in array)
            {
                res.Enqueue(func(entry));
            }

            return res;
        }
    }
}
