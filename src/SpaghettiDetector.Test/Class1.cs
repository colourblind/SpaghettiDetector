using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace SpaghettiDetector.Test
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test()
        {
            SpaghettiDetector s = new SpaghettiDetector("SpaghettiDetector.Test.dll");
            IEnumerable<Node> result = s.Run();
        }
    }
}
