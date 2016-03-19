using System.Collections.Generic;

namespace MiddleConsumer.Property
{
    public interface IGraph
    {
        IList<INode> NodeList { get; set; }
        IList<IEdge> EdgeList { get; set; }
    }

    public class Graph : IGraph
    {
        public IList<INode> NodeList { get; set; }

        public IList<IEdge> EdgeList { get; set; }

        public Graph()
        {
            NodeList = new List<INode>();
            EdgeList = new List<IEdge>();
        }
    }
}
