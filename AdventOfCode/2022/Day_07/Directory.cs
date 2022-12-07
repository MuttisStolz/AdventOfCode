using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Directory
    {
        public string Name { get; set; }
        public string? ParentName { get; set; }
        public Directory Parent { get; set; } 
        public List<DataFiles> DataFiles { get; set; } = new List<DataFiles>();
        public List<Directory> Directories { get; set; } = new List<Directory>();

        public Directory(string name)
        {
            Name = name;
        }
        public Directory(string name, Directory parent)
        {
            Name = name;
            Parent = parent;
        }
        public long GetSize()
        {
            long res = 0;

            foreach(var d in Directories)
            {
                res += d.GetSize();
            }

            return res + DataFiles.Sum(f => f.Size);
        }
        public List<Directory> GetDirectoriesByMinSize(long Min)
        {
            Console.WriteLine(Name + " has size " + GetSize());
            var tmp = new List<Directory>();

            if (this.GetSize() > Min)
            {
                Console.WriteLine(Name + " can delete" );
                tmp.Add(this);
            }

            foreach (var child in Directories)
            {
                tmp.AddRange(child.GetDirectoriesByMinSize(Min));
            }

            return tmp;
        }
        public List<Directory> GetDirectoriesByMaxSize(long max)
        {
            var tmp = new List<Directory>();

            if(this.GetSize() <= max)
            {
                tmp.Add(this);
            }

            foreach(var child in Directories)
            {
                tmp.AddRange(child.GetDirectoriesByMaxSize(max));
            }

            return tmp;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"\t- {this.Name} (dir)");

            foreach (var f in DataFiles)
            {
                sb.AppendLine(f.ToString());
            }

            foreach (var child in Directories)
            {
                sb.AppendLine(child.ToString());
            }

            

            return sb.ToString();
        }
    }
}
