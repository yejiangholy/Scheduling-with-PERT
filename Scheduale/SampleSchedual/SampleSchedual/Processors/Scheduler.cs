using SampleSchedual.Processors;
using SampleSchedule.PropertyBags;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleSchedule.Processors
{
    public interface IScheduler
    {
        List<IEdge> Schedule(ScheduleData scheduleData);
    }

    public class Scheduler : IScheduler
    {
        #region Declarations

        private ResourceSelector _ResourceSelector = new ResourceSelector();
        private EdgeSelector _EdgeSelector = new EdgeSelector();
        private List<IEdge> _Response;
        private ScheduleData _ScheduleData;

        #endregion Declarations

        public List<IEdge> Schedule(ScheduleData scheduleData)
        {
            _Response = new List<IEdge>();
            _ScheduleData = scheduleData;

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
            var edgeHash = _ScheduleData.EdgeHash;
            var edgeList = new List<IEdge>();
            for(int i=1; i<=edgeHash.Count;i++)
            {
                edgeList.Add(edgeHash[i]);
            }

            edgeList[0].Eft = edgeList[0].Est.AddDays(edgeList[0].Duration);

            for(int i=1; i<edgeList.Count;i++)
            {
                foreach(IEdge predecessor in edgeList[i].DependencyList)
                {
                    if (edgeList[i].Est.CompareTo(predecessor.Eft) < 0)
                        edgeList[i].Est = predecessor.Eft;
                }
                edgeList[i].Eft = edgeList[i].Est.AddDays(edgeList[i].Duration);
            }
        }

        private void WalkingBack()
        {
            var edgeHash = _ScheduleData.EdgeHash;
            var edgeList = new List<IEdge>();
            for (int i = 1; i <= edgeHash.Count; i++)
            {
                edgeList.Add(edgeHash[i]);
            }
            var size = edgeList.Count;

            edgeList[size - 1].Lft = edgeList[size - 1].Eft;
            edgeList[size - 1].Lst = edgeList[size-1].Lft.Subtract(new TimeSpan(edgeList[size-1].Duration*24,0,0));

            for(int i = size-2;i>=0;i--)
            {
                var earlistStartTimeInSuccessor = new DateTime(9999, 12, 7);
                foreach(IEdge sucessor in edgeList[i].Successors)
                {
                    if (sucessor.Lst.CompareTo(earlistStartTimeInSuccessor) < 0)
                        earlistStartTimeInSuccessor = sucessor.Lst;
                }
                edgeList[i].Lft = earlistStartTimeInSuccessor;
                edgeList[i].Lst = edgeList[i].Lft.Subtract(new TimeSpan(edgeList[i].Duration * 24, 0, 0));
            }
        }

        private void assignFloatValue()
        {
            var edgeHash = _ScheduleData.EdgeHash;
            foreach(var key in edgeHash.Keys)
            {
                var edge = edgeHash[key];
                edge.Float = edge.Lst.Subtract(edge.Est).TotalDays;
            }

            
        }

        private void createSchedule()
        {
            while (_ScheduleData.EdgeHash.Count() > 0) scheduleEdge();
        }

        private void scheduleEdge()
        {
            var nextTask = _EdgeSelector.SelectNext(_ScheduleData.EdgeHash);
            var nextResource = _ResourceSelector.SelectNext(_ScheduleData.ResourceHash,nextTask);

            assignNextEdge(nextTask, nextResource);
            resourceSetInfo(nextTask, nextResource);

            _Response.Add(nextTask);
            _ScheduleData.EdgeHash.Remove(nextTask.Id);
        }

        private void assignNextEdge(IEdge nextEdge, NextResource nextResource)
        {
            nextEdge.TakenBy = nextResource.Resource;
            nextEdge.Schedule = true;

            nextEdge.StartTime = nextEdge.Est.CompareTo(nextResource.Resource.FreeTime) < 0
                ? nextEdge.StartTime = nextResource.Resource.FreeTime
                : nextEdge.StartTime = nextEdge.Est;

            assignFinishTime(nextEdge);
        }

        private void assignFinishTime(IEdge nextEdge)
        {
            nextEdge.FinishTime = nextEdge.StartTime + new TimeSpan(nextEdge.Duration, 0, 0, 0);
        }

        private void resourceSetInfo(IEdge nextEdge, NextResource nextResource)
        {
            nextResource.Resource.StartWork = nextEdge.StartTime;
            nextResource.Resource.FreeTime = nextEdge.FinishTime;
        }

    }
}
