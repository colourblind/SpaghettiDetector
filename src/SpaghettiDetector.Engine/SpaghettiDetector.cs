using System;
using System.Collections.Generic;
using Mono.Cecil;

namespace SpaghettiDetector
{
    public class SpaghettiDetector
    {
        public AssemblyDefinition StartAssembly
        {
            get;
            set;
        }

        private Settings Settings
        {
            get;
            set;
        }

        public IList<string> VisitedTypes
        {
            get;
            set;
        }

        public SpaghettiDetector(string assemblyPath) : this(assemblyPath, new Settings())
        {
            
        }

        public SpaghettiDetector(string assemblyPath, Settings settings)
        {
            StartAssembly = AssemblyDefinition.ReadAssembly(assemblyPath);
            Settings = settings;
            VisitedTypes = new List<string>();
        }

        public IEnumerable<Node> Run()
        {
            if (StartAssembly == null)
                throw new ArgumentNullException("StartAssembly");

            return Run(StartAssembly);
        }

        private IEnumerable<Node> Run(AssemblyDefinition a)
        {
            VisitedTypes.Clear();
            IList<Node> result = new List<Node>();

            foreach (TypeDefinition t in a.MainModule.GetTypes())
                result.Add(new Node(t, 0, VisitedTypes, Settings));

            return result;
        }
    }
}
