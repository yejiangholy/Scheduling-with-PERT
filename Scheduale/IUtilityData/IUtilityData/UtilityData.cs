using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;

namespace CPI.Graphing.GraphingEngine.Contracts.Dc
{
    public interface IUtilityData
    {
        int Id { get; set; }
        string Name { get; set; }
        string DependsOn { get; set; }
        string ExportString { get; }
        ICollection<int> DependsOnList { get; set; }
        ICollection<IUtilityData> DependentList { get; set; }
    }

    public class UtilityData : IUtilityData
    {
       public int Id { get; set; } 
       public string Name { get; set; }
        public ICollection<int> DependsOnList { get; set; }

        public ICollection<IUtilityData> DependentList { get; set; }

        public UtilityData()
        {
            DependsOnList = new Collection<int>();
            DependentList = new Collection<IUtilityData>();
        }

        public string ExportString
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append(Id);
                sb.Append(",");
                sb.Append(Name);
                sb.Append(",");
                //sb.Append(Duration);
                //sb.Append(",");
                foreach (var dependsOn in DependsOnList)
                {
                    sb.Append(dependsOn);
                    sb.Append(";");
                }
                return sb.ToString();
            }
        }

        string IUtilityData.DependsOn
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override string ToString()
        {
            var sbDependent = new StringBuilder();
            var firstItem = true;
            foreach (var utilityData in DependentList)
            {
                if (!firstItem) sbDependent.Append(",");
                firstItem = false;
                sbDependent.Append(utilityData.Id);
            }

            var sbDependsOn = new StringBuilder();
            firstItem = true;
            foreach (var dependsOn in DependsOnList)
            {
                if (!firstItem) sbDependsOn.Append(",");
                firstItem = false;
                sbDependsOn.Append(dependsOn);
            }

            return string.Format("{0}, {1}, DependsOn: {2}, Dependents: {3}", Id, Name, sbDependsOn, sbDependent);
        }

    }

}
