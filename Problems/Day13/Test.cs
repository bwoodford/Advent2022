using NUnit.Framework;

namespace AdventOfCode2022.Problems.Day13
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
            Assert.AreEqual(5806, value);
        }

        [Test]
        public void TestProblemTwo()
        {
            var value = Code.ProblemTwo();
            Assert.AreEqual(23600, value);
        }
    }
}