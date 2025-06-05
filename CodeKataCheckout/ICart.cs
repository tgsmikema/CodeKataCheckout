namespace CodeKataCheckout;

public interface ICart
{
    void AddToCart(string sku);
    IEnumerable<(string sku, int quantity)> GetAllItems();
}