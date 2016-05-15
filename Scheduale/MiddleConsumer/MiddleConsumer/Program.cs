using MiddleConsumer.Factory;
using SampleSchedule.Processors;
using System;
using SampleSchedule.PropertyBags;
using System.Collections.Generic;
namespace MiddleConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new GraphFactory();
            var inputGraph = factory.Create();
            Console.WriteLine("Please enter how many people you have or 0 if do not konw");
            var avaliableResource = int.Parse(Console.ReadLine());
            Console.WriteLine("Scheudling with " + avaliableResource + " resources");

            var feedDataFactory = new ScheduleDataFactory();
            var feedData = feedDataFactory.Create(inputGraph, avaliableResource);

            var scheduler = new Scheduler();
            var scheduledList = scheduler.Schedule(feedData);
            var attendenceChart = scheduler.AttendenceChart;

            printSchedule(scheduledList);
            Console.WriteLine();
            printAttendence(attendenceChart);

            var utilityFactory = new UtilityDataFactory();
            var utilityDataList = utilityFactory.Create(scheduledList);
        }

        private static void printSchedule(List<Activity> ActivityList)
        {
            foreach (var Activity in ActivityList) Console.WriteLine(Activity.ToString());
            Console.ReadLine();
        }

        private static void printAttendence(bool[,] chart)
        {
            for(int j=0;j<chart.GetLength(0);j++)
            {
                for(int i=0;i<chart.GetLength(1);i++)
                {
                    Console.Write(chart[j, i]+ " ");
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
