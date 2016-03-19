using System;
using System.Collections.Generic;

namespace SampleSchedule.PropertyBags
{
    public interface IEdge
    {
        int Id { get; set; }
        int Duration { get; set; }
        bool Schedule { get; set; }
        IResource TakenBy { get; set; }
        List<IEdge> DependencyList { get; set; }
        List<IEdge> Successors { get; set; }
        double Float { get; set; }
        int Preference { get; set; }
        DateTime Est { get; set; }
        DateTime Eft { get; set; }
        DateTime Lst { get; set; }
        DateTime Lft { get; set; }
        DateTime StartTime { get; set; }
        DateTime FinishTime { get; set; }
    }

    public class Edge : IEdge
    {
        #region Declarations

        public int Id { get; set; }
        public int Duration { get; set; }
        public bool Schedule { get; set; }
        public int Preference { get; set; }
        public IResource TakenBy { get; set; }
        public List<IEdge> DependencyList { get; set; }
        public double Float { get; set; }
        public List<IEdge> Successors { get; set; }
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