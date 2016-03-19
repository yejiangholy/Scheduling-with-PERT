using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SampleSchedule.PropertyBags;
using SampleSchedule.Processors;

namespace SchedulerTest
{
    [TestClass]
    public class ScheduleTest
    {

        [TestMethod]
        public void EdgeSelector_CriticalPath_LinearTasks()
        {
            Dictionary<int, IEdge> Task = new Dictionary<int, IEdge>();
            Dictionary<int, IResource> Resource = new Dictionary<int, IResource>();
            Task.Add(1, new Edge
            {
                Id = 1,
                DependencyList = new List<IEdge>(),
                Duration = 3,
                Est = new DateTime(2015, 10, 3),
            });

            Task.Add(2, new Edge
            {
                Id = 2,
                DependencyList = new List<IEdge>(),
                Duration = 10,
                Est = new DateTime(2015, 10, 5),
            });
            Task.Add(3, new Edge
            {
                Id = 3,
                DependencyList = new List<IEdge>(),
                Duration = 3,
                Est = new DateTime(2015, 10, 2),
            });
            Task.Add(4, new Edge
            {
                Id = 4,
                DependencyList = new List<IEdge>(),
                Duration = 4,
                Est = new DateTime(2015, 10, 4),
            });
            Task.Add(5, new Edge
            {
                Id = 5,
                DependencyList = new List<IEdge>(),
                Duration = 3,
                Est = new DateTime(2015, 10, 3),
            });

            Resource.Add(1, new Employee
            {
                Name = "E1",
                FreeTime = new DateTime(2015, 10, 1),
                StartWork = new DateTime(2015, 9, 1),
            });

            Resource.Add(2, new Employee
            {
                Name = "E2",
                FreeTime = new DateTime(2015, 10, 1),
                StartWork = new DateTime(2015, 9, 1),
            });

            // assing dependency 
            var t2Depen = new List<IEdge>();
            t2Depen.Add(Task[1]);
            Task[2].DependencyList = t2Depen;

            var t3Depen = new List<IEdge>();
            t3Depen.Add(Task[2]);
            Task[3].DependencyList = t3Depen;

            var t4Depen = new List<IEdge>();
            t4Depen.Add(Task[3]);
            Task[4].DependencyList = t4Depen;

            var t5Depen = new List<IEdge>();
            t5Depen.Add(Task[4]);
            Task[5].DependencyList = t5Depen;

            //assign sucessor
            var t1Suc = new List<IEdge>();
            t1Suc.Add(Task[2]);
            Task[1].Successors = t1Suc;

            var t2Suc = new List<IEdge>();
            t2Suc.Add(Task[3]);
            Task[2].Successors = t2Suc;

            var t3Suc = new List<IEdge>();
            t3Suc.Add(Task[4]);
            Task[3].Successors = t3Suc;

            var t4Suc = new List<IEdge>();
            t4Suc.Add(Task[5]);
            Task[4].Successors = t4Suc;

            //create data 
            ScheduleData data = new ScheduleData()
            {
                EdgeHash = Task,
                ResourceHash = Resource
            };

            //using 
            var scheduledList = new Scheduler().Schedule(data);
            //test 
            Assert.AreEqual(1, scheduledList[0].Id);
            Assert.AreEqual(2, scheduledList[1].Id);
            Assert.AreEqual(3, scheduledList[2].Id);
            Assert.AreEqual(4, scheduledList[3].Id);
            Assert.AreEqual(5, scheduledList[4].Id);
        }

