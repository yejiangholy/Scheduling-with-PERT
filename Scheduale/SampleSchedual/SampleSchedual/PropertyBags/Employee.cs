using System;
using System.Collections;
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
        public int StartWork { get; set; }
        public int FreeTime { get; set; }
        public ArrayList AttendenceList { get; set; }

        public override string ToString()
        {
            return Name;
        }

    }
}