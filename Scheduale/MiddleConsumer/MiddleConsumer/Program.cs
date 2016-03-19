using MiddleConsumer.Factory;
using SampleSchedule.Processors;
namespace MiddleConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new GraphFactory();
            var inputGraph = factory.Create();
            var avaliableResource = 1;

            var feedDataFactory = new ScheduleDataFactory();
            var feedData = feedDataFactory.Create(inputGraph, avaliableResource);

            var scheduler = new Scheduler();
            var scheduledList = scheduler.Schedule(feedData);

            var utilityFactory = new UtilityDataFactory();
            var utilityDataList = utilityFactory.Create(scheduledList);
        }
    }
}
