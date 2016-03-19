using SampleSchedule.Factories;
using SampleSchedule.Processors;
using SampleSchedule.PropertyBags;
using System;
using System.Collections.Generic;

namespace SampleSchedule
{
    public class Program
    {
        static void Main(string[] args)
        {
            var scheduleData = new ScheduleFactory().Create();
            var scheduler = new Scheduler();
            var scheduledList = scheduler.Schedule(scheduleData);
            printSchedule(scheduledList);
        }

        private static void printFloatValue(List<IEdge> edgeList)
        {
            foreach (var edge in edgeList) Console.WriteLine("edge"+edge.Id+"has float value "+edge.Float);
            Console.ReadLine();
        }
        private static void printSchedule(List<IEdge> edgeList)
        {
            foreach (var edge in edgeList) Console.WriteLine(edge.ToString());
            Console.ReadLine();
        }

    }
}






