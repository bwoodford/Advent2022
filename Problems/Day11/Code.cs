using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022.Problems.Day11
{
    internal class Code
    {
        public static int ProblemOne()
        {
            const int rounds = 20;
            var monkeyBusiness = GetMonkeyBusiness();

            for(int i = 0; i < rounds; i++)
            {
                for (var j = 0; j < monkeyBusiness.Count; j++)
                {
                    var monkey = monkeyBusiness[j];

                    while(monkey.Items.Count > 0)
                    {
                        var item = monkey.Items.Dequeue();
                        monkey.Inspected += 1;
                        var upLvl = monkey.WorryLevel(item);
                        upLvl /= 3;
                        
                        if (upLvl % monkey.Test.Divisible == 0)
                        {
                            monkeyBusiness.Where(x => x.Number == monkey.Test.True).First().Items.Enqueue(upLvl);
                        } else
                        {
                            monkeyBusiness.Where(x => x.Number == monkey.Test.False).First().Items.Enqueue(upLvl);
                        }
                    }
                }
            }

            var answer = monkeyBusiness.OrderByDescending(x => x.Inspected).ToList();
            return answer[0].Inspected * answer[1].Inspected;
        }

        public static int ProblemTwo()
        {
            return 0;
        }
        
        private static List<Monkey> GetMonkeyBusiness()
        {
            string[] lines = System.IO.File.ReadAllLines(@"./Problems/Day11/day11.txt");
            var business = new List<Monkey>();

            var monkey = new Monkey();
            var items = new Queue<int>();
            var operations = new List<string>();
            var test = new Test();

            foreach (var line in lines)
            {
                if (line.Contains("Monkey"))
                {
                    var tmpMon = line.Split(" ");
                    monkey.Number = int.Parse(tmpMon[tmpMon.Length - 1].TrimEnd(':'));
                } else if(line.Contains("Starting"))
                {
                    var tmpStart = line.Split(": ");
                    var tmpItems = tmpStart[tmpStart.Length - 1].Split(", ").ToList();
                    tmpItems.ForEach(x => items.Enqueue(int.Parse(x)));
                } else if(line.Contains("Operation"))
                {
                    var tmpOper = line.Split(": ");
                    var rawOper = tmpOper[tmpOper.Length - 1].Split(" ");
                    operations = rawOper.ToList();
                    operations.Reverse();
                } else if(line.Contains("Test"))
                {
                    var tmpTest = line.Split(" ");
                    test.Divisible = int.Parse(tmpTest[tmpTest.Length - 1]);
                } else if(line.Contains("If"))
                {
                    var tmpIf = line.Split(" ");
                    if (line.Contains("true"))
                    {
                        test.True = int.Parse(tmpIf[tmpIf.Length - 1]);
                    } else
                    {
                        test.False = int.Parse(tmpIf[tmpIf.Length - 1]);

                        monkey.Test = test;
                        monkey.Operations = operations;
                        monkey.Items = items;

                        business.Add(monkey);
             
                        monkey = new Monkey();
                        items = new Queue<int>();
                        operations = new List<string>();
                        test = new Test();
                    }
                } 
            }

            return business;
        }   

        private class Monkey
        { 
            public int Number { get; set; }
            public int Inspected { get; set; }
            public Queue<int> Items { get; set; }
            public List<string> Operations { get; set; }
            public Test Test { get; set; }
            
            public int WorryLevel(int item)
            {
                var buff = 0;
                var i = 0;
                var oper = "";

                while (Operations[i] != Operation.Equal)
                {
                    if (Operations[i] == Operation.Add)
                    {
                        oper = Operation.Add;
                    } else if (Operations[i] == Operation.Old)
                    {
                        if (oper == Operation.Add)
                        {
                            buff += item;
                        } else if (oper == Operation.Multiply)
                        {
                            buff *= item;
                        } else
                        {
                            buff = item;
                        }
                    } else if (Operations[i] == Operation.Multiply)
                    {
                        oper = Operation.Multiply;
                    } else if (int.TryParse(Operations[i], out buff)) 
                    {
                    }

                    i++;
                }

                return buff;
            }
        }

        private static class Operation
        {
            public static readonly string Old = "old";
            public static readonly string New = "new";
            public static readonly string Multiply = "*";
            public static readonly string Add = "+";
            public static readonly string Equal = "=";
        }
        
        private struct Test
        {
            public int Divisible { get; set; }
            public int True { get; set; }
            public int False { get; set; }
        }
    }
}
