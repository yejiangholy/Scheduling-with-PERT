using System;
namespace SampleSchedule.PropertyBags
{
    public interface IResource
    {
        int Id { get; set; }
        string Name { get; set; }
    }

    public class Employee : IResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartWork { get; set; }
        public DateTime FreeTime { get; set; }

        public override string ToString()
        {
            return Name;
        }

    }
}