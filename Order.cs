public class Order {
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } // e.g., Pending, Paid, Shipped

    public List<OrderItem> OrderItems { get; set; }
    public Customer Customer { get; set; }
}
