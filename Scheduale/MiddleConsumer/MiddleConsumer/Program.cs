using MiddleConsumer.Factory;
using SampleSchedule.Processors;
using System;
namespace MiddleConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new GraphFactory();
            var inputGraph = factory.Create();
            Console.WriteLine("Please enter how many people you have or 0 if do not konw");
            var avaliableResource = Console.Read();

            var feedDataFactory = new ScheduleDataFactory();
            var feedData = feedDataFactory.Create(inputGraph, avaliableResource);

            var scheduler = new Scheduler();
            var scheduledList = scheduler.Schedule(feedData);

            var utilityFactory = new UtilityDataFactory();
            var utilityDataList = utilityFactory.Create(scheduledList);
        }
    }
}
