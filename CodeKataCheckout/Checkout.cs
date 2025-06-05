namespace CodeKataCheckout;

public class Checkout : ICheckout
{
    private Dictionary<string, IPricingRule> _unitPricing;
    private Dictionary<string, int> _cart;
    public Checkout(IEnumerable<IPricingRule> pricingRules)
    {
        Initialise(pricingRules);
    }
    
    public void Scan(string sku)
    {
        if (!_cart.ContainsKey(sku))
        {
            _cart[sku] = 0;
        }
        _cart[sku]++;
    }

    public decimal GetTotal()
    {
        decimal total = 0;

        foreach (var item in _cart)
        {
            string sku = item.Key;
            int quantity = item.Value;
            
            var unitPricing = _unitPricing[sku];
            total += unitPricing.GetUnitPrice() * quantity;
        }
        
        return total;
    }

    private void Initialise(IEnumerable<IPricingRule> pricingRules)
    {
        _unitPricing = new Dictionary<string, IPricingRule>();
        _cart = new Dictionary<string, int>();

        foreach (var pricingRule in pricingRules)
        {
            _unitPricing.TryAdd(pricingRule.Sku, pricingRule);
        }
    }
}