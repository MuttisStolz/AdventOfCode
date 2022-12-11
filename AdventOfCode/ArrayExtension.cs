using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class ArrayExtension
    {
        public static Queue<T> ToQueue<T,T2>(this T2[] array)
        {
            Queue<T> res = new Queue<T>();

            foreach (var entry in array)
            {
                res.Enqueue((T)Convert.ChangeType(entry, typeof(T)));
            }

            return res;
        }

        public static Queue<T> ToQueue<T>(this T[] array)
        {
            Queue<T> res = new Queue<T>();

            foreach (var entry in array)
            {
                res.Enqueue(entry);
            }

            return res;
        }

        public static Queue<T> ToQueue<T>(this string[] array, Func<string,T> func)
        {
            Queue<T> res = new Queue<T>();

            foreach (var entry in array)
            {
                res.Enqueue(func(entry));
            }
            return res;
        }

        public static List<T> ToList<T>(this string[] array)
        {
            List<T> res = new List<T>();

            foreach(var entry in array)
            {
                res.Add((T)Convert.ChangeType(entry, typeof(T)));
            }

            return res;
        }

        public static List<T> ToList<T>(this string[] array, Func<string, T> func)
        {
            List<T> res = new List<T>();

            return res;
        }
    }
}
