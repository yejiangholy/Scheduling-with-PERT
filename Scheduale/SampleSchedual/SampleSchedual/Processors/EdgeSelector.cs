using SampleSchedule.PropertyBags;
using System;
using System.Collections.Generic;
using CPI.Graphing.GraphingEngine.Contracts.Dc;

namespace SampleSchedual.Processors
{
    public interface IActivitySelector
    {
        Tasks SelectNext(Dictionary<int, Tasks> ActivityList);
    }

    public class ActivitySelector : IActivitySelector
    {
        #region Declarations

        private Tasks _minFloat;

        #endregion Declarations

        public Tasks SelectNext(Dictionary<int, Tasks> ActivityList)
        {
            var scheduableList = generateScheduableList(ActivityList);

            var EarlyStartList = generateEarlyStartList(scheduableList);

            _minFloat = findMinFloat(EarlyStartList);

            return _minFloat;
        }

        private List<Tasks> generateEarlyStartList(List<Tasks> list)
        {
            var earlyStart = new List<Tasks>();
            var earliestStartTime = findMinEst(list);
            foreach(var Activity in list)
            {
                if (Activity.Est-earliestStartTime==0)
                    earlyStart.Add(Activity);
            }
            return earlyStart;
        }

        private Tasks findMinFloat(List<Tasks> list)
        {
            double min =9999.0;
            Tasks minFloat = new Tasks();
            foreach(var Activity in list)
            {
                if (Activity.Float < min)
                {
                    min = Activity.Float;
                    minFloat = Activity;
                }
            }
            return minFloat;
        }

        private List<Tasks> generateScheduableList(Dictionary<int, Tasks> ActivityList)
        {
            var schedulablelist = new List<Tasks>();
            foreach (var entry in ActivityList)
            {
                if (!hasUnstuffedDependency(entry.Value))
                    schedulablelist.Add(entry.Value);
            }
            return schedulablelist;
        }

        private bool hasUnstuffedDependency(Tasks task)
        {
            foreach (var dependency in task.DependsOnList)
            {
                var Activity = dependency as Tasks;
                if (!Activity.Schedule) return true;
            }
            return false;
        }

        private int findMinEst(List<Tasks> ActivityList)
        {
            var min = 9999;

            foreach (var Activity in ActivityList)
            {
                if (Activity.Est-min >= 0) continue;
                min = Activity.Est;
            }

            return min;
        }

    }
}

