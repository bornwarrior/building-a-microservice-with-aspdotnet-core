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
    }
}
