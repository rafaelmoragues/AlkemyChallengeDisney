using System.ComponentModel.DataAnnotations;

namespace AlkemyChallengeDisney.Models
{
    public class Genero
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Img { get; set; } 
        public IEnumerable<Pelicula>? PeliculaList { get; set; }
    }
}
