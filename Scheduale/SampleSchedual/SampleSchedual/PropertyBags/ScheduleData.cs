using System.Collections.Generic;
using CPI.Graphing.GraphingEngine.Contracts.Dc;

namespace SampleSchedule.PropertyBags
{
    public class ScheduleData
    {
        public Dictionary<int, Activity> ActivityHash { get; set; }
        public Dictionary<int, IResource> ResourceHash { get; set; }
    }
}