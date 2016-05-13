using System.Collections.Generic;
using CPI.Graphing.GraphingEngine.Contracts.Dc;

namespace MiddleConsumer.Property
{
    public class Graph : IGraph
    {
        public IList<INode> NodeList { get; set; }

        public IList<IEdge> EdgeList { get; set; }

        public bool IsDirected { get; set; }

        public ICollection<DrawingProperty> DrawingPropertyList { get; set; }

        public Graph()
        {
            NodeList = new List<INode>();
            EdgeList = new List<IEdge>();
        }
    }
}
