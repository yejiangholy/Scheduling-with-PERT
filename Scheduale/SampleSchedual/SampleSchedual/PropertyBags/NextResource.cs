using System;

namespace SampleSchedule.PropertyBags
{
    public class NextResource
    {
        public IResource Resource { get; set; }
        public DateTime AvailableTime { get; set; }
    }
}
