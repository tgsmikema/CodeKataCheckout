namespace CodeKataCheckout;

public class Cart
{
    private readonly Dictionary<string, int> _items;

    public Cart()
    {
        _items = new Dictionary<string, int>();
    }

    public void AddToCart(string sku)
    {
        if (!_items.ContainsKey(sku))
        {
            _items[sku] = 0;
        }
        _items[sku]++;
    }

    public int GetQuantity(string sku)
    {
        var exist = _items.TryGetValue(sku, out int quantity);
        return exist ? quantity : 0;
    }

    public IEnumerable<(string sku, int quantity)> GetAllItems()
    {
        var result = new List<(string sku, int quantity)>();

        foreach (var item in _items)
        {
            string sku = item.Key;
            int quantity = item.Value;
            
            result.Add((sku, quantity));
        }
        
        return result;
    }

}