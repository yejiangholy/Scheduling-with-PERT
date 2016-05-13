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

        private static Dictionary<int, Activity> TaskHash = new Dictionary<int, Activity>();
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
                FreeTime = new DateTime(2015, 10, 1),
                StartWork = new DateTime(2014, 10, 1),
            });

            EmployeeHash.Add(2, new Employee
            {
                Name = "E2",
                FreeTime = new DateTime(2015, 10, 1),
                StartWork = new DateTime(2014, 10, 1),
            });
        }
           

        private static void assignTaskHash()
        {
            TaskHash.Add(1, new Activity
            {
                Id = 1,
                DependsOnList = new List<IEdge>(),
                Duration = 3,
                Est = new DateTime(2015, 10, 3),
            });

            TaskHash.Add(2, new Activity
            {
                Id = 2,
                DependsOnList = new List<IEdge>(),
                Duration = 10,
                Est = new DateTime(2015, 10, 5),
            });
            TaskHash.Add(3, new Activity
            {
                Id = 3,
                DependsOnList = new List<IEdge>(),
                Duration = 3,
                Est = new DateTime(2015, 10, 2),
            });
            TaskHash.Add(4, new Activity
            {
                Id = 4,
                DependsOnList = new List<IEdge>(),
                Duration = 4,
                Est = new DateTime(2015, 10, 4),
            });
            TaskHash.Add(5, new Activity
            {
                Id = 5,
                DependsOnList = new List<IEdge>(),
                Duration = 3,
                Est = new DateTime(2015, 10, 3),
            });
            TaskHash.Add(6, new Activity
            {
                Id = 6,
                DependsOnList = new List<IEdge>(),
                Duration = 4,
                Est = new DateTime(2015, 10, 5),
            });
            TaskHash.Add(7, new Activity
            {
                Id = 7,
                DependsOnList = new List<IEdge>(),
                Duration = 7,
                Est = new DateTime(2015, 10, 5),
            });
            TaskHash.Add(8, new Activity
            {
                Id = 8,
                DependsOnList = new List<IEdge>(),
                Duration = 1,
                Est = new DateTime(2015, 10, 5),
            });
            TaskHash.Add(9, new Activity
            {
                Id = 9,
                DependsOnList = new List<IEdge>(),
                Duration = 2,
                Est = new DateTime(2015, 10, 10),
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