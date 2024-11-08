using System.ComponentModel.DataAnnotations;

namespace AcApp.Models
{
    public class Product: BaseId
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
        public int StockQuantity { get; set; }
    }
}
