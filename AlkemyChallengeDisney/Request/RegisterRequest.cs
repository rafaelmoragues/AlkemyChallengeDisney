using System.ComponentModel.DataAnnotations;

namespace AlkemyChallengeDisney.Request
{
    public class RegisterRequest
    {
        [Required]
        public string Name { get; set; }
        public string? Direction { get; set; }
        public int Age { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
