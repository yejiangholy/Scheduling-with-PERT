using SampleSchedule.PropertyBags;
using System.Collections.Generic;
using SampleSchedule.Factories;
using CPI.Graphing.GraphingEngine.Contracts.Dc;
using System.Collections;

namespace SampleSchedual.Processors
{
    public interface IResourceSelector
    {
        NextResource SelectNext(Dictionary<int, IResource> resourceHash,Activity nextTask);
    }

    public class ResourceSelector : IResourceSelector
    {
        #region Declarations
        private Activity _nextTask;
        private IResource _FirstFreeTime;
        private IResource _SuitableOneInTime;
        private int _FirstFreeTimeKey;
        private IResource _PreferResource;
        private Dictionary<int, IResource> _ResourceHash;
        private NextResource _Response;

        #endregion Declarations

        public NextResource SelectNext(Dictionary<int, IResource> resourceHash, Activity nextTask)
        {
            _ResourceHash = resourceHash;
            _nextTask = nextTask;

            findSuitableOneInTime();
                takePreference();
                assignNextResource();

                return _Response;
        }

        public NextResource CreateNext(Dictionary<int, IResource> resourceHash, Activity nextTask)
        {
            _ResourceHash = resourceHash;
            _nextTask = nextTask;

            if(findNext()==null)
            {
                Employee nextOne = new Employee
                {
                    Id = _ResourceHash.Count,
                    Name = string.Format("E{0}", (_ResourceHash.Count)),
                    FreeTime = nextTask.Est-1,
                    StartWork = nextTask.Est-2,
                    AttendenceList = new ArrayList(),
                };
                _ResourceHash.Add(_ResourceHash.Count, nextOne );
                _PreferResource = nextOne;
                assignNextResource();
                return _Response;
            }
            else
            {
                _PreferResource = findNext();
                assignNextResource();
                return _Response;
            }
        }

        private Employee findNext()
        {
            Employee nextOne = null;
            foreach(var e in _ResourceHash)
            {
                if (((Employee)e.Value).FreeTime.CompareTo(_nextTask.Est)<=0)
                {
                    nextOne = (Employee)e.Value;
                    return nextOne;
                }
            }
            return nextOne;
        }

        private void findSuitableOneInTime()
        {
            var startTime = _nextTask.Est;
            _SuitableOneInTime = null;

            for(int i=0; i<_ResourceHash.Count;i++)
            {
                if(((Employee)_ResourceHash[i]).FreeTime-startTime<=0)
                {
                    _SuitableOneInTime = _ResourceHash[i];
                    break;
                }
            }

            if(_SuitableOneInTime==null)
            {
                findFirstFreeTime();
                _SuitableOneInTime = _FirstFreeTime;
            }

        }

        private void assignNextResource()
        {
            _Response = new NextResource
            {
                Resource = _PreferResource,
                AvailableTime = ((Employee)_PreferResource).FreeTime
            };
        }

        private void findFirstFreeTime()
        {
            _FirstFreeTime = _ResourceHash[0];

            foreach (var e in _ResourceHash)
            {
                if (((Employee)e.Value).FreeTime.CompareTo(((Employee)_FirstFreeTime).FreeTime) >= 0) continue;
                _FirstFreeTime = e.Value;
                _FirstFreeTimeKey = e.Key;
            }
        }

        private IResource getById(int id)
        {
            IResource getbyId;
            _ResourceHash.TryGetValue(id, out getbyId);
            return getbyId;
        }

        private void takePreference()
        {
            int preference_Id = _nextTask.Preference;
            if((preference_Id!=0)&&(preference_Id!=_FirstFreeTimeKey)&&((((Employee)_SuitableOneInTime).FreeTime+2).CompareTo(((Employee)getById(preference_Id)).FreeTime) >=0))
                {
                _PreferResource = getById(preference_Id);
                }
            else
            {
                _PreferResource = _SuitableOneInTime;
            }
        }

    }
}