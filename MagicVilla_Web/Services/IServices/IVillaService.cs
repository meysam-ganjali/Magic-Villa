using MagicVilla_Web.Models.DTOs;
using Microsoft.AspNetCore.Http;

namespace MagicVilla_Web.Services.IServices;

public interface IVillaService:IBaseService
{
    Task<T> GetAllAsync<T>();
    Task<T> GetAsync<T>(int id);
    Task<T> CreateAsync<T>(VillaCreateDTO dto);
    Task<T> UpdateAsync<T>(VillaUpdateDTO dto);
    Task<T> DeleteAsync<T>(int id);
}