namespace CodeKataCheckout;

public class Checkout : ICheckout
{
    private Dictionary<string, IPricingRule> _unitPricingRules;
    private List<IDiscountRule> _discountRules;
    private ICart _cart;
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
            
            // get max discounts for each sku
            total += subtotal - FindMaxDiscounts(item.sku, item.quantity);
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

    private decimal FindMaxDiscounts(string sku, int quantity)
    {
        // get bulk discounts for each sku
        // assumption that buy more to save more
        var discounts =
            _discountRules.Where(x => x.Sku == sku)
                .OrderByDescending(y => y.BulkQty).ToList();

        int remainingQty = quantity;
        decimal totalDiscount = 0;
        
        foreach (var discount in discounts)
        {
            decimal currentDiscount = discount.CalculateDiscount(remainingQty);
            if (currentDiscount > 0)
            {
                int sets = remainingQty / discount.BulkQty;
                totalDiscount += currentDiscount;
                remainingQty -= sets * discount.BulkQty;
            }
        } 
        return totalDiscount;
    }
}