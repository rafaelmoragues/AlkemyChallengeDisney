using System.ComponentModel.DataAnnotations;

namespace AlkemyChallengeDisney.Models
{
    public class Pelicula
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        [Range(1, 5,
        ErrorMessage = "El valor de {0} debe estar entre {1} y {2}.")]
        public int Qualification { get; set; }
        public string? Img { get; set; }
        public IEnumerable<Personaje>? PersonajeList { get; set; }
    }
}
