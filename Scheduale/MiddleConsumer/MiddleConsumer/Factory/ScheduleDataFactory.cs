using System.Collections;
using System.Collections.Generic;
using SampleSchedule.PropertyBags;
using CPI.Graphing.GraphingEngine.Contracts.Dc;
using MiddleConsumer.Property;

namespace MiddleConsumer.Factory
{
    public interface IScheduleDataFactory
    {
        ScheduleData Create(IGraph graph, int numResources);
    }

    public class ScheduleDataFactory : IScheduleDataFactory
    {
        #region Declarations

        private static Dictionary<int, Tasks> TaskHash = new Dictionary<int, Tasks>();
        private static Dictionary<int, IResource> EmployeeHash = new Dictionary<int, IResource>();

        #endregion Declarations 
        public ScheduleData Create(IGraph graph, int numResources)
        {
            assignTaskHash(graph);

            assignEmployeeHash(numResources);

            assignDependencies(graph);

            assignSuccessors(graph);

            ScheduleData data = new ScheduleData() { ActivityHash = TaskHash, ResourceHash = EmployeeHash };
            return data;
        }

        private void assignTaskHash(IGraph graph)
        {
            for (int i = 0; i <graph.EdgeList.Count; i++)
            {
                TaskHash.Add(i, new Tasks
                {
                    Id = graph.EdgeList[i].Id,
                    Duration = (graph.EdgeList[i] as Task).Timing.Duration,
                DependsOnList = new List<IEdge>(),
                    DependentList = new List<IEdge>(),
                    Est = (graph.EdgeList[i] as Task).Timing.Est,
                });
            }
        }

        private void assignEmployeeHash(int num)
        {
                for (int i = 0; i < num; i++)
                {
                EmployeeHash.Add(i, new Employee
                {
                    Id = i,
                    Name = string.Format("E{0}", i),
                    FreeTime = 0,
                    StartWork = 0,
                    AttendenceList = new ArrayList(),
                    });
                }

        }

        private void assignDependencies(IGraph graph)
        {
            for (int i = 0; i < graph.EdgeList.Count; i++)
            {
                foreach (var edge in graph.EdgeList[i].DependsOnList)
                {
                    TaskHash[i].DependsOnList.Add(TaskHash[edge.Id]);
                }
            }
        }

        private void assignSuccessors(IGraph graph)
        {
           for(int i=0; i<graph.EdgeList.Count; i++)
            {
                foreach(var edge in graph.EdgeList[i].DependentList)
                {
                    TaskHash[i].DependentList.Add(TaskHash[edge.Id]);
                }
            }
        }
    }
}
