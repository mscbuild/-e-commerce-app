public class Customer {
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; } // use Identity or hash manually
    public string Address { get; set; }
}
