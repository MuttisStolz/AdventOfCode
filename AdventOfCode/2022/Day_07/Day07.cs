using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Day02;

namespace AdventOfCode
{
    public class Day07 : IPuzzle
    {
        List<((string command,string argument), List<string> output)> Input 
            = new List<((string command, string argument), List<string> output)>();

        private Directory current;
        private Directory start;

        public Day07()
        {
            //var examplefile = "2022/Day_07/Example.txt";
            var fileName = "2022/Day_07/07input.txt";

            this.ReadInput(fileName);
            CreateFileSystem();
        }

        public void PuzzlePart1()
        {
            Console.WriteLine(start);

            var m = start.GetDirectoriesByMaxSize(100_000);

            Console.WriteLine($"Size = " + m.Sum(x=>x.GetSize()));

        }

        public void PuzzlePart2()
        {
            long DiscSpace =  70_000_000;
            long UpdateSize = 30_000_000;
            var unusedspace = DiscSpace - start.GetSize();
            var spaceToMakeFree = UpdateSize - unusedspace;

            var minDicToGetEnoughSpace = start
                .GetDirectoriesByMinSize(spaceToMakeFree)
                .MinBy(d=>d.GetSize());

            Console.WriteLine($"To Delete = " 
                + minDicToGetEnoughSpace?.Name 
                +" "+ minDicToGetEnoughSpace?.GetSize());
        }


        private void CreateFileSystem()
        {
            
            foreach (var entry in this.Input)
            {
                Console.WriteLine($"{entry.Item1.command} {entry.Item1.argument}");
                switch (entry.Item1.command)
                {
                    case "cd":
                        this.CD(entry.Item1.argument);
                        break;
                    case "ls":
                        this.LS(entry.output);
                        break;

                }
                //Console.WriteLine(start);
            }

            
        }

        private void LS(List<string> outputs)
        {
            foreach (var output in outputs)
            {
                var s = output.Split(' ');

                if (s[0].Equals("dir"))
                {
                    current.Directories.Add(new Directory(s[1],current));
                }
                else
                {
                    current.DataFiles.Add(new DataFiles(s[1], long.Parse(s[0])));
                }
            }
        }

        private void CD(string arg)
        {
            if(current != null && !arg.Equals(".."))
            {
                var change = current.Directories.FirstOrDefault(d => d.Name.Equals(arg));
                
                if (change != null)
                {
                    current = change;
                }
                else
                {
                    var d = new Directory(arg, current);
                    current = d;
                }
                
            }
            else if(arg.Equals(".."))
            {
                current = current.Parent;
            }
            else
            {
                current = new Directory(arg);
                start = current;
            }
        }



        private void ReadInput(string file)
        {
            (string command, string argument) command = new("", "");
            List<string> output = new List<string>();


            foreach (var line in File.ReadAllLines(file))
            {
                if (line.StartsWith("$"))
                {
                    if (!string.IsNullOrEmpty(command.command))
                    {
                        this.Input.Add((command, output));
                        command.command = string.Empty;
                        output = new List<string>();
                    }

                    var s = line.Split(' ');

                    command.command = s[1];
                    command.argument = s.Count() == 3
                        ? s[2]
                        : string.Empty;

                }
                else
                {
                    output.Add(line);
                }

            }

            if (!string.IsNullOrEmpty(command.command))
            {
                this.Input.Add((command, output));
            }
        }
    }
}
