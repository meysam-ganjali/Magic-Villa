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

        public VillaApiController(ILogger<VillaApiController> logger, IVillaRepository villaRepository)
        {
            _logger = logger;
            _villaRepository = villaRepository;
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

            var villa = await _villaRepository.GetAsync(id);
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
            var result = await _villaRepository.CreateAsync(villaDto);
            if (result == null)
            {
                _logger.LogError($"Parameter InValid ( BadRequest )");
                return BadRequest();
            }
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

            bool isSuccess = await _villaRepository.RemoverAsync(id);

            if (!isSuccess) {
                _logger.LogError($"Remove Villa Faild {isSuccess}");
                return BadRequest(isSuccess);
            }
            _logger.LogInformation($"Villa {id} Removeed From DataBase");

            return Ok(isSuccess);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateVillas([FromBody] VillaUpdateDTO villaDto) {
            if (villaDto.Id == 0) {
                _logger.LogError("Id Equal 0 Not Found In DataBase");
                return BadRequest();
            }

            var villa = await _villaRepository.UpdateAsync(villaDto);
            if (villa == null) {
                _logger.LogError("Update Vila Is Faild");
                return BadRequest();
            }

            return Ok(villa);
        }
    }
}
