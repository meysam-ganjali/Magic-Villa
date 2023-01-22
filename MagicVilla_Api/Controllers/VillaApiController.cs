using MagicVilla_Api.Data;
using MagicVilla_Api.Models;
using MagicVilla_Api.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class VillaApiController : ControllerBase {


        [HttpGet(Name = "GetVillas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetVilas() {
            var villa = VillaStore.VillaList.ToList();

            return Ok(villa);
        }


        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetVilla(int id) {
            if (id == 0 || id < 0) {
                return BadRequest();
            }
            var villa = VillaStore.VillaList.FirstOrDefault(x => x.Id.Equals(id));
            if (villa == null) {
                return NotFound();
            }
            return Ok(villa);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult CreateVilla([FromBody] VillaDto villaDto) {
            if (villaDto.Id == 0) {
                return BadRequest();
            }
            villaDto.Id = VillaStore.VillaList.OrderByDescending(x=>x.Id).FirstOrDefault().Id + 1;
            VillaStore.VillaList.Add(villaDto);
            return Ok(villaDto);
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult RemoveVilla(int id) {
            if (id == 0) {
                return BadRequest();
            }

            var villa = VillaStore.VillaList.FirstOrDefault(x => x.Id.Equals(id));

            if (villa == null) {
                return NotFound();
            }
            VillaStore.VillaList.Remove(villa);


            return Ok();
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateVillas(int id, [FromBody] VillaDto villaDto) {
            if (villaDto.Id == 0) {
                return BadRequest();
            }

            var villa = VillaStore.VillaList.FirstOrDefault(x => x.Id.Equals(id));
            if (villa == null) {
                return NotFound();
            }

            villa.Amenity = villaDto.Amenity;
            villa.Name = villaDto.Name;
            villa.Occupancy = villaDto.Occupancy;
            villa.Sqft = villaDto.Sqft;
            villa.Details = villaDto.Details;
            villa.Rate = villaDto.Rate;

            return Ok();
        }
    }
}
