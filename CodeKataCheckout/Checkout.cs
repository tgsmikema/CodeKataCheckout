namespace CodeKataCheckout;

public class Checkout : ICheckout
{
    private readonly IEnumerable<IPricingRule> _pricingRules;
    private readonly Dictionary<string, decimal> _cart;
    public decimal Total { get; private set; }
    private string _currentItem;
    public Checkout(IEnumerable<IPricingRule> pricingRules)
    {
        _pricingRules = pricingRules;
        _cart = new Dictionary<string, decimal>();
        Total = 0;
        _currentItem = null;
    }
    
    public void Scan(string sku)
    {
        _currentItem = sku;
        if (!_cart.ContainsKey(sku))
        {
            _cart.Add(sku, 1);
        }
        else
        {
            _cart[sku] += 1;
        }
        UpdateTotal();
    }

    private void UpdateTotal()
    {
        foreach (var pricingRule in _pricingRules.ToList())
        {
            if (pricingRule.Sku == _currentItem)
            {
                Total += pricingRule.CalculatePrice();
            }
        }
        
    }
}