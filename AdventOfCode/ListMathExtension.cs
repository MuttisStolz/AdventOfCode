using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class ListMathExtension
    {
        public static int Product(this IEnumerable<int> source)
        {

            return source.Count() > 0 ? source.Aggregate((a, x) => a * x) : 0;
        }

        public static double Product(this IEnumerable<double> source)
        {
            return source.Count() > 0 ? source.Aggregate((a, x) => a * x) : 0;
        }

        public static int Fraction(this IEnumerable<int> source)
        {

            return source.Count() > 0 ? source.Aggregate((a, x) => a / x) : 0;
        }

        public static double Fraction(this IEnumerable<double> source)
        {
            return source.Count() > 0 ? source.Aggregate((a, x) => a / x) : 0;
        }

        public static int Difference(this IEnumerable<int> source)
        {

            return source.Count() > 0 ? source.Aggregate((a, x) => a - x) : 0;
        }

        public static double Difference(this IEnumerable<double> source)
        {
            return source.Count() > 0 ? source.Aggregate((a, x) => a - x) : 0;
        }

        public static int Power(this IEnumerable<int> source)
        {

            return source.Count() > 0 ? source.Aggregate((a, x) => int.Parse(Math.Pow(a,x).ToString())) : 0;
        }

        public static double Power(this IEnumerable<double> source)
        {
            return source.Count() > 0 ? source.Aggregate((a, x) => Math.Pow(a, x)) : 0;
        }

    }
}
