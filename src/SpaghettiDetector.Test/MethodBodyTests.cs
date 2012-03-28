using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Mono.Cecil;
using NUnit.Framework;

namespace SpaghettiDetector.Test
{
    [TestFixture]
    public class MethodBodyTests
    {
        [Test]
        public void DetectVariableInMethodBody()
        {
            AssemblyDefinition a = AssemblyDefinition.ReadAssembly("SpaghettiDetector.Test.dll");
            TypeDefinition t = a.MainModule.GetType("SpaghettiDetector.Test", "TestClassMethod");

            Node n = new Node(t, 0, new List<string>(), 1);

            Assert.That(n.Dependencies.Where(x => x.TypeName.EndsWith("TestClassA")).Count() > 0);
        }

        [Test]
        public void DetectUnassignedVariableInMethodBody()
        {
            AssemblyDefinition a = AssemblyDefinition.ReadAssembly("SpaghettiDetector.Test.dll");
            TypeDefinition t = a.MainModule.GetType("SpaghettiDetector.Test", "TestClassMethod2");

            Node n = new Node(t, 0, new List<string>(), 1);

            Assert.That(n.Dependencies.Where(x => x.TypeName.EndsWith("TestClassMethod")).Count() > 0);
        }
    }
}
