namespace AcApp.Models
{
    public class Order: BaseId
    {
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderItems> OrderItems { get; set; }
    }
}
