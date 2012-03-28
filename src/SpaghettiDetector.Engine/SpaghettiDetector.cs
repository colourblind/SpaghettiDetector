﻿using System;
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

        public int MaxDepth
        {
            get;
            set;
        }

        public SpaghettiDetector()
        {
            MaxDepth = 3;
        }

        public IList<string> VisitedTypes
        {
            get;
            set;
        }

        public SpaghettiDetector(string assemblyPath) : this(assemblyPath, 3)
        {
        }

        public SpaghettiDetector(string assemblyPath, int maxDepth)
        {
            StartAssembly = AssemblyDefinition.ReadAssembly(assemblyPath);
            MaxDepth = maxDepth;
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
                result.Add(new Node(t, 0, VisitedTypes, MaxDepth));

            return result;
        }
    }
}
