using System;
using System.Collections.Generic;
using StatlerWaldorfCorp.LocationService.Models;

namespace StatlerWaldorfCorp.LocationService.Persistence
{
    public class LocationRecordRepository : ILocationRecordRepositary
    {
        public LocationRecord Add(LocationRecord locationRecord)
        {
            throw new NotImplementedException();
        }

        public ICollection<LocationRecord> AllForMember(Guid memberId)
        {
            throw new NotImplementedException();
        }

        public LocationRecord Delete(Guid memberId, Guid recordId)
        {
            throw new NotImplementedException();
        }

        public LocationRecord GetLatestForMember(Guid memberId)
        {
            throw new NotImplementedException();
        }

        public LocationRecord GetLocation(Guid memberId, Guid recordId)
        {
            throw new NotImplementedException();
        }

        public LocationRecord Update(LocationRecord locationRecord)
        {
            throw new NotImplementedException();
        }
    }
}