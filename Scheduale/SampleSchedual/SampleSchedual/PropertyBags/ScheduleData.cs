using System.Collections.Generic;
using CPI.Graphing.GraphingEngine.Contracts.Dc;

namespace SampleSchedule.PropertyBags
{
    public class ScheduleData
    {
        public Dictionary<int, Tasks> ActivityHash { get; set; }
        public Dictionary<int, IResource> ResourceHash { get; set; }
    }
}