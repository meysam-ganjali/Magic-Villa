using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Api.Models.DTOs;

public class VillaDto {
    public int? Id { get; set; }
    [Required]
    [MinLength(1)]
    public string Name { get; set; }
    public string Details { get; set; }
    public double Rate { get; set; }
    public int Sqft { get; set; }
    public int Occupancy { get; set; }
    public string ImageUrl { get; set; }
    public string Amenity { get; set; }

}