        [TestMethod]
        public void EdgeSelector_CriticalPath_PreferLowerFloat1()
        {
            Dictionary<int, IEdge> Task = new Dictionary<int, IEdge>();
            Dictionary<int, IResource> Resource = new Dictionary<int, IResource>();
            Task.Add(1, new Edge
            {
                Id = 1,
                DependencyList = new List<IEdge>(),
                Duration = 3,
                Est = new DateTime(2015, 10, 3),
            });

            Task.Add(2, new Edge
            {
                Id = 2,
                DependencyList = new List<IEdge>(),
                Duration = 2,
                Est = new DateTime(2015, 10, 5),
            });
            Task.Add(3, new Edge
            {
                Id = 3,
                DependencyList = new List<IEdge>(),
                Duration = 5,
                Est = new DateTime(2015, 10, 2),
            });
            Task.Add(4, new Edge
            {
                Id = 4,
                DependencyList = new List<IEdge>(),
                Duration = 3,
                Est = new DateTime(2015, 10, 2),
            });



            Resource.Add(1, new Employee
            {
                Name = "E1",
                FreeTime = new DateTime(2015, 10, 1),
                StartWork = new DateTime(2015, 9, 1),
            });


            // assing dependency 
            var t2Depen = new List<IEdge>();
            t2Depen.Add(Task[1]);
            Task[2].DependencyList = t2Depen;

            var t3Depen = new List<IEdge>();
            t3Depen.Add(Task[1]);
            Task[3].DependencyList = t3Depen;

            var t4Depen = new List<IEdge>();
            t4Depen.Add(Task[3]); t4Depen.Add(Task[2]);
            Task[4].DependencyList = t4Depen;


            //assign sucessor
            var t1Suc = new List<IEdge>();
            t1Suc.Add(Task[2]); t1Suc.Add(Task[3]);
            Task[1].Successors = t1Suc;

            var t2Suc = new List<IEdge>();
            t2Suc.Add(Task[4]);
            Task[2].Successors = t2Suc;
            Task[3].Successors = t2Suc;

            //create data 
            ScheduleData data = new ScheduleData()
            {
                EdgeHash = Task,
                ResourceHash = Resource
            };

            //using 
            var scheduledList = new Scheduler().Schedule(data);
            //test 
            Assert.AreEqual(1, scheduledList[0].Id);
            Assert.AreEqual(3, scheduledList[1].Id);
            Assert.AreEqual(2, scheduledList[2].Id);
            Assert.AreEqual(4, scheduledList[3].Id);
        }

        [TestMethod]
        public void EdgeSelector_CriticalPath_PreferLowerFloat2()
        {
            Dictionary<int, IEdge> Task = new Dictionary<int, IEdge>();
            Dictionary<int, IResource> Resource = new Dictionary<int, IResource>();
            Task.Add(1, new Edge
            {
                Id = 1,
                DependencyList = new List<IEdge>(),
                Duration = 3,
                Est = new DateTime(2015, 10, 3),
            });

            Task.Add(2, new Edge
            {
                Id = 2,
                DependencyList = new List<IEdge>(),
                Duration = 6,
                Est = new DateTime(2015, 10, 5),
            });
            Task.Add(3, new Edge
            {
                Id = 3,
                DependencyList = new List<IEdge>(),
                Duration = 3,
                Est = new DateTime(2015, 10, 2),
            });
            Task.Add(4, new Edge
            {
                Id = 4,
                DependencyList = new List<IEdge>(),
                Duration = 9,
                Est = new DateTime(2015, 10, 2),
            });
            Task.Add(5, new Edge
            {
                Id = 5,
                DependencyList = new List<IEdge>(),
                Duration = 3,
                Est = new DateTime(2015, 10, 2),
            });



            Resource.Add(1, new Employee
            {
                Name = "E1",
                FreeTime = new DateTime(2015, 10, 1),
                StartWork = new DateTime(2015, 9, 1),
            });
            // assing dependency 
            var t2Depen = new List<IEdge>();
            t2Depen.Add(Task[1]);
            Task[2].DependencyList = t2Depen;
            Task[3].DependencyList = t2Depen;
            Task[4].DependencyList = t2Depen;

            var t5Depen = new List<IEdge>();
            t5Depen.Add(Task[3]); t5Depen.Add(Task[2]); t5Depen.Add(Task[4]);
            Task[5].DependencyList = t5Depen;


            //assign sucessor
            var t1Suc = new List<IEdge>();
            t1Suc.Add(Task[2]); t1Suc.Add(Task[3]); t1Suc.Add(Task[4]);
            Task[1].Successors = t1Suc;

            var t2Suc = new List<IEdge>();
            t2Suc.Add(Task[5]);
            Task[2].Successors = t2Suc;
            Task[3].Successors = t2Suc;
            Task[4].Successors = t2Suc;


            //create data 
            ScheduleData data = new ScheduleData()
            {
                EdgeHash = Task,
                ResourceHash = Resource
            };

            //using 
            var scheduledList = new Scheduler().Schedule(data);
            //test 
            Assert.AreEqual(1, scheduledList[0].Id);
            Assert.AreEqual(4, scheduledList[1].Id);
            Assert.AreEqual(2, scheduledList[2].Id);
            Assert.AreEqual(3, scheduledList[3].Id);
            Assert.AreEqual(5, scheduledList[4].Id);
        }

