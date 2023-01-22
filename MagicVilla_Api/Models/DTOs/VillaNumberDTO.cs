using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Api.Models.DTOs;

public class VillaNumberDTO
{
    [Required]
    public int VillaNo { get; set; }
    [Required]
    public int VillaID { get; set; }
    public string SpecialDetails { get; set; }
    public VillaDto Villa { get; set; }
}