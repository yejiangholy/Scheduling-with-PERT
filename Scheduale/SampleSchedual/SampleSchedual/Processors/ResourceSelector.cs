using SampleSchedule.PropertyBags;
using System;
using System.Collections.Generic;
using SampleSchedule.Factories;

namespace SampleSchedual.Processors
{
    public interface IResourceSelector
    {
        NextResource SelectNext(Dictionary<int, IResource> resourceHash,IEdge nextTask);
    }

    public class ResourceSelector : IResourceSelector
    {
        #region Declarations
        private IEdge _nextTask;
        private IResource _FirstFreeTime;
        private int _FirstFreeTimeKey;
        private IResource _PreferResource;
        private Dictionary<int, IResource> _ResourceHash;
        private NextResource _Response;

        #endregion Declarations

        public NextResource SelectNext(Dictionary<int, IResource> resourceHash,IEdge nextTask)
        {
            _ResourceHash = resourceHash;
            _nextTask = nextTask;

            findFirstFreeTime();
            takePreference();
            assignNextResource();

            return _Response;
        }

        private void assignNextResource()
        {
            _Response = new NextResource
            {
                Resource = _PreferResource,
                AvailableTime = _PreferResource.FreeTime
            };
        }

        private void findFirstFreeTime()
        {
            _FirstFreeTime = _ResourceHash[1];

            foreach (var e in _ResourceHash)
            {
                if (e.Value.FreeTime.CompareTo(_FirstFreeTime.FreeTime) >= 0) continue;
                _FirstFreeTime = e.Value;
                _FirstFreeTimeKey = e.Key;
            }
        }

        private IResource getById(int id)
        {
            IResource getbyId;
            _ResourceHash .TryGetValue(id, out getbyId);
            return getbyId;
        }

        private void takePreference()
        {
            int preference_Id = _nextTask.Preference;
            if((preference_Id!=0)&&(preference_Id!=_FirstFreeTimeKey)&&(_FirstFreeTime.FreeTime.AddDays(2.0).CompareTo(getById(preference_Id).FreeTime) >=0))
                {
                _PreferResource = getById(preference_Id);
                }
            else
            {
                _PreferResource = _FirstFreeTime;
            }
        }

    }
}