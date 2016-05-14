using SampleSchedual.Processors;
using SampleSchedule.PropertyBags;
using System;
using System.Collections.Generic;
using System.Linq;
using CPI.Graphing.GraphingEngine.Contracts.Dc;

namespace SampleSchedule.Processors
{
    public interface IScheduler
    {
        List<Activity> Schedule(ScheduleData scheduleData);
    }

    public class Scheduler : IScheduler
    {
        #region Declarations

        private ResourceSelector _ResourceSelector = new ResourceSelector();
        private ActivitySelector _ActivitySelector = new ActivitySelector();
        private List<Activity> _Response;
        private ScheduleData _ScheduleData;
        private bool givenResourceNum;

        #endregion Declarations

        public List<Activity> Schedule(ScheduleData scheduleData)
        {
            _Response = new List<Activity>();
            _ScheduleData = scheduleData;
            givenResourceNum = (_ScheduleData.ResourceHash.Count != 0);

            assignFloat();

            createSchedule();

            return _Response;
        }

        private void assignFloat()
        {
            findCriticalPath();
        }

        private void findCriticalPath()
        {
            WalkingAhead();
            WalkingBack();
            assignFloatValue();
        }

        private void WalkingAhead()
        {
            var ActivityHash = _ScheduleData.ActivityHash;
            var ActivityList = new List<Activity>();
            for (int i = 0; i < ActivityHash.Count; i++)
            {
                ActivityList.Add(ActivityHash[i]);
            }

            ActivityList[0].Eft = ActivityList[0].Est.AddDays(ActivityList[0].Duration);

            for (int i = 1; i < ActivityList.Count; i++)
            {
                foreach (Activity predecessor in ActivityList[i].DependsOnList)
                {
                    if (ActivityList[i].Est.CompareTo(predecessor.Eft) < 0)
                        ActivityList[i].Est = predecessor.Eft;
                }
                ActivityList[i].Eft = ActivityList[i].Est.AddDays(ActivityList[i].Duration);
            }
        }

        private void WalkingBack()
        {
            var ActivityHash = _ScheduleData.ActivityHash;
            var ActivityList = new List<Activity>();
            for (int i = 0; i < ActivityHash.Count; i++)
            {
                ActivityList.Add(ActivityHash[i]);
            }
            var size = ActivityList.Count;

            ActivityList[size - 1].Lft = ActivityList[size - 1].Eft;
            ActivityList[size - 1].Lst = ActivityList[size - 1].Lft.Subtract(new TimeSpan(ActivityList[size - 1].Duration * 24, 0, 0));

            for (int i = size - 2; i >= 0; i--)
            {
                var earlistStartTimeInSuccessor = new DateTime(9998, 12, 7);
                foreach (Activity sucessor in ActivityList[i].DependentList)
                {
                    if (sucessor.Lst.CompareTo(earlistStartTimeInSuccessor) < 0)
                        earlistStartTimeInSuccessor = sucessor.Lst;
                }
                ActivityList[i].Lft = earlistStartTimeInSuccessor;
                ActivityList[i].Lst = ActivityList[i].Lft.Subtract(new TimeSpan(ActivityList[i].Duration * 24, 0, 0));
            }
        }

        private void assignFloatValue()
        {
            var ActivityHash = _ScheduleData.ActivityHash;
            foreach (var key in ActivityHash.Keys)
            {
                var Activity = ActivityHash[key];
                Activity.Float = Activity.Lst.Subtract(Activity.Est).TotalDays;
            }


        }

        private void createSchedule()
        {
            while (_ScheduleData.ActivityHash.Count() > 0) scheduleActivity();
        }

        private void scheduleActivity()
        {
            var nextTask = _ActivitySelector.SelectNext(_ScheduleData.ActivityHash);
            NextResource nextResource = null;
            if (givenResourceNum)
            {
                nextResource = _ResourceSelector.SelectNext(_ScheduleData.ResourceHash, nextTask);
            }
            else
            {
                nextResource = _ResourceSelector.CreateNext(_ScheduleData.ResourceHash, nextTask);
            }

            assignNextActivity(nextTask, nextResource);
            resourceSetInfo(nextTask, nextResource);

            _Response.Add(nextTask);
            _ScheduleData.ActivityHash.Remove(nextTask.Id);
        }

        private void assignNextActivity(Activity nextActivity, NextResource nextResource)
        {
            nextActivity.TakenBy = nextResource.Resource;
            nextActivity.Schedule = true;

            nextActivity.StartTime = nextActivity.Est.CompareTo(((Employee)nextResource.Resource).FreeTime) < 0
                ? nextActivity.StartTime = ((Employee)nextResource.Resource).FreeTime
                : nextActivity.StartTime = nextActivity.Est;

            assignFinishTime(nextActivity);
        }

        private void assignFinishTime(Activity nextActivity)
        {
            var duration = nextActivity.Duration;
            nextActivity.FinishTime = nextActivity.StartTime.AddDays(duration);
        }

        private void resourceSetInfo(Activity nextActivity, NextResource nextResource)
        {
            ((Employee)nextResource.Resource).StartWork = nextActivity.StartTime;
            ((Employee)nextResource.Resource).FreeTime = nextActivity.FinishTime;
        }

    }
}
