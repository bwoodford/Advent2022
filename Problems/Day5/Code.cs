using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problems.Day5
{
    internal class Code
    {
        public static string ProblemOne()
        {
            var stacks = GetStacks();
            var commands = GetCommands();
            var answer = "";

            foreach (var command in commands)
            {
                for (int i = 0; i < command.Move; i++)
                {
                    stacks[command.To].Push(stacks[command.From].Pop());
                }
            }

            foreach(var stack in stacks)
            {
                answer += stack.Pop();
            }
            
            return answer;
        }

        public static int ProblemTwo() 
        {
            return 0; 
        }

        private static List<Stack<string>> GetStacks()
        {
            var fileStacks = System.IO.File.ReadLines(@"./Problems/Day5/day5.txt").TakeWhile(x => x != "");
            var stacks = new List<Stack<string>>();

            var regex = new Regex(@"\[[A-Z]\]|\s{4}");

            foreach(var line in fileStacks.Reverse())
            {
                var matches = regex.Matches(line);
                for (int i = 0; i < matches.Count; i++)
                {
                    if (stacks.Count <= i) stacks.Insert(i, new Stack<string>());
                    var parsed = matches[i].Value.TrimStart('[').TrimEnd(']').Trim();
                    if (parsed != String.Empty) stacks[i].Push(parsed);
                }
            }

            return stacks;
        }

        private static List<Command> GetCommands()
        {
            var fileCommands = System.IO.File.ReadLines(@"./Problems/Day5/day5.txt")
                                                .SkipWhile(x => x != "")
                                                .ToList();
            // Remove empty line
            fileCommands.RemoveAt(0);

            var regex = new Regex(@"\d+");

            var commands = new List<Command>();

            foreach(var line in fileCommands)
            {
                var matches = regex.Matches(line);
                var command = new Command();
                for (int i = 0; i < matches.Count; i++)
                {
                    switch(i) {
                        case 0:
                            command.Move = int.Parse(matches[i].Value);
                            break;
                        case 1:
                            command.From = int.Parse(matches[i].Value) - 1;
                            break;
                        case 2:
                            command.To = int.Parse(matches[i].Value) - 1;
                            break;
                        default:
                            throw new ArgumentException("file commands formatting not supported");
                    }
                }
                commands.Add(command);
            }
            return commands;
        }

        private struct Command
        {
            public int Move { get; set; }
            public int From { get; set; }
            public int To { get; set; }
        }
    }
}
