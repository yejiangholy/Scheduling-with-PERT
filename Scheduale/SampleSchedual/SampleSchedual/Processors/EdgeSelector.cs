using SampleSchedule.PropertyBags;
using System;
using System.Collections.Generic;
using CPI.Graphing.GraphingEngine.Contracts.Dc;

namespace SampleSchedual.Processors
{
    public interface IActivitySelector
    {
        Activity SelectNext(Dictionary<int, Activity> ActivityList);
    }

    public class ActivitySelector : IActivitySelector
    {
        #region Declarations

        private Activity _minFloat;

        #endregion Declarations

        public Activity SelectNext(Dictionary<int, Activity> ActivityList)
        {
            var scheduableList = generateScheduableList(ActivityList);

            var EarlyStartList = generateEarlyStartList(scheduableList);

            _minFloat = findMinFloat(EarlyStartList);

            return _minFloat;
        }

        private List<Activity> generateEarlyStartList(List<Activity> list)
        {
            var earlyStart = new List<Activity>();
            var earliestStartTime = findMinEst(list);
            foreach(var Activity in list)
            {
                if (Activity.Est-earliestStartTime==0)
                    earlyStart.Add(Activity);
            }
            return earlyStart;
        }

        private Activity findMinFloat(List<Activity> list)
        {
            double min =9999.0;
            Activity minFloat = new Activity();
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

        private List<Activity> generateScheduableList(Dictionary<int, Activity> ActivityList)
        {
            var schedulablelist = new List<Activity>();
            foreach (var entry in ActivityList)
            {
                if (!hasUnstuffedDependency(entry.Value))
                    schedulablelist.Add(entry.Value);
            }
            return schedulablelist;
        }

        private bool hasUnstuffedDependency(Activity task)
        {
            foreach (var dependency in task.DependsOnList)
            {
                var Activity = dependency as Activity;
                if (!Activity.Schedule) return true;
            }
            return false;
        }

        private int findMinEst(List<Activity> ActivityList)
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

