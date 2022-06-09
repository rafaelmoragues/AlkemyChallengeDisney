using System.ComponentModel.DataAnnotations;

namespace AlkemyChallengeDisney.Models
{
    public class Personaje
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Age { get; set; }
        public double? Weight { get; set; }
        public string? History { get; set; }
        public string? Img { get; set; }

        public IEnumerable<Pelicula>? PeliculasList { get; set; }
    }
}
