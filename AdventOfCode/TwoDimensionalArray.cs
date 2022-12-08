using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class TwoDimensionalArray<T>
    {
        private T[,] Cluster;
        private int lastAddedRow = 0;
        public int RowCount { get; }
        public int ColumnCount { get; }

        public TwoDimensionalArray(int rows, int cloumns)
        {
            RowCount = rows;
            ColumnCount = cloumns;
            this.Cluster = new T[this.RowCount, this.ColumnCount];
        }

        public void Clear()
        {
            lastAddedRow = 0;
            this.Cluster = new T[this.RowCount, this.ColumnCount];
        }

        public void AddRow(List<T> values)
        {
            for(int i=0; i<values.Count; i++)
            {
                Cluster[lastAddedRow, i] = values[i];
            }

            lastAddedRow++;
        }

        public List<T> GetRow(int index)
        {
            List<T> tmp = new List<T>();

            for (int i = 0; i < ColumnCount; i++)
            {
                 tmp.Add(Cluster[index, i]);
            }

            return tmp;
        }

        public override string ToString()
        {
            StringBuilder res = new StringBuilder();

            for (int i = 0; i < RowCount; i++)
            {
                var row = this.GetRow(i);

                var rowString = row.Aggregate(new StringBuilder(),
                    (sb, cv) => sb.Append(cv?.ToString()))
                    .ToString();

                res.AppendLine(rowString);
            }

            return res.ToString();
        }

        public void RunThroughAllValues()
        {
            for (int r = 0; r < RowCount; r++)
            {
                for (int c = 0; c < ColumnCount; c++)
                {
                    var n = GetNeighborValues(r, c);
                    var current = Cluster[r, c];
                }
            }
        }

        public List<T> GetNeighborValues(int r, int c)
        {
            Console.WriteLine($"Search for Coardinate {r}|{c} with Value {Cluster[r, c]}");
            List <T> res = new List<T>();

            //UpperRow
            var ru = r-1;
            while (ru >= 0)
            {
                Console.WriteLine($"Found UpperRow {Cluster[ru, c]}");
                res.Add(Cluster[ru, c]);
                ru--;
            }

            //ButtonRow

            var rb = r+1;
            while (rb < RowCount)
            {
                Console.WriteLine($"Found BottomRow {Cluster[rb, c]}");
                res.Add(Cluster[rb, c]);
                rb++;
            }


            return res;
        }
    }
}
