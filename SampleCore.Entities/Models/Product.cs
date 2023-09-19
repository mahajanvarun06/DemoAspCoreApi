using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SampleCore.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Category { get; set; }
        public string Color { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Please enter valid Number and should be greater than 0")]
        public decimal UnitPrice { get; set; }
        public int AvailableQuantity { get; set; }
    }
}
