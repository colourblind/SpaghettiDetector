using System;
using System.Collections.Generic;

namespace SpaghettiDetector
{
    public class Settings
    {
        public int MaxDepth
        {
            get;
            set;
        }

        public List<string> NamespacesToIgnore
        {
            get;
            set;
        }

        public Settings()
            : this(3, new List<string>() { "System", "Microsoft" })
        {

        }

        public Settings(int maxDepth)
            : this(maxDepth, new List<string>() { "System", "Microsoft" })
        {

        }

        public Settings(int maxDepth, List<string> namespacesToIgnore)
        {
            MaxDepth = maxDepth;
            NamespacesToIgnore = namespacesToIgnore;
        }
    }
}
