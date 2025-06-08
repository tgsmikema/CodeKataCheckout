namespace CodeKataCheckout;

public class Buy2AGet1BFreePricingRule : IPricingRule
{
    private readonly string _triggerSku;
    private readonly int _triggerQty;
    private readonly decimal _triggerUnitPrice;
    private readonly string _targetSku;
    private readonly decimal _targetUnitPrice;
    private readonly decimal _totalPrice;

    public Buy2AGet1BFreePricingRule(string triggerSku, int triggerQty, decimal triggerUnitPrice, string targetSku,
        decimal targetUnitPrice, decimal totalPrice)
    {
        _triggerSku = triggerSku;
        _triggerQty = triggerQty;
        _triggerUnitPrice = triggerUnitPrice;
        _targetSku = targetSku;
        _targetUnitPrice = targetUnitPrice;
        _totalPrice = totalPrice;
    }
    public decimal CalculatePrice(Dictionary<string, int> cart)
    {
        if (!cart.ContainsKey(_targetSku) || !cart.ContainsKey(_triggerSku))
        {
            return CalculateSimplePrice(cart);
        }
        
        int triggerCount = cart[_triggerSku];
        int targetCount = cart[_targetSku];

        int sets = Math.Min(triggerCount / _triggerQty, targetCount);

        if (sets == 0)
        {
            return CalculateSimplePrice(cart);
        }
        
        // Calculate total with Promotion
        decimal total = sets * _totalPrice;
        
        // Add remaining items at simple price
        int remainingTrigger = triggerCount - (sets * _triggerQty);
        int remainingTarget = targetCount - sets;
        
        total += remainingTrigger * _triggerUnitPrice;
        total += remainingTarget * _targetUnitPrice;

        return total;
        
    }

    public PricingRule GetPricingRule()
    {
        return PricingRule.MultiSkusPricingRule;
    }

    public HashSet<string> GetSkus()
    {
        return [_triggerSku, _targetSku];
    }

    private decimal CalculateSimplePrice(Dictionary<string, int> cart)
    {
        decimal total = 0;
        foreach (var item in cart)
        {
            string sku = item.Key;
            int quantity = item.Value;

            if (sku == _triggerSku)
            {
                total += quantity * _triggerUnitPrice;
            } else if (sku == _targetSku)
            {
                total += quantity * _targetUnitPrice;
            }
        }
        return total;
    }
    
}