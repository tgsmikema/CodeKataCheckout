namespace CodeKataCheckout;

public class Checkout : ICheckout
{
    private Dictionary<string, IPricingRule> _unitPricingRules;
    private List<IDiscountRule> _discountRules;
    private Cart _cart;
    public Checkout(IEnumerable<IPricingRule> pricingRules)
    {
        Initialise(pricingRules);
    }
    
    public void Scan(string sku)
    {
        _cart.AddToCart(sku);
    }

    public decimal GetTotal()
    {
        decimal total = 0;

        foreach (var item in _cart.GetAllItems())
        {
            // get full price for each sku
            var unitPricing = _unitPricingRules[item.sku];
            decimal subtotal = unitPricing.GetUnitPrice() * item.quantity;

            // get potential discounts for each sku
            decimal discount = _discountRules
                .Where(dr => dr.Sku == item.sku)
                .Sum(s => s.CalculateDiscount(item.quantity));
            
            total += subtotal - discount;
        }
        
        return total;
    }

    private void Initialise(IEnumerable<IPricingRule> pricingRules)
    {
        _unitPricingRules = new Dictionary<string, IPricingRule>();
        _discountRules = new List<IDiscountRule>();
        _cart = new Cart();
        
        foreach (var pricingRule in pricingRules)
        {
           if (pricingRule is BulkDiscountRule bulkDiscount)
           {
               _discountRules.Add(bulkDiscount);
           }
           else
           {
               _unitPricingRules.TryAdd(pricingRule.Sku, pricingRule);
           }
        }
    }
}