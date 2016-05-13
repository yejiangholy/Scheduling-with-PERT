using System.Collections.Generic;
using System.Text;
using CPI.Graphing.GraphingEngine.Contracts.Dc;

namespace MiddleConsumer.Property
{

    public class Node : INode
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<IEdge> TailList { get; set; }

        public IList<IEdge> HeadList { get; set; }

        public ICollection<DrawingProperty> DrawingPropertyList { get; set; }


        public Node()
        {
            TailList = new List<IEdge>();
            HeadList = new List<IEdge>();
        }

        public override string ToString()
        {
            var sbHead = new StringBuilder();
            var sbTail = new StringBuilder();

            var firstItem = true;
            foreach (var edge in HeadList)
            {
                if (!firstItem) sbHead.Append(",");
                firstItem = false;
                sbHead.Append(edge.HeadNode.Name);
            }

            firstItem = true;
            foreach (var edge in TailList)
            {
                if (!firstItem) sbTail.Append(",");
                firstItem = false;
                sbTail.Append(edge.TailNode.Name);
            }

            return string.Format("{0} TailList: {1} HeadList: {2}", Name, sbTail, sbHead);
        }

    }
}
