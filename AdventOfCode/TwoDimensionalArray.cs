using System;
using System.Collections;
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

        public T Get(int row, int column)
        {
            return this.Cluster[row, column];
        }        

        public void RunThroughAllValues(Action<(int,int)> action)
        {
            for (int r = 0; r < RowCount; r++)
            {
                for (int c = 0; c < ColumnCount; c++)
                {
                    action((r, c));
                }
            }
        }

        public bool IsEdgeNode(int r, int c)
        {
            return r == 0 || c == 0 || r == RowCount-1 || c == ColumnCount-1;
        }

        public List<T> GetButtonRowValues(int r, int c)
        {
            List<T> res = new List<T>();

            var rb = r + 1;
            while (rb < RowCount)
            {
                res.Add(Cluster[rb, c]);
                rb++;
            }

            return res;
        }

        public List<T> GetUpperRowValues(int r, int c)
        {
            List<T> res = new List<T>();

            //UpperRow
            var ru = r - 1;
            while (ru >= 0)
            {
                res.Add(Cluster[ru, c]);
                ru--;
            }

            return res;
        }

        public List<T> GetLeftColumnValues(int r, int c)
        {
            List<T> res = new List<T>();

            var cl = c - 1;
            while (cl >= 0)
            {
                res.Add(Cluster[r, cl]);
                cl--;
            }

            return res;
        }

        public List<T> GetRightColumnValues(int r, int c)
        {
            List<T> res = new List<T>();

            var cr = c + 1;
            while (cr < ColumnCount)
            {
                res.Add(Cluster[r, cr]);
                cr++;
            }

            return res;
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
    }
}
