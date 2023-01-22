using AutoMapper;
using MagicVilla_Api.Data;
using MagicVilla_Api.Models;
using MagicVilla_Api.Models.DTOs;
using MagicVilla_Api.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_Api.Repository;

public class VillaRepository:IVillaRepository
{
    private readonly DataBaseContext _db;
    private IMapper _mapper;

    public VillaRepository(DataBaseContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<VillaDto>> GetAllAsync()
    {
        var villas =  await _db.Villas.ToListAsync();
        var mappToDto = _mapper.Map<IEnumerable<VillaDto>>(villas);
        return mappToDto;
    }

    public async Task<VillaDto> GetAsync(int id)
    {
        var villa = await _db.Villas.FirstOrDefaultAsync(x=>x.Id == id);
        if (villa == null)
        {
            return null;
        }
        var mappToDto = _mapper.Map<VillaDto>(villa);
        return mappToDto;
    }

    public async Task<bool> RemoverAsync(int id)
    {
        var villa = await _db.Villas.FirstOrDefaultAsync(x => x.Id == id);
        if (villa == null)
        {
            return false;
        }

        _db.Villas.Remove(villa);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<VillaDto> UpdateAsync(VillaUpdateDTO villaDto)
    {
        var villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == villaDto.Id); ;
        if (villa == null)
        {
            return null;
        }

        var mappToVilla = _mapper.Map<Villa>(villaDto);
        _db.Villas.Update(mappToVilla);
        await _db.SaveChangesAsync();
        return _mapper.Map<VillaDto>(mappToVilla);
    }

    public async Task<VillaDto> CreateAsync(VillaCreateDTO villaDto)
    {

        var mappToVilla = _mapper.Map<Villa>(villaDto);
        await _db.Villas.AddAsync(mappToVilla);
        await _db.SaveChangesAsync();
        return _mapper.Map<VillaDto>(mappToVilla);
    }
}