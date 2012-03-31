using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace SpaghettiDetector.Test
{
    [TestFixture]
    class GenericTests : TestBase
    {
        [Test]
        public void ConstructedGenericProperty()
        {
            var n = GetNode("GenericPropertyTestA");
            Assert.That(n.Dependencies.Where(x => x.ClassName == "GenericTestA").Count() > 0);
        }

        [Test]
        public void ConstructedNestedGenericProperty()
        {
            var n = GetNode("GenericPropertyTestB");
            Assert.That(n.Dependencies.Where(x => x.ClassName == "GenericTestA").Count() > 0);
        }

        [Test]
        public void ConstructedGenericField()
        {
            var n = GetNode("GenericFieldTestA");
            Assert.That(n.Dependencies.Where(x => x.ClassName == "GenericTestA").Count() > 0);
        }

        [Test]
        public void ConstructedNestedGenericField()
        {
            var n = GetNode("GenericFieldTestB");
            Assert.That(n.Dependencies.Where(x => x.ClassName == "GenericTestA").Count() > 0);
        }
    }

    #region Test Classes

    class GenericTestA
    {
        string Foo { get; set; }
    }

    class GenericPropertyTestA
    {
        IList<GenericTestA> Foo { get; set; }
    }

    class GenericPropertyTestB
    {
        IDictionary<string, IList<GenericTestA>> Foo { get; set; }
    }

    class GenericFieldTestA
    {
        IList<GenericTestA> _foo;
    }

    class GenericFieldTestB
    {
        IDictionary<string, IList<GenericTestA>> _foo;
    }

    #endregion
}
