using Xunit;

namespace AdventOfCode
{
    public class Day06 : IPuzzle
    {
        string fileBuffer = File.ReadAllText("2022/Day_06/06input.txt");

        public void PuzzlePart1()
        {
            Console.WriteLine(GetStartMarker(this.fileBuffer, 4));
        }

        public void PuzzlePart2()
        {
            Console.WriteLine(GetStartMarker(this.fileBuffer, 14));
        }

        private int GetStartMarker(string buffer, int markerSize)
        {
            LinkedList<char> marker = new LinkedList<char>();            

            for (int i = 1; i <= buffer.Length; i++)
            {
                if(marker.Count < markerSize)
                {
                    marker.AddLast(buffer[i - 1]);

                    if (marker.Count != markerSize)
                    {
                        continue;
                    }
                }
                else
                {
                    marker.AddLast(buffer[i - 1]);
                    marker.RemoveFirst();
                }

                if(marker.Distinct().Count() == markerSize)
                {
                    return i;
                }
                
            }

            return -1;
        }

        public void Example()
        {
            string t1 = "mjqjpqmgbljsphdztnvjfqwrcgsmlb";
            string t2 = "bvwbjplbgvbhsrlpgdmjqwftvncz";
            string t3 = "nppdvjthqldpwncqszvftbrmjlhg";
            string t4 = "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg";
            string t5 = "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw";

            string t11 = "mjqjpqmgbljsphdztnvjfqwrcgsmlb";
            string t22 = "bvwbjplbgvbhsrlpgdmjqwftvncz";
            string t33 = "nppdvjthqldpwncqszvftbrmjlhg";
            string t44 = "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg";
            string t55 = "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw";

            Assert.Equal(7, GetStartMarker(t1, 4));
            Assert.Equal(5, GetStartMarker(t2, 4));
            Assert.Equal(6, GetStartMarker(t3, 4));
            Assert.Equal(10, GetStartMarker(t4, 4));
            Assert.Equal(11, GetStartMarker(t5, 4));

            Assert.Equal(19, GetStartMarker(t11, 14));
            Assert.Equal(23, GetStartMarker(t22, 14));
            Assert.Equal(23, GetStartMarker(t33, 14));
            Assert.Equal(29, GetStartMarker(t44, 14));
            Assert.Equal(26, GetStartMarker(t55, 14));
        }
    }
}
