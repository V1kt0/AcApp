namespace AcApp.Models
{
    public class OrderItems: BaseId
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public virtual Order Order { get; set; }
        public int OrderId { get; set; }
        public virtual Product Product { get; set; }
        public int ProductID { get; set; }
    }
}
