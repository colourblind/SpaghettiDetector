using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SpaghettiDetector.UI
{
    /**
     * This is not a generalised serialiser like YamlSerialiser!
     */
    class JsonSerialiser
    {
        private IEnumerable<Node> Nodes
        {
            get;
            set;
        }

        public JsonSerialiser(IEnumerable<Node> nodes)
        {
            Nodes = nodes;
        }

        public void Run(TextWriter writer)
        {
            IList<string> nodeData = new List<string>();
            IList<string> edgeData = new List<string>();

            foreach (Node node in Nodes)
                CheckNode(node, nodeData, edgeData);

            writer.WriteLine("var nodes = [");
            writer.WriteLine(String.Join(",\n\t", nodeData.ToArray()));
            writer.WriteLine("];");

            writer.WriteLine("var edges = [");
            writer.WriteLine(String.Join(",\n\t", edgeData.ToArray()));
            writer.WriteLine("];");
        }

        public void CheckNode(Node node, IList<string> nodeData, IList<string> edgeData)
        {
            string serialisedNode = String.Format("{{ id: '{0}' }}", node);

            if (nodeData.Contains(serialisedNode))
                return;

            nodeData.Add(serialisedNode);

            string serialisedEdge;
            foreach (Node n in node.Dependencies)
            {
                serialisedEdge = String.Format("{{ a: '{0}', b: '{1}' }}", node, n);
                if (!edgeData.Contains(serialisedEdge))
                    edgeData.Add(serialisedEdge);

                CheckNode(n, nodeData, edgeData);
            }
        }
    }
}
