using NUnit.Framework;
using System.Collections.Generic;

namespace AdventOfCode2022.Problems.Day10
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestProblemOne()
        {
            var value = Code.ProblemOne();
            Assert.AreEqual(value, 16020);
        }

        [Test]
        public void TestProblemTwo()
        {
            var value = Code.ProblemTwo();
            Assert.AreEqual(value, new List<string>
            {
                {"####..##..####.#..#.####..##..#....###.."},
                {"#....#..#....#.#..#....#.#..#.#....#..#."},
                {"###..#......#..#..#...#..#..#.#....#..#."},
                {"#....#.....#...#..#..#...####.#....###.."},
                {"#....#..#.#....#..#.#....#..#.#....#.#.."},
                {"####..##..####..##..####.#..#.####.#..#."},
            });
        }
    }
}