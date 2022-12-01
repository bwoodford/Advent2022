using NUnit.Framework;

namespace AdventOfCode2022.Problems.Day1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.AreEqual(Code.ProblemOne(), 69693);
        }
    }
}