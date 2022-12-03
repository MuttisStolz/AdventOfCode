using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day03 : IPuzzle
    {
        public void PuzzlePart1()
        {
            var rucksacks = this.ReadRucksackInput();

            var sumPrio = rucksacks.Sum(r => r.Prio);

            Console.WriteLine($"sum of the priorities = {sumPrio} ");
        }

        public void PuzzlePart2()
        {
            var groups = this.ReadGroupFromInput();

            Console.WriteLine($"Found Groups = {groups.Count} ");
            Console.WriteLine($"Prio of all Groups = {groups.Sum(r => r.Prio)} ");
        }

        private List<Group> ReadGroupFromInput()
        {
            var res = new List<Group>();

            var lines = File.ReadAllLines("2022/Day_03/d03input.txt");
            Console.WriteLine($"Foud Lines {lines.Length}");

            while (lines.Length > 0)
            {                
                res.Add(new Group(lines.Take(3)));
                lines = lines.Skip(3).ToArray();
            }

            return res;
        }

        private List<Rucksack> ReadRucksackInput()
        {
            var res = new List<Rucksack>();

            foreach (var line in File.ReadAllLines("2022/Day_03/d03input.txt"))
            {
                var rucksack = new Rucksack(line);
                Console.WriteLine(rucksack);
                res.Add(rucksack);
            }
            return res;
        }
    }

    public class Group
    {
        public char Badges
        {
            get
            {
                return FindBadges();
            }
        }

        public int Prio
        {
            get
            {
                var res = Char.IsLower(Badges)
                    ? (int)Badges % 32
                    : (int)Badges % 32 + 26;

                return res;
            }
        }

        private IEnumerable<string> Lines;

        public Group(IEnumerable<string> enumerable)
        {
            this.Lines = enumerable;
            this.FindBadges();
        }

        private char FindBadges()
        {
            char? res = null;

            var t1 = Lines.ElementAt(0).ToCharArray().Distinct();
            var t2 = Lines.ElementAt(1).ToCharArray().Distinct();
            var t3 = Lines.ElementAt(2).ToCharArray().Distinct();

            int i = 0;

            foreach (var c in t1)
            {
                if (t2.Contains(c) && t3.Contains(c))
                {
                    i++;
                    Console.WriteLine($"Found {c}");
                    res = c;
                }
            }

            if (i != 1)
                throw new Exception($"ItemType not clear for \n{t1}\n{t2}\n{t3}");

            return res.Value;
        }
    }

    public class Rucksack
    {
        public string Item1 { get; set; }
        public string Item2 { get; set; }

        public char ItemType { get; set; }

        public int Prio 
        { 
            get 
            {
                var res = Char.IsLower(ItemType) 
                    ? (int)ItemType % 32 
                    : (int)ItemType % 32 + 26;

                return res; 
            } 
        }

        public Rucksack(string line)
        {
            this.FindItems(line);
            this.FindItemType();
        }

        private void FindItems(string line)
        {
            var half = line.Length / 2;
            this.Item1 = line.Substring(0, half).Trim();
            this.Item2 = line.Substring(half, half).Trim();

            if (Item1.Length != Item2.Length)
                throw new Exception("both Parts must be same length");
        }
        private void FindItemType()
        {
            int i = 0;
            var t1  = Item1.ToCharArray().Distinct();
            var t2 = Item2.ToCharArray().Distinct();

            foreach (var c in t1)
            {
                if(t2.Contains(c))
                {
                    i++;
                    this.ItemType = c;
                    Console.WriteLine($"Found {c}");
                }                
            }

            if (i != 1)
                throw new Exception($"ItemType not clear for \n{Item1}\n{Item2}");
        }
        public override string ToString()
        {
            return $"Rucksack with ItemType {this.ItemType} ";
        }
    }
}
