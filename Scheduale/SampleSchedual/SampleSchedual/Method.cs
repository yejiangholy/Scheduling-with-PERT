using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleSchedule
{
    public class Method
    {
        public static List<Task> Schedual(ScheduleData data)
        {
            List<Task> scheduled = new List<Task>();
           
            while(data.TaskDic.Count()>0)
            {
                var nextTask = Task.Select(data.TaskDic);
                var nextPerson = Employee.Select(data.EmployeeDic);
                _taskSetInfo(nextTask,nextPerson);
                _rsourceSetInfo(nextTask,nextPerson);
                scheduled.Add(nextTask);
                data.TaskDic.Remove(nextTask.Id);
            }
            scheduled.Reverse();
            return scheduled;
            }

        private static void _taskSetInfo(Task nextTask,NextResource nextPerson)
        {
            nextTask.TakenBy = nextPerson.Resource;
            nextTask.IsStaffed = true;

            if (nextTask.Eft.CompareTo(nextPerson.Resource.StartWork) < 0) { nextTask.ST = nextTask.Est; }
            else {
                if (nextPerson.AvailableTime.CompareTo(nextTask.Est) > 0) { nextTask.ST = nextPerson.AvailableTime; }
                else { nextTask.ST = nextTask.Est; }
            }
            nextTask.FT = nextTask.ST + new TimeSpan(nextTask.Duration, 0, 0, 0);
        }

        private static void _rsourceSetInfo(Task nextTask, NextResource nextPerson)
        {
            nextPerson.Resource.StartWork = nextTask.ST;
            nextPerson.Resource.FreeTime = nextTask.FT;
        }

        public static void Detail(List<Task>scheduled)
        {
            foreach(Task task in scheduled)
            {
                Console.WriteLine(task.ToString());
            }
            Console.ReadLine();
        }
      
        
        
        }
}
