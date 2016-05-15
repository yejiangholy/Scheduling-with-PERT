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
        public int Est { get; set; }
        public int Eft { get; set; }
        public int Lst { get; set; }
        public int Lft { get; set; }
        public int StartTime { get; set; }
        public int FinishTime { get; set; }

        #endregion Declarations

        public override string ToString()
        {
            if (Schedule)
            {
                return string.Format("Task {0} has been scheduled from day {1} to day {2} on employee {3}", Id, StartTime, FinishTime, TakenBy);
            }

            return string.Format("Task {0} has not been scheduled yet", Id);
        }

    }
}