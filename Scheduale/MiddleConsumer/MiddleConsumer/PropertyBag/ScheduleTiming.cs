using System;
namespace MiddleConsumer.Property
{
    public interface IScheduleTiming
    {
        bool isStuffed { get; set; }
        int Duration { get; set; }
        DateTime Est { get; set; }
    }

    public class ScheduleTiming : IScheduleTiming
    {
        public bool isStuffed { get; set; }
        public int Duration { get; set; }
        public DateTime Est { get; set; }
    }
}
