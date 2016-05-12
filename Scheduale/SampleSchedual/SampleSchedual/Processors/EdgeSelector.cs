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
            DateTime earliestStartTime = findMinEst(list);
            foreach(var Activity in list)
            {
                if (Activity.Est.CompareTo(earliestStartTime)==0)
                    earlyStart.Add(Activity);
            }
            return earlyStart;
        }

        private Activity findMinFloat(List<Activity> list)
        {
            double min = 1000.0;
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

        private DateTime findMinEst(List<Activity> ActivityList)
        {
            var min = new DateTime(9999, 11, 11);

            foreach (var Activity in ActivityList)
            {
                if (Activity.Est.CompareTo(min) >= 0) continue;
                min = Activity.Est;
            }

            return min;
        }

    }
}

