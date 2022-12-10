using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022.Problems.Day10
{
    internal class Code
    {
        public static int ProblemOne()
        {
            var instructions = GetInstructions();
            var queue = new Queue<Instruction>();
            var answer = 0;

            foreach (var instruction in instructions)
            {
                if (instruction.Type == InstructionType.add)
                {
                    queue.Enqueue(new Instruction { Type = InstructionType.noop });
                }
                queue.Enqueue(instruction);
            }

            var x = 1;
            var cycle = 1;
            var i = 20;

            while (queue.Count > 0) 
            {
                if (cycle == i && i <= 220)
                {
                    answer += i * x;
                    i += 40;
                }

                var instruction = queue.Dequeue();
                if (instruction.Type == InstructionType.add)
                {
                    x += (int)instruction.Value;
                }
                cycle++;
            }

            return answer;
        }

        public static int ProblemTwo()
        {
            return 0;
        }

        private static List<Instruction> GetInstructions()
        {
            string[] lines = System.IO.File.ReadAllLines(@"./Problems/Day10/day10.txt");
            var instructions = new List<Instruction>();

            foreach (var line in lines)
            {
                var instruction = new Instruction();
                var input = line.Split(" ");

                if (input.Length > 1)
                {
                    instruction.Register = input[0].Last();
                    instruction.Value = int.Parse(input[1]);
                    input[0] = input[0].Remove(input[0].Length - 1);
                } 
                instruction.Type = (InstructionType)Enum.Parse(typeof(InstructionType), input[0]);
                instructions.Add(instruction);
            }

            return instructions;
        }

        private struct Instruction
        {
            public InstructionType Type { get; set; } 
            public char? Register { get; set; }
            public int? Value { get; set; }
        }
        
        private enum InstructionType
        {
            add,
            noop
        }
    }
}
