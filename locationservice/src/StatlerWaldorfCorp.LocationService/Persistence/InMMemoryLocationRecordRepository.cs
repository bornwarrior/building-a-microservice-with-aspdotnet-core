
using System;
using System.Collections.Generic;
using StatlerWaldorfCorp.LocationService.Models;
using System.Linq;
namespace StatlerWaldorfCorp.LocationService.Persistence
{
    public class InMMemoryLocationRecordRepository : ILocationRecordRepositary
    {
        private static Dictionary<Guid, SortedList<long, LocationRecord>> locationRecords;
        public InMMemoryLocationRecordRepository()
        {
            if (locationRecords == null)
            {
                locationRecords = new Dictionary<Guid, SortedList<long, LocationRecord>>();

            }
        }

            public LocationRecord Add(LocationRecord locationRecord)
            {
                var memberRecords = getMemberRecords(locationRecord.MemberID);
                memberRecords.Add(locationRecord.Timestamp, locationRecord);
                return locationRecord;
            }

        private SortedList<long, LocationRecord> getMemberRecords(Guid memberID)
        {
           if(!locationRecords.ContainsKey(memberID))
           {
               locationRecords.Add(memberID, new SortedList<long,LocationRecord>());

           }

           var list = locationRecords[memberID];
           return list;
        }

        public ICollection<LocationRecord> AllForMember(Guid memberId)
            {
                var memberRecords = getMemberRecords(memberId);
                return memberRecords.Values.Where( l => l.MemberID == memberId).ToList();
               }

            public LocationRecord Delete(Guid memberId, Guid recordId)
            {
                throw new NotImplementedException();
            }

            public LocationRecord GetLatestForMember(Guid memberId)
            {
                var memberRecords = getMemberRecords(memberId);
                LocationRecord lr = memberRecords.Values.LastOrDefault();
                return lr;
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