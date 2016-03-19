using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleSchedule
{
    public class ScheduleData
    {
        public Dictionary<int, Task> TaskDic{ get; set; }
        public Dictionary<int, Employee> EmployeeDic { get; set; }
    }
}
