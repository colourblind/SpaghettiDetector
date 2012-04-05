using System;
using System.Collections.Generic;
using System.Linq;

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

            Settings settings = GetSettings(args);
            string assemblyName = args[args.Length - 1];

            SpaghettiDetector s = new SpaghettiDetector(assemblyName, settings);
            IEnumerable<Node> result = s.Run();

            // YamlSerialiser serialiser = new YamlSerialiser(result);
            JsonSerialiser serialiser = new JsonSerialiser(result);
            serialiser.Run(Console.Out);
        }

        static void Derp()
        {
            Console.WriteLine("Usage: {0} [-s] [-i:ns0,ns1,ns2] [-d:maxDepth] assembly", AppDomain.CurrentDomain.FriendlyName);
            Console.WriteLine();
            Console.WriteLine("-s   Suppress default ignore namespaces (currently System.* and Microsoft.*)");
            Console.WriteLine("-i   Provide a list of namespaces to ignore, separated by commas. Includes");
            Console.WriteLine("     children, so ignoring Colourblind will also ignore Colourblind.Web");
            Console.WriteLine("-d   Maximum depth to search to. Defaults to 3");
            Console.WriteLine();
        }

        static Settings GetSettings(string[] args)
        {
            Settings s = new Settings();

            if (args.Contains("-s") || args.Contains("-S"))
                s.NamespacesToIgnore.Clear();

            foreach (string token in args)
            {
                if (!token.StartsWith("-"))
                    continue;
                if (token.Length < 2)
                    continue;

                string[] argParts = token.Split(':');
                switch (token.ToLower()[1])
                {
                    case 'd':
                        if (argParts.Length < 2)
                            continue;
                        int maxDepth;
                        if (Int32.TryParse(argParts[1], out maxDepth))
                            s.MaxDepth = maxDepth;
                        break;
                    case 'i':
                        if (argParts.Length < 2)
                            continue;
                        s.NamespacesToIgnore.AddRange(argParts[1].Split(','));
                        break;
                }
            }

            return s;
        }
    }
}
