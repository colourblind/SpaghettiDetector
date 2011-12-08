using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;

namespace SpaghettiDetector
{
    public class Node
    {
        public string TypeName
        {
            get;
            private set;
        }

        public string AssemblyName
        {
            get;
            private set;
        }

        public IList<Node> Dependencies
        {
            get;
            private set;
        }

        public Node(TypeDefinition t, int depth, SpaghettiDetector s)
        {
            TypeName = t.Name;
            AssemblyName = t.Module.Assembly.Name.Name;
            Dependencies = new List<Node>();

            if (depth < s.MaxDepth)
            {
                IList<TypeReference> typeList = new List<TypeReference>();

                foreach (PropertyDefinition p in t.Properties)
                {
                    // Prevent duplicates of the same dependencies within this type
                    if (!typeList.Contains(p.PropertyType))
                        typeList.Add(p.PropertyType);
                }

                typeList = typeList.Where(x => !x.FullName.StartsWith("System") && !s.VisitedAssemblies.Contains(x.FullName)).ToList();

                // Split this off so we can add fields, arguments, etc. later
                foreach (TypeReference r in typeList)
                {
                    s.VisitedAssemblies.Add(r.FullName);
                    TypeDefinition typeDef = r.Resolve();
                    if (typeDef != null)
                        Dependencies.Add(new Node(r.Resolve(), depth + 1, s));
                }
            }
        }
    }
}
