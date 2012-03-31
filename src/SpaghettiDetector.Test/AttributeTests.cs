using System;
using System.Linq;
using NUnit.Framework;

namespace SpaghettiDetector.Test
{
    [TestFixture]
    class AttributeTests : TestBase
    {
        [Test]
        public void AttributeClassTarget()
        {
            var n = GetNode("TestAttributeClassTarget");
            Assert.That(n.Dependencies.Where(x => x.ClassName == "TestTargetAttribute").Count() > 0);
        }

        [Test]
        public void AttributeFieldTarget()
        {
            var n = GetNode("TestAttributeFieldTarget");
            Assert.That(n.Dependencies.Where(x => x.ClassName == "TestTargetAttribute").Count() > 0);
        }

        [Test]
        public void AttributePropertyTarget()
        {
            var n = GetNode("TestAttributePropertyTarget");
            Assert.That(n.Dependencies.Where(x => x.ClassName == "TestTargetAttribute").Count() > 0);
        }

        [Test]
        public void AttributeMethodTarget()
        {
            var n = GetNode("TestAttributeMethodTarget");
            Assert.That(n.Dependencies.Where(x => x.ClassName == "TestTargetAttribute").Count() > 0);
        }

    }

    #region Test Classes

    [TestTarget]
    class TestAttributeClassTarget
    {
        int foo { get; set; }
    }

    class TestAttributeFieldTarget
    {
        [TestTarget]
        int foo;
    }

    class TestAttributePropertyTarget
    {
        [TestTarget]
        int foo { get; set; }
    }

    class TestAttributeMethodTarget
    {
        [TestTarget]
        int Foo()
        {
            return 1;
        }
    }

    class TestTargetAttribute : Attribute
    {

    }

    #endregion
}
