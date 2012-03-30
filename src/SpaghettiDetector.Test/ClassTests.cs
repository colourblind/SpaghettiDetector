using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Mono.Cecil;

namespace SpaghettiDetector.Test
{
    [TestFixture]
    class ClassTests : TestBase
    {
        [Test]
        public void TestBasic1()
        {
            var n = GetNode("TestClassA");
            Assert.That(n.Dependencies.Where(x => x.ClassName == "TestClassAA").Count() > 0);
        }

        [Test]
        public void TestBasic2()
        {
            var n = GetNode("TestClassA");
            Assert.That(n.Dependencies.Where(x => x.ClassName == "TestClassAA").Count() > 0);
        }
    }
}
