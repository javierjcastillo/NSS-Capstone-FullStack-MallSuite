using System.ComponentModel.DataAnnotations;

namespace MallSuite.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirebaseUserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
