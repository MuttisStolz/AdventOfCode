using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class ListExtension
    {
        public static string GetOutputString<T>(this List<T> list, char seperator = ' ')
        {
            StringBuilder sb = new StringBuilder();

            foreach (var entry in list)
            {
                sb.Append(entry);
                sb.Append(seperator);
            }

            return sb.ToString();
        }
    }
}
