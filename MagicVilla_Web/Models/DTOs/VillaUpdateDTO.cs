using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Models.DTOs;

public class VillaUpdateDTO {
    [Required]
    public int Id { get; set; }
    [Required]

    public string Name { get; set; }
    public string Details { get; set; }
    [Required]
    public double Rate { get; set; }
    [Required]
    public int Occupancy { get; set; }
    [Required]
    public int Sqft { get; set; }
    [Required]
    public string ImageUrl { get; set; }
    public string Amenity { get; set; }
}