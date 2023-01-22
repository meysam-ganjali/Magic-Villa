using AutoMapper;
using MagicVilla_Api.Data;
using MagicVilla_Api.Models;
using MagicVilla_Api.Models.DTOs;
using MagicVilla_Api.Repository.IRepository;
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
        private readonly IVillaRepository _villaRepository;
        private IMapper _mapper;

        public VillaApiController(ILogger<VillaApiController> logger, IVillaRepository villaRepository, IMapper mapper)
        {
            _logger = logger;
            _villaRepository = villaRepository;
            _mapper = mapper;
        }
        [HttpGet(Name = "GetVillas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVilas()
        {
            var villa = await _villaRepository.GetAllAsync();
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
                _logger.LogError($"{id} No Exist In Data Base (Bad Request)");
                return BadRequest();
            }

            var villa = await _villaRepository.GetAsync(x => x.Id == id);
            if (villa == null) {
                _logger.LogError($"{id} No Exist In Data Base");
                return NotFound();
            }
            _logger.LogInformation("Get Successed Villa");
            return Ok(villa);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateVilla([FromBody] VillaCreateDTO villaDto)
        {
            var mappToVilla = _mapper.Map<Villa>(villaDto);
            await _villaRepository.CreateAsync(mappToVilla);
            _logger.LogInformation($"Add Successed Villa");
            return Ok(villaDto);
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveVilla(int id) {
            if (id == 0) {
                _logger.LogError($"{id} == 0 No Exist In Data Base");
                return BadRequest();
            }

            var villa = await _villaRepository.GetAsync(x => x.Id == id);

            if (villa == null) {
                _logger.LogError($"Villa {id} Not Found");
                return NotFound();
            }

            await _villaRepository.RemoveAsync(villa);
            _logger.LogInformation($"Villa {villa.Id} Removeed From DataBase");

            return Ok();
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateVillas([FromBody] VillaUpdateDTO villaDto)
        {
            var mappToVilla = _mapper.Map<Villa>(villaDto);
            var villa = await _villaRepository.GetAsync(x => x.Id == villaDto.Id,tracked:false);
            if (villa == null) {
                _logger.LogError(" Villa NotFound");
                return NotFound();
            }

            await _villaRepository.UpdateAsync(mappToVilla);
            return Ok(villa);
        }
    }
}
