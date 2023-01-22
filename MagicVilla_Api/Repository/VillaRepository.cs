using AutoMapper;
using MagicVilla_Api.Data;
using MagicVilla_Api.Models;
using MagicVilla_Api.Models.DTOs;
using MagicVilla_Api.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_Api.Repository;

public class VillaRepository : Repository<Villa>, IVillaRepository {
    private readonly DataBaseContext _db;

    public VillaRepository(DataBaseContext db) : base(db) {
        _db = db;
    }


    public async Task<Villa> UpdateAsync(Villa entity)
    {
        entity.UpdatedDate = DateTime.Now;
        _db.Villas.Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }
}