using System.ComponentModel.DataAnnotations;

namespace Start.Models
{
    public class Manufacturer
    {
        public int ManufacturerId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}