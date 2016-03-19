using System;
using System.Collections.Generic;

namespace SampleSchedule
{
    public class Employee
    {
        public string Name { get; set; }
        public DateTime StartWork { get; set; }
        public DateTime FreeTime { get; set; }


        public static NextResource Select(Dictionary<int,Employee> employee)
        {
            var latestStartTime = _FindLatestStartTime(employee);

            var startLateList = _FindEmployeeWithST(latestStartTime, employee);

            var nextPerson = new NextResource { Resource = startLateList[0], AvailableTime = startLateList[0].FreeTime };

            return nextPerson;
        }


        private static DateTime _FindLatestStartTime(Dictionary<int,Employee> employee)
        {
            var lateStart = employee[1].StartWork;

            foreach (var e in employee)

            {
                if (e.Value.StartWork.CompareTo(lateStart) > 0)
                { lateStart = e.Value.StartWork; }
            }
            return lateStart;
        }

        private static List<Employee> _FindEmployeeWithST(DateTime lateStart,Dictionary<int,Employee>employee)
        {
            var availablePeople = new List<Employee>();

            foreach (var e in employee)
            {
                if (e.Value.StartWork.CompareTo(lateStart) == 0)
                { availablePeople.Add(e.Value); }
            }

            return availablePeople;
        }


        public override string ToString()
        {
            return this.Name;
        }
    }
}
