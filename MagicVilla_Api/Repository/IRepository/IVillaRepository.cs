using MagicVilla_Api.Models.DTOs;

namespace MagicVilla_Api.Repository.IRepository;

public interface IVillaRepository
{
    Task<IEnumerable<VillaDto>> GetAllAsync();
    Task<VillaDto> GetAsync(int id);
    Task<bool> RemoverAsync(int id);
    Task<VillaDto> UpdateAsync(VillaUpdateDTO villaDto);
    Task<VillaDto> CreateAsync(VillaCreateDTO villaDto);
}