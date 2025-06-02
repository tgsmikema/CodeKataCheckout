namespace CodeKataCheckout;

public class Checkout : ICheckout
{
    private readonly IEnumerable<IPricingRule> _pricingRules;
    private readonly List<string> _cart;
    public Checkout(IEnumerable<IPricingRule> pricingRules)
    {
        _pricingRules = pricingRules;
        _cart = new List<string>();
    }
    
    public void Scan(string sku)
    {
        _cart.Add(sku);
    }
    
    public decimal GetTotal()
    {
        decimal total = 0;
        
        foreach (var item in _cart)
        {
            var pricingRule = _pricingRules.ToList().FirstOrDefault(x => x.Sku == item);
            if (pricingRule != null)
            {
                total += pricingRule.CalculatePrice();
            }
        }
        return total;
    }
}