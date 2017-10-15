using System;
using Microsoft.AspNetCore.Mvc;
using StatlerWaldorfCorp.LocationService.Models;

namespace StatlerWaldorfCorp.LocationService.Controllers
{
    [Route("locations/{memberId}")]
    public class LocationRecordController : Controller
    {
        private ILocationRecordRepositary locationRepository;
        public LocationRecordController(ILocationRecordRepositary repostiary)
        {
            this.locationRepository = repostiary;   
        }

        [HttpPost]
        public IActionResult AddLocation(Guid memberId, [FromBody]LocationRecord locationRecord)
        {
            locationRepository.Add(locationRecord);
            return this.Created($"/locations/{memberId}/{locationRecord.ID}", locationRecord);
        }

        [HttpGet]
        public IActionResult GetLocationForMemeber(Guid memberId)
        {
            var memberRecords = locationRepository.AllForMember(memberId);
            return this.Ok(memberRecords);
        }

        [HttpGet]
        public IActionResult GetLatestForMember(Guid memberId)
        {
            var memberLatestRecord = locationRepository.GetLatestForMember(memberId);
            return this.Ok(memberLatestRecord);
        }
    }
}