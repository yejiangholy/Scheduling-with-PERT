using System.Collections.Generic;
using System.Text;
using CPI.Graphing.GraphingEngine.Contracts.Dc;

namespace MiddleConsumer.Property
{
    public class Edge : IEdge
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ScheduleTiming Timing { get; set; }

        public IList<IEdge> DependsOnList { get; set; }

        public IList<IEdge> DependentList { get; set; }

        public IEdge DummyPointsToThisEdge { get; set; }

        public INode HeadNode { get; set; }

        public INode TailNode { get; set; }

        public bool IsDummy { get; set; }

        public ICollection<DrawingProperty> DrawingPropertyList { get; set; }

        public string DependsOnDisplay
        {
            get
            {
                var sb = new StringBuilder();

                var firstItem = true;
                foreach (var edge in DependentList)
                {
                    if (!firstItem) sb.Append(",");
                    firstItem = false;
                    sb.Append(edge.Id);
                }

                return sb.ToString();
            }
        }

        public string DependentsDisplay
        {
            get
            {
                var sb = new StringBuilder();

                var firstItem = true;
                foreach (var edge in DependentList)
                {
                    if (!firstItem) sb.Append(",");
                    firstItem = false;
                    sb.Append(edge.Id);
                }

                return sb.ToString();
            }
        }

        public Edge()
        {
            DependsOnList = new List<IEdge>();
            DependentList = new List<IEdge>();
            Timing = new ScheduleTiming();
        }

        public string TailHeadKey
        {
            get
            {
                return string.Format("T{0}:{1}",
                    TailNode == null ? string.Empty : TailNode.Id.ToString(),
                    HeadNode == null ? string.Empty : HeadNode.Id.ToString());
            }
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Id, Name);
        }
    }
}
