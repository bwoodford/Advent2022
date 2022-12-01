using System;
using System.Linq;

namespace AdventOfCode2022.Problems.Day1
{
    internal class Code
    {
        public static int ProblemOne() {
            string[] lines = System.IO.File.ReadAllLines(@"./Problems/Day1/day1.txt");

            int highCount = 0;
            int buffCount = 0;

            foreach (string line in lines)
            {
                if (line == string.Empty)
                {
                    if (buffCount > highCount)
                    {
                        highCount = buffCount;
                    }
                    buffCount = 0;
                }
                else
                {
                    buffCount += Int32.Parse(line);
                }
            }

            return highCount;
        }

        public static int ProblemTwo() {
            string[] lines = System.IO.File.ReadAllLines(@"./Problems/Day1/day1.txt");

            int[] topCalories = new int[3];
            int buffCount = 0;

            foreach (string line in lines)
            {
                if (line == string.Empty)
                {
                    for (int i = 0; i < topCalories.Length; i++)
                    {
                        if (topCalories[i] < buffCount)
                        {
                            int tmp = topCalories[i];
                            topCalories[i] = buffCount;
                            buffCount = tmp;
                        }
                    }
                    buffCount = 0;
                }
                else
                {
                    buffCount += Int32.Parse(line);
                }
            }

            return topCalories.Sum();
        }
    }
}
