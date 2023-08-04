using System.ComponentModel.DataAnnotations;

namespace MallSuite.Models
{
    public class StoreRestaurantTag
    {
        public int Id { get; set; }
        [Required]
        public int StoreRestaurantId { get; set; }
        public StoreRestaurant StoreRestaurant { get; set; }
        [Required]
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
