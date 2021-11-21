using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazorClient.Models
{
    public class Person
    {

        public int Id { get; set; }
        

        [Required] public string FirstName { get; set; }
        
        
        [Required] public string LastName { get; set; }
        

        [Required] public string HairColor { get; set; }
        

        [Required] public string EyeColor { get; set; }
        
     
        [Required, Range(18, 70)] public int Age { get; set; }
        

        [Required] public float Weight { get; set; }

        [Required, Range(110, 210)] public int Height { get; set; }
        

        [Required] public string Sex { get; set; }
    }
}