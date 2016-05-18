using SampleSchedual.Processors;
using SampleSchedule.PropertyBags;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CPI.Graphing.GraphingEngine.Contracts.Dc;

namespace SampleSchedule.Processors
{
    public interface IScheduler
    {
        List<Tasks> Schedule(ScheduleData scheduleData);
    }

    public class Scheduler : IScheduler
    {
        #region Declarations

        private ResourceSelector _ResourceSelector = new ResourceSelector();
        private ActivitySelector _ActivitySelector = new ActivitySelector();
        private List<Tasks> _Response;
        private ScheduleData _ScheduleData;
        private bool givenResourceNum;
        private int AttendenceChartColumns;
        private int AttendenceChartRows;
        public bool[,] AttendenceChart;
        #endregion Declarations

        public List<Tasks> Schedule(ScheduleData scheduleData)
        {
            _Response = new List<Tasks>();
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
            var ActivityList = new List<Tasks>();
            for (int i = 0; i < ActivityHash.Count; i++)
            {
                ActivityList.Add(ActivityHash[i]);
            }

            ActivityList[0].Eft = ActivityList[0].Est+ ActivityList[0].Duration;

            for (int i = 1; i < ActivityList.Count; i++)
            {
                foreach (Tasks predecessor in ActivityList[i].DependsOnList)
                {
                    if (ActivityList[i].Est.CompareTo(predecessor.Eft) < 0)
                        ActivityList[i].Est = predecessor.Eft;
                }
                ActivityList[i].Eft = ActivityList[i].Est+ActivityList[i].Duration;
            }
        }

        private void WalkingBack()
        {
            var ActivityHash = _ScheduleData.ActivityHash;
            var ActivityList = new List<Tasks>();
            for (int i = 0; i < ActivityHash.Count; i++)
            {
                ActivityList.Add(ActivityHash[i]);
            }
            var size = ActivityList.Count;

            ActivityList[size - 1].Lft = ActivityList[size - 1].Eft;
            ActivityList[size - 1].Lst = ActivityList[size - 1].Lft-ActivityList[size - 1].Duration;

            for (int i = size - 2; i >= 0; i--)
            {
                var earlistStartTimeInSuccessor = 9999;
<<<<<<< HEAD
                foreach (Tasks sucessor in ActivityList[i].DependentList)
=======
                foreach (Activity sucessor in ActivityList[i].DependentList)
>>>>>>> d081ec8c9f7eb9b2a76fc65bbedd5c4c8299177c
                {
                    if (sucessor.Lst.CompareTo(earlistStartTimeInSuccessor) < 0)
                        earlistStartTimeInSuccessor = sucessor.Lst;
                }
                ActivityList[i].Lft = earlistStartTimeInSuccessor;
                ActivityList[i].Lst = ActivityList[i].Lft- ActivityList[i].Duration;
            }
        }

        private void assignFloatValue()
        {
            var ActivityHash = _ScheduleData.ActivityHash;
            foreach (var key in ActivityHash.Keys)
            {
                var Activity = ActivityHash[key];
                Activity.Float = Activity.Lst-Activity.Est;
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

        private void assignNextActivity(Tasks nextActivity, NextResource nextResource)
        {
            nextActivity.TakenBy = nextResource.Resource;
            nextActivity.Schedule = true;

            nextActivity.StartTime = nextActivity.Est.CompareTo(((Employee)nextResource.Resource).FreeTime) < 0
                ? nextActivity.StartTime = ((Employee)nextResource.Resource).FreeTime
                : nextActivity.StartTime = nextActivity.Est;

            assignFinishTime(nextActivity);
        }

        private void assignFinishTime(Tasks nextActivity)
        {
            var duration = nextActivity.Duration;
            nextActivity.FinishTime = nextActivity.StartTime+duration;
        }

        private void resourceSetInfo(Tasks nextActivity, NextResource nextResource)
        {
           
            ((Employee)nextResource.Resource).StartWork = nextActivity.StartTime;
            ((Employee)nextResource.Resource).FreeTime = nextActivity.FinishTime;

            for (int i = nextActivity.StartTime; i < nextActivity.FinishTime; i++)
            {
<<<<<<< HEAD
                ((Employee)nextResource.Resource).AttendenceList.Add(i+1);
=======
                ((Employee)nextResource.Resource).AttendenceList.Add(i);
>>>>>>> d081ec8c9f7eb9b2a76fc65bbedd5c4c8299177c
            }

            if (_ScheduleData.ActivityHash.Count() == 1)
            {
                AttendenceChartColumns = nextActivity.FinishTime;
                AttendenceChartRows = _ScheduleData.ResourceHash.Count;
                CreateAttendenceChart();
            }
        }

        private void CreateAttendenceChart()
        {
            AttendenceChart = new bool[AttendenceChartRows, AttendenceChartColumns];

            for(int j =0;j<AttendenceChartRows;j++)
            {
                for(int i=0;i<AttendenceChartColumns;i++)
                {
                    AttendenceChart[j, i] = false;
                }
            }

            for(int i=0;i<AttendenceChartRows; i++)
            {
                ArrayList attendenceList = ((Employee)_ScheduleData.ResourceHash[i]).AttendenceList;
                for (int k = 0; k < attendenceList.Count; k++)
                {
<<<<<<< HEAD
                    AttendenceChart[i, (int)attendenceList[k]-1] = true;
=======
                    AttendenceChart[i, (int)attendenceList[k]] = true;
>>>>>>> d081ec8c9f7eb9b2a76fc65bbedd5c4c8299177c
                }
            }
        }

    }
}
