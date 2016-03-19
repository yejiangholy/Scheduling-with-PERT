using System;
using System.Collections.Generic;
using SampleSchedule.PropertyBags;

namespace MiddleConsumer.Factory
{
    public interface IScheduleDataFactory
    {
        ScheduleData Create(MiddleConsumer.Property.IGraph graph, int numResources);
    }

    public class ScheduleDataFactory : IScheduleDataFactory
    {
        #region Declarations

        private static Dictionary<int, IEdge> TaskHash = new Dictionary<int, IEdge>();
        private static Dictionary<int, IResource> EmployeeHash = new Dictionary<int, IResource>();

        #endregion Declarations 
        public ScheduleData Create(MiddleConsumer.Property.IGraph graph, int numResources)
        {
            assignTaskHash(graph);

            assignEmployeeHash(numResources);

            assignDependencies(graph);

            ScheduleData data = new ScheduleData() { EdgeHash = TaskHash, ResourceHash = EmployeeHash };
            return data;
        }

        private void assignTaskHash(MiddleConsumer.Property.IGraph graph)
        {
            for (int i = 0; i < graph.EdgeList.Count; i++)
            {
                TaskHash.Add(i, new Edge
                {
                    Id = graph.EdgeList[i].Id,
                    Duration = graph.EdgeList[i].Timing.Duration,
                    DependencyList = new List<IEdge>(),
                    Est = graph.EdgeList[i].Timing.Est,
                });
            }
        }

        private void assignEmployeeHash(int num)
        {
            for (int i = 0; i < num; i++)
            {
                EmployeeHash.Add(i, new Employee
                {
                    Name = string.Format("E{0}", i),
                    FreeTime = new DateTime(2015, 10, 1),
                    StartWork = new DateTime(2014, 10, 1),
                });
            }
        }

        private void assignDependencies(MiddleConsumer.Property.IGraph graph)
        {
            for (int i = 0; i < graph.EdgeList.Count; i++)
            {
                foreach (var edge in graph.EdgeList[i].DependsOnList)
                {
                    TaskHash[i].DependencyList.Add(TaskHash[edge.Id]);
                }
            }
        }
    }
}
