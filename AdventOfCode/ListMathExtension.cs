using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class ListMathExtension
    {
        public static int MultiplicationProduct(this IEnumerable<int> source)
        {

            return source.Count() > 0 ? source.Aggregate((a, x) => a * x) : 0;
        }

        public static double MultiplicationProduct(this IEnumerable<double> source)
        {
            return source.Count() > 0 ? source.Aggregate((a, x) => a * x) : 0;
        }

    }
}
