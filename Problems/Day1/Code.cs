using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2022.Problems.Day1
{
    internal class Code
    {
        public static int ProblemOne() {
            try
            {
                string[] lines = System.IO.File.ReadAllLines(@"C:\Users\BrandonWoodford\source\repos\AdventOfCode2022\Problems\Day1\day1");

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

            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
