using System;
using System.Collections.Generic;

namespace SpaghettiDetector.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Derp();
                return;
            }

            string assemblyName = args[0];
            int maxDepth = -1;
            if (!Int32.TryParse(args[args.Length - 1], out maxDepth))
                maxDepth = 3;

            SpaghettiDetector s = new SpaghettiDetector(assemblyName, maxDepth);
            IEnumerable<Node> result = s.Run();

            YamlSerialiser serialiser = new YamlSerialiser(result);
            serialiser.Run(Console.Out);

            //foreach (Node n in result)
            //    Draw(n, 0);
        }

        static void Draw(Node node, int depth)
        {
            System.Console.WriteLine("{0}{1}:{2}", new String(' ', depth * 2), node.AssemblyName, node.TypeName);

            foreach (Node n in node.Dependencies)
                Draw(n, depth + 1);
        }

        static void Derp()
        {
            Console.WriteLine("Usage: {0} assemblyName [maxDepth]", AppDomain.CurrentDomain.FriendlyName);
        }
    }
}
