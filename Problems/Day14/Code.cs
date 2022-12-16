
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;


namespace AdventOfCode2022.Problems.Day14
{
    internal class Code
    {
        public static int ProblemOne()
        {
            var answer = 0;
            var mine = Mine.NewMine();
            var curr = Sand.SpawnSand();

            while (curr.Item2 <= mine.LowestBound)
            {
                if (mine.Collision((curr.Item1, curr.Item2 + 1)))
                {
                    if (!mine.Collision((curr.Item1 - 1, curr.Item2 + 1)))
                    {
                        curr.Item1 -= 1;
                        curr.Item2 += 1;
                    } else if (!mine.Collision((curr.Item1 + 1, curr.Item2 + 1)))
                    {
                        curr.Item1 += 1;
                        curr.Item2 += 1;
                    } else
                    {
                        answer++;
                        mine.Elements.Add(curr);
                        curr = Sand.SpawnSand();
                    }
                } else
                {
                    curr.Item2++;
                }
            }

            return answer;
        }

        public static int ProblemTwo()
        {
            var answer = 0;
            var mine = Mine.NewMine();
            mine.Part2 = true;
            mine.LowestBound += 2;
            var curr = Sand.SpawnSand();
            var stop = Sand.SpawnSand();

            while (true)
            {
                if (mine.Collision((curr.Item1, curr.Item2 + 1)))
                {
                    if (!mine.Collision((curr.Item1 - 1, curr.Item2 + 1)))
                    {
                        curr.Item1 -= 1;
                        curr.Item2 += 1;
                    } else if (!mine.Collision((curr.Item1 + 1, curr.Item2 + 1)))
                    {
                        curr.Item1 += 1;
                        curr.Item2 += 1;
                    } else
                    {
                        answer++;
                        mine.Elements.Add((curr.Item1,curr.Item2));
                        if (curr.Equals(stop)) break;
                        curr = Sand.SpawnSand();
                    }
                } else
                {
                    curr.Item2++;
                }
            }

            return answer;
        }
    }

    internal class Mine
    {
        public int LowestBound { get; set; }
        public HashSet<(int,int)> Elements { get; set; }
        public bool Part2 { get; set; }
        
        public bool Collision((int,int) el)
        {
            if (Part2 && el.Item2 == LowestBound)
            {
                return true; 
            }
            if (Elements.Contains(el))
            {
                return true; 
            }
            return false;
        }

        public static Mine NewMine()
        {
            var lines = System.IO.File.ReadAllLines(@"./Problems/Day14/day14.txt");
            var elements = new HashSet<(int, int)>();
            var mine = new Mine();
            var lowest = 0;

            for (var i = 0; i < lines.Length; i++)
            {
                var position = lines[i].Split(" -> ");
                for (var j = 0; j < position.Length; j++)
                {
                    var segment1 = position[j].Split(",");
                    if (j + 1 < position.Length)
                    {
                        var segment2 = position[j+1].Split(",");

                        var segX1 = int.Parse(segment1[0]);
                        var segY1 = int.Parse(segment1[1]);
                        var segX2 = int.Parse(segment2[0]);
                        var segY2 = int.Parse(segment2[1]);

                        if (segY2 > lowest) lowest = segY2;
                        if (segY1 > lowest) lowest = segY1;

                        if (segX1 < segX2)
                        {
                            for (var h = segX1; h <= segX2; h++)
                            {
                                elements.Add((h, segY1));
                            }
                        } else if (segX1 > segX2)
                        {
                            for (var h = segX2; h <= segX1; h++)
                            {
                                elements.Add((h, segY1));
                            }
                        } else if (segY1 < segY2)
                        {
                            for (var h = segY1; h <= segY2; h++)
                            {
                                elements.Add((segX1, h));
                            }
                        } else if (segY1 > segY2)
                        {
                            for (var h = segY2; h <= segY1; h++)
                            {
                                elements.Add((segX1, h));
                            }
                        }
                    }
                }
            }

            mine.Elements = elements;
            mine.LowestBound = lowest;
            return mine;
        }
    }

    internal class Sand
    {
        public static (int,int) SpawnSand()
        {
            return (500, 0);
        }
    }
}
