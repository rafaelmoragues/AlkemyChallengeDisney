using AlkemyChallengeDisney.Models;

namespace AlkemyChallengeDisney.Response
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
    }
}
