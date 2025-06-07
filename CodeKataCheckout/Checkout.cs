namespace CodeKataCheckout;

public class Checkout
{
    private readonly List<IPricingRule> _pricingRules;
    private readonly Dictionary<string, int> _cart;
    public Checkout(List<IPricingRule> pricingRules)
    {
        _pricingRules = pricingRules;
        _cart = new Dictionary<string, int>();
    }

    public void Scan(string sku)
    {
        if (!_cart.TryGetValue(sku, out var quantity))
        {
            _cart[sku] = 1;
        }
        else
        {
            _cart[sku] = quantity + 1;
        }
    }

    public decimal Total()
    {
        decimal total = 0;
        foreach (var pricingRule in _pricingRules)
        {
            total += pricingRule.CalculatePrice(_cart);
        }
        return total;
    }
}