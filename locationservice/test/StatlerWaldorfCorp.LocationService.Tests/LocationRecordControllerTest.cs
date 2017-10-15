using System;
using Xunit;
using StatlerWaldorfCorp.LocationService.Controllers;
using StatlerWaldorfCorp.LocationService.Persistence;
using StatlerWaldorfCorp.LocationService.Models;
using StatlerWaldorfCorp.LocationService;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace StatlerWaldorfCorp.LocationService.Tests
{
    public class LocationRecordControllerTest
    {
        [Fact]
        public void ShouldAdd()
        {
            ILocationRecordRepositary repository = new InMMemoryLocationRecordRepository();
            LocationRecordController controller = new LocationRecordController(repository);
            
            Guid memberGuid = Guid.NewGuid();
            controller.AddLocation(memberGuid, new LocationRecord(){ ID = Guid.NewGuid(),
            MemberID = memberGuid, Timestamp =1 });
            controller.AddLocation(memberGuid, new LocationRecord(){ ID = Guid.NewGuid(),
            MemberID = memberGuid, Timestamp =2 });

            Assert.Equal(2, repository.AllForMember(memberGuid).Count());
        }

        [Fact]
        public void ShouldReturnEmptyListForNewMembers()
        {
            ILocationRecordRepositary repository = new InMMemoryLocationRecordRepository();
            LocationRecordController controller = new LocationRecordController(repository);

            Guid memberGuid = Guid.NewGuid();

            ICollection<LocationRecord> locationRecords = 
                ((controller.GetLocationForMemeber(memberGuid) as ObjectResult).Value as ICollection<LocationRecord>);

            Assert.Equal(0, locationRecords.Count());

        }

        [Fact]
        public void ShouldTrackAllLocationsForMember()
        {
            ILocationRecordRepositary repository = new InMMemoryLocationRecordRepository();
            LocationRecordController controller = new LocationRecordController(repository);
            Guid memberGuid = Guid.NewGuid();

            controller.AddLocation(memberGuid, new LocationRecord(){ID = Guid.NewGuid(), Timestamp = 1,
                MemberID = memberGuid, Latitude = 12.3f});

            controller.AddLocation(memberGuid, new LocationRecord(){ID = Guid.NewGuid(), Timestamp = 2,
                MemberID = memberGuid, Latitude = 23.4f});
        
            controller.AddLocation(memberGuid, new LocationRecord(){ID = Guid.NewGuid(), Timestamp = 3,
                MemberID = Guid.NewGuid(), Latitude = 23.4f});
            
            ICollection<LocationRecord> locationRecords = 
                ((controller.GetLocationForMemeber(memberGuid) as ObjectResult).Value as ICollection<LocationRecord>);
            
            Assert.Equal(2, locationRecords.Count());

        }

        [Fact]
        public void ShouldTrackNullLatestForNewMembers()
        {
        //Given
        
            ILocationRecordRepositary repository = new InMMemoryLocationRecordRepository();
            LocationRecordController controller = new LocationRecordController(repository);
            Guid memberGuid = Guid.NewGuid();

        //When
            LocationRecord latestLocation = ((controller.GetLatestForMember(memberGuid) as ObjectResult).Value as LocationRecord);

        //Then
            Assert.Null(latestLocation);
        }
    }
}
