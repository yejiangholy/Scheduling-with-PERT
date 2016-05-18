using System;

namespace SampleSchedule.PropertyBags
{
    public class NextResource
    {
        public IResource Resource { get; set; }
        public int AvailableTime { get; set; }
    }
}
