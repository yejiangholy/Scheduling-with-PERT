using System;
using System.Collections.Generic;
using CPI.Graphing.GraphingEngine.Contracts.Dc;
namespace SampleSchedule.PropertyBags
{
    public class Activity : IEdge
    {
        #region Declarations

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDummy { get; set; }
        public IEdge DummyPointsToThisEdge { get; set; }
        public string TailHeadKey { get; }
        public string DependsOnDisplay { get; }
        public string DependentsDisplay { get; }
        public ICollection<DrawingProperty> DrawingPropertyList { get; set; }
        public INode HeadNode { get; set; }
        public INode TailNode { get; set; }

        public IList<IEdge> DependsOnList { get; set; }
        public IList<IEdge> DependentList { get; set; }
        public int Duration { get; set; }
        public bool Schedule { get; set; }
        public int Preference { get; set; }
        public IResource TakenBy { get; set; }
        public double Float { get; set; }
        public DateTime Est { get; set; }
        public DateTime Eft { get; set; }
        public DateTime Lst { get; set; }
        public DateTime Lft { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }

        #endregion Declarations

        public override string ToString()
        {
            if (Schedule)
            {
                return string.Format("Task {0} has been scheduled from {1} to {2} on employee {3}", Id, StartTime, FinishTime, TakenBy);
            }

            return string.Format("Task {0} has not been scheduled yet", Id);
        }

    }
}