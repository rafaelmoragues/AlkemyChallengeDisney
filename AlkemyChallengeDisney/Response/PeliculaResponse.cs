using System.ComponentModel.DataAnnotations;

namespace AlkemyChallengeDisney.Response
{
    public class PeliculaResponse
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public string Img { get; set; }
    }
}
