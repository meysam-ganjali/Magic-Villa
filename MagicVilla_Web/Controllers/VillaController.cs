using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTOs;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

namespace MagicVilla_Web.Controllers {
    public class VillaController : Controller {
        private readonly IVillaService _villaService;
        private IMapper _mapper;

        public VillaController(IVillaService villaService, IMapper mapper) {
            _villaService = villaService;
            _mapper = mapper;
        }
        public async Task<IActionResult> IndexVilla() {
            List<VillaDto> list = new();
            var response = await _villaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess) {
                list = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        public async Task<IActionResult> CreateVilla() {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVilla(VillaCreateDTO model) {
            if (ModelState.IsValid) {

                var response = await _villaService.CreateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess) {
                    TempData["success"] = "Villa created successfully";
                    return RedirectToAction(nameof(IndexVilla));
                }
            }
            TempData["error"] = "Error encountered.";
            return View(model);
        }

        public async Task<IActionResult> UpdateVilla(int id) {
            var villa = await _villaService.GetAsync<APIResponse>(id);
            if (villa != null && villa.IsSuccess) {
                VillaDto model = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(villa.Result));
                return View(_mapper.Map<VillaUpdateDTO>(model));
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVilla(VillaUpdateDTO model) {
            if (ModelState.IsValid) {

                var response = await _villaService.UpdateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess) {
                    TempData["success"] = "Villa Update successfully";
                    return RedirectToAction(nameof(IndexVilla));
                }
            }
            TempData["error"] = "Error encountered.";
            return View(model);
        }

        public async Task<IActionResult> DeleteVilla(int id) {

            var response = await _villaService.DeleteAsync<APIResponse>(id);
            if (response != null && response.IsSuccess) {
                TempData["success"] = "Villa Delete successfully";
                return RedirectToAction(nameof(IndexVilla));
            }
            TempData["error"] = "Error encountered.";
            return RedirectToAction(nameof(IndexVilla));
        }
    }
}
