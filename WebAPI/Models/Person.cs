using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebAPI.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(20)] public string FirstName { get; set; }
        [Required, MaxLength(20)] public string LastName { get; set; }
        [Required] public string HairColor { get; set; }
        [Required] public string EyeColor { get; set; }
        [Required, Range(18, 70, ErrorMessage = "We work only with adults, so 18+ ")] public int Age { get; set; }
        [Required] public float Weight { get; set; }
        [Required, Range(110, 210)] public int Height { get; set; }
        [Required] public string Sex { get; set; }
    }
}