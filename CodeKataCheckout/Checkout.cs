namespace CodeKataCheckout;

public class Checkout : ICheckout
{
    private Dictionary<string, IPricingRule> _unitPricingRule;
    private List<IDiscountRule> _discountRules;
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
            
            var unitPricing = _unitPricingRule[sku];
            decimal subtotal = unitPricing.GetUnitPrice() * quantity;

            decimal discount = _discountRules
                .Where(dr => dr.Sku == sku)
                .Sum(s => s.CalculateDiscount(quantity));
            
            total += subtotal - discount;
        }
        
        return total;
    }

    private void Initialise(IEnumerable<IPricingRule> pricingRules)
    {
        _unitPricingRule = new Dictionary<string, IPricingRule>();
        _discountRules = new List<IDiscountRule>();
        _cart = new Dictionary<string, int>();

        foreach (var pricingRule in pricingRules)
        {
           if (pricingRule is BulkDiscount bulkDiscount)
           {
               _discountRules.Add(bulkDiscount);
           }
           else
           {
               _unitPricingRule.TryAdd(pricingRule.Sku, pricingRule);
           }
        }
    }
}