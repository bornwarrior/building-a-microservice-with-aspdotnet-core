using System;
using Xunit;
using StatlerWaldorfCorp.LocationService.Controllers;
using StatlerWaldorfCorp.LocationService.Persistence;
using StatlerWaldorfCorp.LocationService.Models;
using StatlerWaldorfCorp.LocationService;
using System.Linq;

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
    }
}
