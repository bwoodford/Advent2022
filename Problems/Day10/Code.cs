using System;
using System.Collections.Generic;

namespace AdventOfCode2022.Problems.Day10
{
    internal class Code
    {
        public static int ProblemOne()
        {
            var queue = GetQueue();
            var answer = 0;

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

        public static List<string> ProblemTwo()
        {
            const int width = 40;
            var queue = GetQueue();
            var answer = new List<string>();

            var x = 1;
            var cycle = 1;
            var i = 40;

            var buff = new char[width];

            while (queue.Count > 0) 
            {
                var c = '.';
                var col = (cycle - 1) % width;

                if (x == col || x-1 == col || x+1 == col)
                {
                    c = '#';
                }

                buff[col] = c;

                if (cycle == i)
                {
                    answer.Add(new string(buff));
                    Array.Clear(buff, 0, width);
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
        
        private static Queue<Instruction> GetQueue()
        {
            var instructions = GetInstructions();
            var queue = new Queue<Instruction>();

            foreach (var instruction in instructions)
            {
                if (instruction.Type == InstructionType.add)
                {
                    queue.Enqueue(new Instruction { Type = InstructionType.noop });
                }
                queue.Enqueue(instruction);
            }
            return queue;
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
            public int? Value { get; set; }
        }
        
        private enum InstructionType
        {
            add,
            noop
        }
    }
}
