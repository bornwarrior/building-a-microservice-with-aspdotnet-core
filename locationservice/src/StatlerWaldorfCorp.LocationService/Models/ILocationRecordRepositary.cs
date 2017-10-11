using System;
using System.Collections.Generic;

namespace StatlerWaldorfCorp.LocationService.Models
{
    public interface ILocationRecordRepositary
    {
        LocationRecord Add(LocationRecord locationRecord);
        LocationRecord Update(LocationRecord locationRecord);
        LocationRecord GetLocation(Guid memberId, Guid recordId);
        LocationRecord Delete(Guid memberId, Guid recordId);

        LocationRecord GetLatestForMember(Guid memberId);

        ICollection<LocationRecord> AllForMember(Guid memberId);
    }
}
