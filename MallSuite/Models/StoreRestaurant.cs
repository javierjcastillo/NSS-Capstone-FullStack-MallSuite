namespace MallSuite.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class StoreRestaurant
    {
        public int Id { get; set; }
        [Required]
        public string Type { get; set; } // either 'store' or 'restaurant'
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; } 
        public string Location { get; set; }
        public string ContactInfo { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; } 
        public List<Tag> Tags { get; set; }
    }
}
