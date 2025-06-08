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
            return 0;
        }
        return _price * quantity;
    }

    public PricingRule GetPricingRule()
    {
        return PricingRule.SimplePricingRule;
    }

    public HashSet<string> GetSkus()
    {
        return [_sku];
    }
}