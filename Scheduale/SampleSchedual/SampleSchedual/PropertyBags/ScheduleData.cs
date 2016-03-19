using System.Collections.Generic;

namespace SampleSchedule.PropertyBags
{
    public class ScheduleData
    {
        public Dictionary<int, IEdge> EdgeHash { get; set; }
        public Dictionary<int, IResource> ResourceHash { get; set; }
    }
}