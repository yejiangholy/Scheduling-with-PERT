using SampleSchedule.PropertyBags;
using System;
using System.Collections.Generic;

namespace SampleSchedual.Processors
{
    public interface IEdgeSelector
    {
        IEdge SelectNext(Dictionary<int, IEdge> edgeList);
    }

    public class EdgeSelector : IEdgeSelector
    {
        #region Declarations

        private IEdge _minFloat;

        #endregion Declarations

        public IEdge SelectNext(Dictionary<int, IEdge> edgeList)
        {
            var scheduableList = generateScheduableList(edgeList);

            var EarlyStartList = generateEarlyStartList(scheduableList);

            _minFloat = findMinFloat(EarlyStartList);

            return _minFloat;
        }

        private List<IEdge> generateEarlyStartList(List<IEdge> list)
        {
            var earlyStart = new List<IEdge>();
            DateTime earliestStartTime = findMinEst(list);
            foreach(var edge in list)
            {
                if (edge.Est.CompareTo(earliestStartTime)==0)
                    earlyStart.Add(edge);
            }
            return earlyStart;
        }

        private IEdge findMinFloat(List<IEdge> list)
        {
            double min = 1000.0;
            IEdge minFloat = new Edge();
            foreach(var edge in list)
            {
                if (edge.Float < min)
                {
                    min = edge.Float;
                    minFloat = edge;
                }
            }
            return minFloat;
        }

        private List<IEdge> generateScheduableList(Dictionary<int, IEdge> edgeList)
        {
            var schedulablelist = new List<IEdge>();
            foreach (var entry in edgeList)
            {
                if (!hasUnstuffedDependency(entry.Value))
                    schedulablelist.Add(entry.Value);
            }
            return schedulablelist;
        }

        private bool hasUnstuffedDependency(IEdge task)
        {
            foreach (var dependency in task.DependencyList)
            {
                if (!dependency.Schedule) return true;
            }
            return false;
        }

        private DateTime findMinEst(List<IEdge> edgeList)
        {
            var min = new DateTime(9999, 11, 11);

            foreach (var edge in edgeList)
            {
                if (edge.Est.CompareTo(min) >= 0) continue;
                min = edge.Est;
            }

            return min;
        }

    }
}

