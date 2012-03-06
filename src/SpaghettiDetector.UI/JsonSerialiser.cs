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

        private IList<string> AssemblyList
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
            AssemblyList = new List<string>();

            foreach (Node node in Nodes)
                BuildAssemblyList(node);

            foreach (Node node in Nodes)
                CheckNode(node, nodeData, edgeData);

            writer.WriteLine("var nodes = [");
            writer.WriteLine(String.Join(",\n\t", nodeData.ToArray()));
            writer.WriteLine("];");

            writer.WriteLine("var edges = [");
            writer.WriteLine(String.Join(",\n\t", edgeData.ToArray()));
            writer.WriteLine("];");
        }

        private void BuildAssemblyList(Node node)
        {
            if (!AssemblyList.Contains(node.AssemblyName))
                AssemblyList.Add(node.AssemblyName);
            foreach (Node child in node.Dependencies)
                BuildAssemblyList(child);
        }

        public void CheckNode(Node node, IList<string> nodeData, IList<string> edgeData)
        {
            float hue = 360.0f * AssemblyList.IndexOf(node.AssemblyName) / AssemblyList.Count;
            string serialisedNode = String.Format("{{ id: '{0}', colour: '{1}' }}", node, Utils.GetColourHexStringFromHSV(hue, 1, 1));

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
