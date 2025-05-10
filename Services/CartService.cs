public class CartService
{
    public List<CartItem> Items { get; } = new();

    public void AddItem(Product product)
    {
        var item = Items.FirstOrDefault(i => i.Product.Id == product.Id);
        if (item != null)
            item.Quantity++;
        else
            Items.Add(new CartItem { Product = product, Quantity = 1 });
    }

    public void ClearCart() => Items.Clear();
}

public class CartItem
{
    public Product Product { get; set; }
    public int Quantity { get; set; }
}
