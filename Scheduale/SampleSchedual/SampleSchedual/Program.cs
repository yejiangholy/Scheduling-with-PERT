using SampleSchedule.Factories;
using SampleSchedule.Processors;
using SampleSchedule.PropertyBags;
using System;
using CPI.Graphing.GraphingEngine.Contracts.Dc;
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

        private static void printFloatValue(List<Tasks> ActivityList)
        {
            foreach (var Activity in ActivityList) Console.WriteLine("Activity"+Activity.Id+"has float value "+Activity.Float);
            Console.ReadLine();
        }
        private static void printSchedule(List<Tasks> ActivityList)
        {
            foreach (var Activity in ActivityList) Console.WriteLine(Activity.ToString());
            Console.ReadLine();
        }

    }
}






