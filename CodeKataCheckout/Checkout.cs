namespace CodeKataCheckout;

public class Checkout
{
    private readonly IEnumerable<PricingRule> _pricingRules;
    private readonly List<string> _scannedItems;
    public Checkout(IEnumerable<PricingRule> pricingRules)
    {
        _pricingRules = pricingRules;
        _scannedItems = new List<string>();
    }

    public decimal Total()
    {
        decimal total = 0;
        
        foreach (var item in _scannedItems)
        {
            var pricingRule = _pricingRules.ToList().FirstOrDefault(x => x.Sku == item);
            if (pricingRule != null)
            {
                total += pricingRule.Price;
            }
        }
        return total;
    }

    public void Scan(string sku)
    {
        _scannedItems.Add(sku);
    }
}