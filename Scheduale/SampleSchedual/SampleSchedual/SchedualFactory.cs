using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleSchedule
{
    public class ScheduleFactory
    {
        private  static Dictionary<int, Task> TaskHash = new Dictionary<int, Task>();
        private  static Dictionary<int, Employee> EmployeeHash = new Dictionary<int, Employee>();
        public static ScheduleData Create()
        {

            assignTaskHash();
            assignEmployeeHash();
            assignDependencies();
            ScheduleData data = new ScheduleData() { TaskDic = TaskHash, EmployeeDic = EmployeeHash };
            return data;
            
    }
        private static void assignEmployeeHash()
        {
            EmployeeHash.Add(1, new Employee
            {
                Name = "E1",
                FreeTime = new DateTime(2015, 10, 1),
                StartWork = new DateTime(2016, 10, 1),
            });

            EmployeeHash.Add(2, new Employee
            {
                Name = "E2",
                FreeTime = new DateTime(2015, 10, 1),
                StartWork = new DateTime(2016, 10, 1),
            });

            EmployeeHash.Add(3, new Employee
            {
                Name = "E3",
                FreeTime = new DateTime(2015, 10, 1),
                StartWork = new DateTime(2016, 10, 1),
            });
        }

        private  static void assignTaskHash()
        {
            TaskHash.Add(1, new Task
            {
                Id = 1,
                Duration = 3,
                DependencyList = new List<int>(),
                Est = new DateTime(2015, 10, 1),
                Eft = new DateTime(2015, 10, 4),
            });

            TaskHash.Add(2, new Task

            {
                Id = 2,
                Duration = 2,
                DependencyList = new List<int>(),
                Est = new DateTime(2015, 10, 3),
                Eft = new DateTime(2015, 10, 5),
            });

            TaskHash.Add(3, new Task
            {
                Id = 3,
                Duration = 3,
                DependencyList = new List<int>(),
                Est = new DateTime(2015, 10, 2),
                Eft = new DateTime(2015, 10, 5),
            });

            TaskHash.Add(4, new Task
            {
                Id = 4,
                Duration = 1,
                DependencyList = new List<int>(),
                Est = new DateTime(2015, 10, 8),
                Eft = new DateTime(2015, 10, 9),
            });

            TaskHash.Add(5, new Task
            {
                Id = 5,
                Duration = 3,
                DependencyList = new List<int>(),
                Est = new DateTime(2015, 10, 6),
                Eft = new DateTime(2015, 10, 9),
            });

            TaskHash.Add(6, new Task
            {
                Id = 6,
                Duration = 2,
                DependencyList = new List<int>(),
                Est = new DateTime(2015, 10, 10),
                Eft = new DateTime(2015, 10, 12),
            });

            TaskHash.Add(7, new Task
            {
                Id = 7,
                Duration = 2,
                DependencyList = new List<int>(),
                Est = new DateTime(2015, 10, 11),
                Eft = new DateTime(2015, 10, 13),
            });
        }

        public static void assignDependencies()
        {
            var t4Depen = new List<int>();
            t4Depen.Add(1); t4Depen.Add(2);
            TaskHash[4].DependencyList = t4Depen;

            var t5Depen = new List<int>();
            t5Depen.Add(3);
            TaskHash[5].DependencyList = t5Depen;
        }
    }
}
