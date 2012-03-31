using System;
using System.Linq;
using NUnit.Framework;

namespace SpaghettiDetector.Test
{
    [TestFixture]
    class ClassTests : TestBase
    {
        [Test]
        public void Dependency()
        {
            var n = GetNode("TestClassA");
            Assert.That(n.Dependencies.Where(x => x.ClassName == "TestClassAA").Count() > 0);
        }

        [Test]
        public void MultipleDependencies()
        {
            var n = GetNode("TestClassAA");
            Assert.That(n.Dependencies.Where(x => x.ClassName == "TestClassAAA").Count() > 0);
            Assert.That(n.Dependencies.Where(x => x.ClassName == "TestClassAAB").Count() > 0);
        }

        [Test]
        public void ExternalDependency()
        {
            var n = GetNode("TestClassAAA");
            Assert.That(n.Dependencies.Where(x => x.ClassName == "SpaghettiDetector").Count() > 0);
        }

        [Test]
        public void NoDependencies()
        {
            var n = GetNode("TestClassAAB");
            Assert.That(n.Dependencies.Count == 0);
        }
    }

    #region Test Classes

    class TestClassA
    {
        int foo { get; set; }
        string bar { get; set; }
        TestClassAA a { get; set; }
    }

    class TestClassAA
    {
        int foo { get; set; }
        TestClassAAA a { get; set; }
        TestClassAAB b { get; set; }
    }

    class TestClassAAA
    {
        int foo { get; set; }
        SpaghettiDetector s { get; set; }
    }

    class TestClassAAB
    {
        int foo { get; set; }
    }

    #endregion
}
