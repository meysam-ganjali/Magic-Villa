using MagicVilla_Api.Data;
using MagicVilla_Api.Models;
using MagicVilla_Api.Repository.IRepository;

namespace MagicVilla_Api.Repository;

public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository {
    private readonly DataBaseContext _db;
    public VillaNumberRepository(DataBaseContext db) : base(db) {
        _db = db;
    }


    public async Task<VillaNumber> UpdateAsync(VillaNumber entity) {
        entity.UpdatedDate = DateTime.Now;
        _db.VillaNumbers.Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }
}