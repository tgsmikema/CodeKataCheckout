namespace CodeKataCheckout;

public class SimplePricingRule : IPricingRule
{
    private readonly string _sku;
    private readonly decimal _price;

    public SimplePricingRule(string sku, decimal price)
    {
        _sku = sku;
        _price = price;
    }
    public decimal CalculatePrice(Dictionary<string, int> cart)
    {
        if (!cart.TryGetValue(_sku, out var quantity))
        {
            throw new ArgumentException($"Sku {_sku} not found");
        }
        return _price * quantity;
    }
}