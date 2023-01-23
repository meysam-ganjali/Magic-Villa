using MagicVilla_Web.Models.DTOs;
using Microsoft.AspNetCore.Http;

namespace MagicVilla_Web.Services.IServices;

public interface IVillaNumberService : IBaseService
{
    Task<T> GetAllAsync<T>();
    Task<T> GetAsync<T>(int id);
    Task<T> CreateAsync<T>(VillaNumberCreateDTO dto);
    Task<T> UpdateAsync<T>(VillaNumberUpdateDTO dto);
    Task<T> DeleteAsync<T>(int id);
}