        [TestMethod]
        public void EdgeSelector_CriticalPath_NotJustWaitingLowerFloat()
        {
            Dictionary<int, IEdge> Task = new Dictionary<int, IEdge>();
            Dictionary<int, IResource> Resource = new Dictionary<int, IResource>();
            Task.Add(1, new Edge
            {
                Id = 1,
                DependencyList = new List<IEdge>(),
                Duration=3,
                Est = new DateTime(2015, 10, 3),
            });

            Task.Add(2, new Edge
            {
                Id = 2,
                DependencyList = new List<IEdge>(),
                Duration=10,
                Est = new DateTime(2015, 10, 5),
            });
            Task.Add(3, new Edge
            {
                Id = 3,
                DependencyList = new List<IEdge>(),
                Duration=3,
                Est = new DateTime(2015, 10, 2),
            });
            Task.Add(4, new Edge
            {
                Id = 4,
                DependencyList = new List<IEdge>(),
                Duration = 4,
                Est = new DateTime(2015, 10, 4),
            });
            Task.Add(5, new Edge
            {
                Id = 5,
                DependencyList = new List<IEdge>(),
                Duration=3,
                Est = new DateTime(2015, 10, 3),
            });
            Task.Add(6, new Edge
            {
                Id = 6,
                DependencyList = new List<IEdge>(),
                Duration = 4,
                Est = new DateTime(2015, 10, 5),
            });
            Task.Add(7, new Edge
            {
                Id = 7,
                DependencyList = new List<IEdge>(),
                Duration = 7,
                Est = new DateTime(2015, 10, 5),
            });
            Task.Add(8, new Edge
            {
                Id = 8,
                DependencyList = new List<IEdge>(),
                Duration = 1,
                Est = new DateTime(2015, 10, 5),
            });
            Task.Add(9, new Edge
            {
                Id = 9,
                DependencyList = new List<IEdge>(),
                Duration = 2,
                Est = new DateTime(2015, 10, 10),
            });

            Resource.Add(1, new Employee
            {
                Name = "E1",
                FreeTime = new DateTime(2015, 10, 1),
                StartWork = new DateTime(2015, 9, 1),
            });

            Resource.Add(2, new Employee
            {
                Name = "E2",
                FreeTime = new DateTime(2015, 10, 1),
                StartWork = new DateTime(2015, 9, 1),
            });

            // assing dependency 
            var t2Depen = new List<IEdge>();
            t2Depen.Add(Task[1]);
            Task[2].DependencyList = t2Depen;

            var t3Depen = new List<IEdge>();
            t3Depen.Add(Task[1]);
            Task[3].DependencyList = t3Depen;

            var t4Depen = new List<IEdge>();
            t4Depen.Add(Task[1]);
            Task[4].DependencyList = t4Depen;

            var t5Depen = new List<IEdge>();
            t5Depen.Add(Task[3]);
            Task[5].DependencyList = t5Depen;

            var t6Depen = new List<IEdge>();
            t6Depen.Add(Task[5]); t6Depen.Add(Task[4]);
            Task[6].DependencyList = t6Depen;

            var t7Depen = new List<IEdge>();
            t7Depen.Add(Task[2]);
            Task[7].DependencyList = t7Depen;

            var t8Depen = new List<IEdge>();
            t8Depen.Add(Task[6]);
            Task[8].DependencyList = t8Depen;

            var t9Depen = new List<IEdge>();
            t9Depen.Add(Task[7]); t9Depen.Add(Task[8]);
            Task[9].DependencyList = t9Depen;

            //assign sucessor
            var t1Suc = new List<IEdge>();
            t1Suc.Add(Task[2]); t1Suc.Add(Task[3]); t1Suc.Add(Task[4]);
            Task[1].Successors = t1Suc;

            var t2Suc = new List<IEdge>();
            t2Suc.Add(Task[7]);
            Task[2].Successors = t2Suc;

            var t3Suc = new List<IEdge>();
            t3Suc.Add(Task[5]);
            Task[3].Successors = t3Suc;

            var t4Suc = new List<IEdge>();
            t4Suc.Add(Task[6]);
            Task[4].Successors = t4Suc;
            Task[5].Successors = t4Suc;

            var t6Suc = new List<IEdge>();
            t6Suc.Add(Task[8]);
            Task[6].Successors = t6Suc;

            var t7Suc = new List<IEdge>();
            t7Suc.Add(Task[9]);
            Task[7].Successors = t7Suc;
            Task[8].Successors = t7Suc;

            //create data 
            ScheduleData data = new ScheduleData()
            {
                EdgeHash = Task,
                ResourceHash = Resource
            };

            //using 
            var scheduledList = new Scheduler().Schedule(data);
            //test 
            Assert.AreEqual(1, scheduledList[0].Id);
            Assert.AreEqual(2, scheduledList[1].Id);
            Assert.AreEqual(3, scheduledList[2].Id);
            Assert.AreEqual(4, scheduledList[3].Id);
            Assert.AreEqual(5, scheduledList[4].Id);
            Assert.AreEqual(6, scheduledList[5].Id);
            Assert.AreEqual(7, scheduledList[6].Id);
            Assert.AreEqual(8, scheduledList[7].Id);
            Assert.AreEqual(9, scheduledList[8].Id);
        }
    }
}