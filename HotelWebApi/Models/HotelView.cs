using System.ComponentModel.DataAnnotations;

namespace HotelWebApi.Models
{
    public class HotelView
    {
        [Key]
        public int PropertyId { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public int PricePerNight { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public bool IsAvailable { get; set; }
    }
}
