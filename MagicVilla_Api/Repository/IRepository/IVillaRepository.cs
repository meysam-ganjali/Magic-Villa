using MagicVilla_Api.Models;
using MagicVilla_Api.Models.DTOs;

namespace MagicVilla_Api.Repository.IRepository;

public interface IVillaRepository:IRepository<Villa>
{
    
    Task<Villa> UpdateAsync(Villa entity);
}