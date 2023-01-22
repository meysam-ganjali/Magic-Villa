using AutoMapper;
using MagicVilla_Api.Data;
using MagicVilla_Api.Models;
using MagicVilla_Api.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {

        private ILogger<VillaApiController> _logger;
        private readonly DataBaseContext _db;
        private IMapper _mapper;

        public VillaApiController(ILogger<VillaApiController> logger, DataBaseContext db, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }
        [HttpGet(Name = "GetVillas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVilas()
        {
            var villa = await _db.Villas.ToListAsync();
            if (villa != null)
            {
                _logger.LogInformation($"Get {villa.Count()} :  Number Villas In Date {DateTime.Now.ToShortDateString()}");
            }
            return Ok(villa);
        }


        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVilla(int id) {
            if (id == 0 || id < 0) {
                return BadRequest();
            }
            var villa = await _db.Villas.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (villa == null) {
                return NotFound();
            }
            return Ok(villa);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateVilla([FromBody] VillaCreateDTO villaDto)
        {
            var villa = _mapper.Map<Villa>(villaDto);
           await _db.Villas.AddAsync(villa);
           await _db.SaveChangesAsync();
            return Ok(villaDto);
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveVilla(int id) {
            if (id == 0) {
                return BadRequest();
            }

            var villa = await _db.Villas.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (villa == null) {
                return NotFound();
            }
            _db.Villas.Remove(villa);
            await _db.SaveChangesAsync();

            return Ok();
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateVillas(int id, [FromBody] VillaDto villaDto) {
            if (villaDto.Id == 0) {
                return BadRequest();
            }

            var villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (villa == null) {
                return NotFound();
            }

            var mappVilla = _mapper.Map<Villa>(villaDto);
            _db.Villas.Update(mappVilla);
            await _db.SaveChangesAsync();

            return Ok();
        }
    }
}
