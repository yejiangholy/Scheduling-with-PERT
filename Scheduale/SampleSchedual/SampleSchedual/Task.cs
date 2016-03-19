using System;
using System.Collections.Generic;

namespace SampleSchedule
{
    public class Task
    {
       public int Id {get; set;}
        public int Duration { get; set; }
        public bool IsStaffed { get; set; }
        public Employee TakenBy { get; set; }
        public List<int> DependencyList { get; set; }
        public DateTime Est { get; set; }
        public DateTime Eft { get; set; }
        public DateTime Lst { get; set; }
        public DateTime Lft { get; set; }
        public DateTime ST { get; set; }
        public DateTime FT { get; set; }


        public static Task Select(Dictionary<int, Task> tasks)
        {
           
            var maxEst = _FindMaxEst(tasks);

            var taskList = _FindTaskSameEst(maxEst, tasks);

            return taskList[0];
        }


           
        private static DateTime _FindMaxEst(Dictionary<int,Task> tasks)
        {
            
            DateTime max = tasks[1].Est;
            foreach (var entry in tasks)
            {
                if (entry.Value.Est.CompareTo(max) > 0)
                { max = entry.Value.Est; }

            }
            return max;
        }

        private static List<Task> _FindTaskSameEst(DateTime date,Dictionary<int,Task>tasks)
        {
            var maxEST = new List<Task>();
            foreach (var entry in tasks)
            {
                if (entry.Value.Est.CompareTo(date) == 0)
                { maxEST.Add(entry.Value); }
            }
            return maxEST;
        }

        public override string ToString()
        {
            if (this.IsStaffed)
                return string.Format("Task {0} has been schedualed from {1} to {2} on employee {3}", this.Id, this.ST, this.FT, this.TakenBy);
            else
                return string.Format("Task {0} has not been schedualed yet", this.Id);
        }

    }
}
