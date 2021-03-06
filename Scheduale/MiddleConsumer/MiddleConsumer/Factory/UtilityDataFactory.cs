﻿using System.Collections.Generic;
using CPI.Graphing.GraphingEngine.Contracts.Dc;
using SampleSchedule.PropertyBags;

namespace MiddleConsumer.Factory
{
    public interface IUtilityDataFactory
    {
        List<UtilityData> Create(List<Tasks> edgelist);
    }
    public class UtilityDataFactory : IUtilityDataFactory
    {
        #region Declarations 
        private List<UtilityData> utilityList;
        private ICollection<int> DependtList;
        #endregion Declarations 
        public  List<UtilityData> Create(List<Tasks> edgeList )
        {
            utilityList = new List<UtilityData>();

            assignUtilityData(edgeList);

            return utilityList;
        }

        private void assignUtilityData(List<Tasks> edgeList)
        {
            for (int i = 0; i < edgeList.Count; i++)
            {
                DependtList = new List<int>();
                foreach (var edge in edgeList)
                {
                    if (edge.FinishTime.CompareTo(edgeList[i].StartTime) == 0)
                    {
                        DependtList.Add(edge.Id);
                    }
                }

                utilityList.Add(new UtilityData
                {
                    Id = edgeList[i].Id,
                    Name = string.Format("Task{0}", edgeList[i].Id),
                    DependsOnList = DependtList,
                });
            }
        }
    }
}
