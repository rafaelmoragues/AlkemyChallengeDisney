using System.ComponentModel.DataAnnotations;

namespace AlkemyChallengeDisney.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Direction { get; set; }
        public int Age { get; set; }
        [Required]
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime FechaAlta { get; set; } = DateTime.Now;
        public Role Role { get; set; }
    }
}
