﻿using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace SpaghettiDetector
{
    public class Node
    {
        public string TypeName
        {
            get;
            private set;
        }

        public string ClassName
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

        public Node(TypeDefinition t, int depth, IList<string> visitedTypes, int maxDepth)
        {
            TypeName = t.FullName;
            ClassName = t.Name;
            AssemblyName = t.Module.Assembly.Name.Name;
            Dependencies = new List<Node>();

            if (depth < maxDepth)
            {
                IList<TypeReference> typeList = new List<TypeReference>();

                if (t.BaseType != null)
                {
                    typeList.Add(t.BaseType);
                }

                foreach (TypeReference i in t.Interfaces)
                {
                    typeList.Add(t);
                }

                foreach (PropertyDefinition p in t.Properties)
                {
                    typeList.Add(p.PropertyType);
                    typeList = typeList.Concat(p.CustomAttributes.Select(x => x.AttributeType)).ToList();
                }

                foreach (FieldDefinition f in t.Fields)
                {
                    typeList.Add(f.FieldType);
                    typeList = typeList.Concat(f.CustomAttributes.Select(x => x.AttributeType)).ToList();
                }

                foreach (CustomAttribute a in t.CustomAttributes)
                {
                    typeList.Add(a.AttributeType);
                }

                foreach (MethodDefinition m in t.Methods)
                {
                    foreach (CustomAttribute a in m.CustomAttributes)
                    {
                        typeList.Add(a.AttributeType);
                    }

                    if (m.Body != null) // Interfaces (and I guess abstracts)
                    {
                        foreach (VariableDefinition v in m.Body.Variables)
                        {
                            typeList.Add(v.VariableType);
                        }

                        // And now sift through the method line by line . . .
                        foreach (Instruction i in m.Body.Instructions)
                        {
                            if (i.Operand == null)
                                continue;

                            Type operandType = i.Operand.GetType();
                            TypeReference r;
                            if (operandType == typeof(FieldDefinition))
                                r = ((FieldDefinition)i.Operand).FieldType;
                            else if (operandType == typeof(MethodDefinition))
                                r = ((MethodDefinition)i.Operand).DeclaringType;
                            else if (operandType == typeof(MethodReference))
                                r = ((MethodReference)i.Operand).DeclaringType;
                            else
                                continue;

                            typeList.Add(r);
                        }
                    }
                }

                foreach (TypeReference r in typeList)
                    typeList = typeList.Concat(GetGenericArguments(r)).ToList();

                typeList = typeList.Where(x => 
                    !x.FullName.StartsWith("System")
                    && !x.FullName.StartsWith("Microsoft")
                    && !visitedTypes.Contains(x.FullName)
                ).Distinct().ToList();

                // Split this off so we can add fields, arguments, etc. later
                foreach (TypeReference r in typeList)
                {
                    visitedTypes.Add(r.FullName);
                    TypeDefinition typeDef = r.Resolve();
                    if (typeDef != null && typeDef != t) // Skip self-references
                        Dependencies.Add(new Node(typeDef, depth + 1, visitedTypes, maxDepth));
                }
            }
        }

        public override string ToString()
        {
            return TypeName;
        }

        private IList<TypeReference> GetGenericArguments(TypeReference t)
        {
            IList<TypeReference> result = new List<TypeReference>();

            if (t.IsGenericInstance)
            {
                foreach (TypeReference a in ((GenericInstanceType)t).GenericArguments)
                {
                    result.Add(a);
                    result = result.Concat(GetGenericArguments(a)).ToList();
                }
            }

            return result;
        }
    }
}
