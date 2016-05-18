using System;
using System.Collections.Generic;
using SampleSchedule.PropertyBags;
using CPI.Graphing.GraphingEngine.Contracts.Dc;

namespace SampleSchedule.Factories
{
    public interface IFactory
    {
        ScheduleData Create();
    }

    public class ScheduleFactory : IFactory
    {
        #region Declarations

        private static Dictionary<int, Tasks> TaskHash = new Dictionary<int, Tasks>();
        private static Dictionary<int, IResource> EmployeeHash = new Dictionary<int, IResource>();

        #endregion Declarations

        public ScheduleData Create()
        {
            assignTaskHash();
            assignEmployeeHash();
            assignDependencies();
            assignSuccessors();

            ScheduleData data = new ScheduleData() { ActivityHash = TaskHash, ResourceHash = EmployeeHash };

            return data;
        }

        private static void assignEmployeeHash()
        {
            EmployeeHash.Add(1, new Employee
            {
                Name = "E1",
                FreeTime = 0,
                StartWork = 0,
            });

            EmployeeHash.Add(2, new Employee
            {
                Name = "E2",
                FreeTime = 0,
                StartWork = 0,
            });
        }
           

        private static void assignTaskHash()
        {
            TaskHash.Add(1, new Tasks
            {
                Id = 1,
                DependsOnList = new List<IEdge>(),
                Duration = 3,
                Est = 0,
            });

            TaskHash.Add(2, new Tasks
            {
                Id = 2,
                DependsOnList = new List<IEdge>(),
                Duration = 10,
                Est =2,
            });
            TaskHash.Add(3, new Tasks
            {
                Id = 3,
                DependsOnList = new List<IEdge>(),
                Duration = 3,
                Est = 2,
            });
            TaskHash.Add(4, new Tasks
            {
                Id = 4,
                DependsOnList = new List<IEdge>(),
                Duration = 4,
                Est = 4,
            });
            TaskHash.Add(5, new Tasks
            {
                Id = 5,
                DependsOnList = new List<IEdge>(),
                Duration = 3,
                Est = 2,
            });
            TaskHash.Add(6, new Tasks
            {
                Id = 6,
                DependsOnList = new List<IEdge>(),
                Duration = 4,
                Est = 5,
            });
            TaskHash.Add(7, new Tasks
            {
                Id = 7,
                DependsOnList = new List<IEdge>(),
                Duration = 7,
                Est = 5,
            });
            TaskHash.Add(8, new Tasks
            {
                Id = 8,
                DependsOnList = new List<IEdge>(),
                Duration = 1,
                Est = 5,
            });
            TaskHash.Add(9, new Tasks
            {
                Id = 9,
                DependsOnList = new List<IEdge>(),
                Duration = 2,
                Est = 5,
            });

        }

        public static void assignDependencies()
        {
            var t2Depen = new List<IEdge>();
            t2Depen.Add(TaskHash[1]);
            TaskHash[2].DependsOnList = t2Depen;

            var t3Depen = new List<IEdge>();
            t3Depen.Add(TaskHash[1]);
            TaskHash[3].DependsOnList = t3Depen;

            var t4Depen = new List<IEdge>();
            t4Depen.Add(TaskHash[1]);
            TaskHash[4].DependsOnList = t4Depen;

            var t5Depen = new List<IEdge>();
            t5Depen.Add(TaskHash[3]);
            TaskHash[5].DependsOnList = t5Depen;

            var t6Depen = new List<IEdge>();
            t6Depen.Add(TaskHash[5]); t6Depen.Add(TaskHash[4]);
            TaskHash[6].DependsOnList = t6Depen;

            var t7Depen = new List<IEdge>();
            t7Depen.Add(TaskHash[2]);
            TaskHash[7].DependsOnList = t7Depen;

            var t8Depen = new List<IEdge>();
            t8Depen.Add(TaskHash[6]);
            TaskHash[8].DependsOnList = t8Depen;

            var t9Depen = new List<IEdge>();
            t9Depen.Add(TaskHash[7]); t9Depen.Add(TaskHash[8]);
            TaskHash[9].DependsOnList = t9Depen;
        }
        public static void assignSuccessors()
        {
            var t1Suc = new List<IEdge>();
            t1Suc.Add(TaskHash[2]); t1Suc.Add(TaskHash[3]); t1Suc.Add(TaskHash[4]);
            TaskHash[1].DependentList = t1Suc;

            var t2Suc = new List<IEdge>();
            t2Suc.Add(TaskHash[7]);
            TaskHash[2].DependentList = t2Suc;

            var t3Suc = new List<IEdge>();
            t3Suc.Add(TaskHash[5]);
            TaskHash[3].DependentList = t3Suc;

            var t4Suc = new List<IEdge>();
            t4Suc.Add(TaskHash[6]);
            TaskHash[4].DependentList = t4Suc;
            TaskHash[5].DependentList = t4Suc;

            var t6Suc = new List<IEdge>();
            t6Suc.Add(TaskHash[8]);
            TaskHash[6].DependentList = t6Suc;

            var t7Suc = new List<IEdge>();
            t7Suc.Add(TaskHash[9]);
            TaskHash[7].DependentList = t7Suc;
            TaskHash[8].DependentList = t7Suc;
        }

    }
}