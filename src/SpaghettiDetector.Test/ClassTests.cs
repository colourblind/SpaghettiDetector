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
}
