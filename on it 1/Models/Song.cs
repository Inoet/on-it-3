using System.ComponentModel.DataAnnotations;

namespace on_it_1.Models
{
    public class Song
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public string Album { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Tuning { get; set; } = "E Standard"; 
    }
}
