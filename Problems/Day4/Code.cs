using System;
using System.Collections.Generic;

namespace AdventOfCode2022.Problems.Day4
{
    internal class Code
    {
        public static int ProblemOne()
        {
            string[] lines = System.IO.File.ReadAllLines(@"./Problems/Day4/day4.txt");

            var count = 0;

            foreach(var line in lines)
            {
                var lineRanges = line.Split(',');
                var ranges1 = Array.ConvertAll(lineRanges[0].Split('-'), int.Parse);
                var ranges2 = Array.ConvertAll(lineRanges[1].Split('-'), int.Parse);

                var total1 = ranges1[1] - ranges1[0];
                var total2 = ranges2[1] - ranges2[0];

                if ((total1 >= total2 && ranges2[0] >= ranges1[0] && ranges2[1] <= ranges1[1]) || 
                    (total1 <= total2 && ranges1[0] >= ranges2[0] && ranges1[1] <= ranges2[1]))
                {
                    count += 1;
                }
            }

            return count;
        }

        public static int ProblemTwo() 
        {
            string[] lines = System.IO.File.ReadAllLines(@"./Problems/Day4/day4.txt");

            var count = 0;

            foreach(var line in lines)
            {
                var lineRanges = line.Split(',');
                var ranges1 = Array.ConvertAll(lineRanges[0].Split('-'), int.Parse);
                var ranges2 = Array.ConvertAll(lineRanges[1].Split('-'), int.Parse);
                
                if ((ranges1[0] <= ranges2[0] && ranges1[1] >= ranges2[0]) || 
                    (ranges1[0] >= ranges2[0] && ranges1[0] <= ranges2[1]))
                {
                    count += 1;
                }
            }
            return count; 
        }
    }
}
