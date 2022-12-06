using System.Collections.Generic;

namespace AdventOfCode2022.Problems.Day6
{
    internal class Code
    {
        public static int ProblemOne()
        {
            string input = System.IO.File.ReadAllText(@"./Problems/Day6/day6.txt");
            char[] chars = input.ToCharArray();

            var curr = new HashSet<char>();

            var p1 = 0;
            var p2 = 0;

            curr.Add(chars[p1]);

            while (p2 < chars.Length)
            {
                p2++;
                if (curr.Count == 4) {
                    return p2;
                }

                if (!curr.Contains(chars[p2]))
                {
                    curr.Add(chars[p2]);
                } else
                {
                    p1++;
                    p2 = p1;
                    curr.Clear();
                    curr.Add(chars[p1]);
                }
            }

            return 0;
        }

        public static int ProblemTwo()
        {
            string input = System.IO.File.ReadAllText(@"./Problems/Day6/day6.txt");
            char[] chars = input.ToCharArray();

            var curr = new HashSet<char>();

            var p1 = 0;
            var p2 = 0;

            curr.Add(chars[p1]);

            while (p2 < chars.Length)
            {
                p2++;
                if (curr.Count == 14)
                {
                    return p2;
                }

                if (!curr.Contains(chars[p2]))
                {
                    curr.Add(chars[p2]);
                }
                else
                {
                    p1++;
                    p2 = p1;
                    curr.Clear();
                    curr.Add(chars[p1]);
                }
            }

            return 0;
        }
    }
}
