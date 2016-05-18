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
<<<<<<< HEAD
            var earlyStart = new List<Tasks>();
=======
            var earlyStart = new List<Activity>();
>>>>>>> d081ec8c9f7eb9b2a76fc65bbedd5c4c8299177c
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
<<<<<<< HEAD
            Tasks minFloat = new Tasks();
=======
            Activity minFloat = new Activity();
>>>>>>> d081ec8c9f7eb9b2a76fc65bbedd5c4c8299177c
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

<<<<<<< HEAD
        private int findMinEst(List<Tasks> ActivityList)
=======
        private int findMinEst(List<Activity> ActivityList)
>>>>>>> d081ec8c9f7eb9b2a76fc65bbedd5c4c8299177c
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

