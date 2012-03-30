using System;
using System.Collections.Generic;
using Mono.Cecil;

namespace SpaghettiDetector.Test
{
    internal class TestBase
    {
        private AssemblyDefinition TestAssembly
        {
            get;
            set;
        }

        protected TestBase()
        {
            TestAssembly = AssemblyDefinition.ReadAssembly("SpaghettiDetector.Test.dll");
        }

        protected Node GetNode(string testClass)
        {
            TypeDefinition t = TestAssembly.MainModule.GetType("SpaghettiDetector.Test", testClass);
            return new Node(t, 0, new List<string>(), 1);
        }

    }
}
