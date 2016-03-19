using System;
namespace SampleSchedule.PropertyBags
{
    public interface IResource
    {
        string Name { get; set; }
        DateTime StartWork { get; set; }
        DateTime FreeTime { get; set; }
    }

    public class Employee : IResource
    {
        public string Name { get; set; }
        public DateTime StartWork { get; set; }
        public DateTime FreeTime { get; set; }

        public override string ToString()
        {
            return Name;
        }

    }
}