using System.ComponentModel.DataAnnotations;

namespace MallSuite.Models
{
    public class Tag
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
